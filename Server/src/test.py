import socket, ssl

import threading

from protocol import Header,HeaderParser,Protocol

import time

conn = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
conn.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)

addr = ('127.0.0.1',7777)

conn.connect(addr)


def transfer(h,p):
    conn.send(h)
    conn.send(p)

def login(login: str, password: str): 
    h, p = Protocol.encode(Header.LOG,login=login,password=password)
    transfer(h,p)

def register(login: str, password: str):
    h, p = Protocol.encode(Header.REG,login=login,password=password)
    transfer(h,p)

def listRequest():
    h, p = Protocol.encode(Header.ALI,msg = 'give')
    transfer(h,p)

def statsRequest():
    h, p = Protocol.encode(Header.STR,msg = 'give')
    transfer(h,p)

def beginQuiz(category):
    h, p = Protocol.encode(Header.QUI,category=category)
    transfer(h,p)

def nextQueston():
    h, p = Protocol.encode(Header.NXT,msg='give')
    transfer(h,p)

def endQuiz(score):
    h,p = Protocol.encode(Header.END,score=score)
    transfer(h,p)

'''
register('Eryk','123')

headerType, size = HeaderParser.decode(conn.recv(3))
data = Protocol.decode(conn.recv(size))

if headerType == Header.ACK:
    print(data['msg'])
elif headerType == Header.ERR:
    print(data['msg'])
'''

session = None

_login = 'Eryk'
login(_login,'123')
    
headerType, size = HeaderParser.decode(conn.recv(3))
data = Protocol.decode(conn.recv(size))

if headerType == Header.SES:
    session = data['session']
    print(session)

    #statsRequest()
    beginQuiz('Sport')

    headerType, size = HeaderParser.decode(conn.recv(3))
    data = Protocol.decode(conn.recv(size))
    print(data)

    for x in range(9):
        nextQueston()
        headerType, size = HeaderParser.decode(conn.recv(3))
        data = Protocol.decode(conn.recv(size))
        print(data)

    endQuiz(888)
    headerType, size = HeaderParser.decode(conn.recv(3))
    data = Protocol.decode(conn.recv(size))
    print(data)

elif headerType == Header.ERR:
    print(data['msg'])
elif headerType == Header.ACK:
    print(data['msg'])


h, p = Protocol.encode(Header.DIS, msg='Disconnect')
transfer(h,p)

conn.close()

