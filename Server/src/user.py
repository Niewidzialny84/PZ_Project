from logger import Logger
from protocol import Header,HeaderParser,Protocol

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
                r = requests.post(URL.local+'users', data=json.dumps({'username':data['login'], 'password':self.passwordHash(data['password']).hex()}))
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
                r = requests.get(URL.local+'category')

                if r.status_code == 200:
                    j = r.json()
                    category = j['categories']
                    h,p = Protocol.encode(Header.LIS, category=category)
                    Logger.log('Category request')
                else:
                    Logger.log('Category request failed')

            if h != None and p != None:
                self.transfer(h,p)
                
        return None

