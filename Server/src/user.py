from logger import Logger
from protocol import Header,HeaderParser,Protocol
from quiz import Quiz, Question

import requests, json, socket, hashlib, os

class URL(object):
    local = 'http://127.0.0.1:5000/api/'
    remote = 'http://molly.ovh:5000/api/'

class User(object):
    def __init__(self, conn, addr):
        super().__init__()

        self.address = addr
        self.socket = conn
        self.connected = True
        self.uuid = None

    def __repr__(self):
        return str(self.address)+' '+str(self.uuid)

    def quit(self, message: str):
        self.connected = False
        self.socket.close()
        Logger.log('Client closed from:'+str(self.address)+' '+str(message))

    def handle(self):
        r = self.socket.recv(3)
        if r != b'':
            headerType, size = HeaderParser.decode(r)
            data = Protocol.decode(self.socket.recv(size))
            h,p = None,None

            if headerType == Header.LOG:
                r = requests.get(URL.local+'users', params={'username':data['login']})
                j = r.json()
                if r.status_code == 200 and j != {}:
                    u2 = bytes.fromhex(j['password'])
                    u1 = self.passwordHash(data['password'], u2[:32])

                    if u1[32:] == u2[32:]:
                        h,p = Protocol.encode(Header.SES, session = self.uuid)
                        self.transfer(h,p)
                        Logger.log('User logged in ('+str(data['login'])+')')
                        return UserLogged(self,j['id'],j['username'])
                
                h,p = Protocol.encode(Header.ERR, msg = 'Invalid login data')
                Logger.log('User login invalid data '+ str(self.address))           
            elif headerType == Header.REG:
                r = requests.post(URL.local+'users', json={'username':data['login'], 'password':self.passwordHash(data['password']).hex()})
                if r.status_code == 201:
                    h,p = Protocol.encode(Header.ACK, msg = 'Created Account')
                    Logger.log('User registered ')
                elif r.status_code == 409:
                    h,p = Protocol.encode(Header.ERR, msg = 'Account already exists')
                    msg = r.json()['Message']
                    Logger.log('User thats already exists creation try ('+str(data['login'])+')')
                else:
                    h,p = Protocol.encode(Header.ERR, msg = 'Invalid register data')
                    Logger.log('User register invalid data ')
            elif headerType == Header.DIS:
                raise socket.error(data['msg'])
                
            if h != None and p != None:
                self.transfer(h,p)
        return None

    def transfer(self,h,p):
        self.socket.send(h)
        self.socket.send(p)

    def passwordHash(self, password: str, salt=None):
        salt = salt or os.urandom(32)
        key = hashlib.pbkdf2_hmac('sha256',password.encode(),salt,10000)
        return (salt+key)

class UserLogged(User):
    def __init__(self, user: User, dbID, username):
        super().__init__(user.socket,user.address)
        self.uuid = user.uuid
        self.dbID = dbID
        self.username = username
        self.quiz = None

    def __repr__(self):
        return str(self.address)+' '+str(self.uuid)+' '+self.username

    def handle(self):
        r = self.socket.recv(3)
        if r != b'':
            headerType, size = HeaderParser.decode(r)
            data = Protocol.decode(self.socket.recv(size))
            h,p = None,None

            if headerType == Header.DIS:
                raise socket.error('Disconnect')   
            elif headerType == Header.ALI:
                r = requests.get(URL.local+'quizzes-categories')

                if r.status_code == 200:
                    j = r.json()
                    l = []
                    for x in j:
                        l.append(x['category_name'])
                    h,p = Protocol.encode(Header.LIS, quizes=l)
                    Logger.log('Category request')
                else:
                    h,p = Protocol.encode(Header.ERR, msg='Cant get categories')
                    Logger.log('Category request failed')
            elif headerType == Header.STR:
                r = requests.get(URL.local+'quizzes-categories')
                r2 = requests.get(URL.local+'stats', params={'userid':self.dbID})

                if r.status_code == 200 and r2.status_code == 200:
                    cat = r.json()
                    stat = r2.json()

                    print(stat)
                    ret = []
                    for x in stat:
                        for y in cat:
                            if x['quizid'] == y['id']:
                                print('abc')
                                ret.append({'category':y['category_name'], 'score': x['score']})
                                break
                    
                    if ret != []:
                        h,p = Protocol.encode(Header.STA, stats=ret)
                    else:
                        h,p = Protocol.encode(Header.ERR,msg='empty')
                    Logger.log('Stats request '+str(self.dbID))
                else:
                    Logger.log('Stats request failed'+str(self.dbID))
            elif headerType == Header.QUI:
                r = requests.get(URL.local+'quizzes', params={'category_name':data['category']})
                
                if r.status_code == 200 and self.quiz == None:
                    j = r.json()
                    qq = []
                    for x in j:
                        q = x['Question']['question']
                        cor = None
                        a = x['Answers']
                        for y in a:
                            if y['id'] == x['Question']['correct_answer']:
                                cor = y['answer']
                        
                        qq.append(Question(q,a[0]['answer'],a[1]['answer'],a[2]['answer'],a[3]['answer'],cor))

                    self.quiz = Quiz(qq,j[0]['Question']['quizid'])
                    question = self.quiz.next()
                    h,p = Protocol.encode(Header.QUE, question=question.question,a1=question.a1,a2=question.a2,a3=question.a3,a4=question.a4,correct=question.correct)
                    Logger.log('Quiz begin'+str(self.dbID))
                else:
                    h,p = Protocol.encode(Header.ERR, msg='Cant begin quiz')
                    Logger.log('Quiz request fail '+str(self.dbID))
            elif headerType == Header.NXT:
                if self.quiz != None:
                    question = self.quiz.next()
                    h,p = Protocol.encode(Header.QUE, question=question.question,a1=question.a1,a2=question.a2,a3=question.a3,a4=question.a4,correct=question.correct)
                    Logger.log('Quiz request fail '+str(self.dbID))
                else:
                    h,p = Protocol.encode(Header.ERR, msg='Invalid request')
                    Logger.log('Quiz request fail '+str(self.dbID))
            elif headerType == Header.END:
                if self.quiz != None:
                    r = requests.patch(URL.local+'stats', json={'userid':self.dbID,'quizid':self.quiz.quizid,'score':data['score']})

                    self.quiz = None

                    h,p = Protocol.encode(Header.ACK, msg='Quiz completed')
                    Logger.log('Quiz end '+str(self.dbID))
                else:
                    h,p = Protocol.encode(Header.ERR, msg='Invalid end request')
                    Logger.log('Quiz end request fail '+str(self.dbID))
            if h != None and p != None:
                self.transfer(h,p)
                
        return None

