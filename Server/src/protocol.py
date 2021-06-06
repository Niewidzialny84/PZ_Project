from enum import Enum
import json

class ProtocolVersion(Enum):
    V1 = 1

class Header(Enum):
    '''Enum of avaible headers for package with amount of arguments'''

    ACK = 0 #Acknowledment 
    ERR = 1 #Any type error 
    DIS = 2 #Disconnection 
    LOG = 3 #Login 
    SES = 4 #Session 
    REG = 5 #Register 
    LIS = 6 #Get Quiz list
    ALI = 7 #List request
    QUI = 8 #Selected/Begin Quiz
    QUE = 9 #Quiz question
    NXT = 10 #Question ask
    END = 11 #Quiz End
    STA = 12 #Personal Stats
    STR = 13 #Pesonal Stats request

    '''
    | Version  | Type   | Payload Size |
    |----------|--------|--------------|
    | 2 bits   | 6 bits | 16 bits      |

    Header is 3 bytes long
    '''


class HeaderParser(object):
    @staticmethod
    def encode(header: Header, size: int):
        '''Header creator for internal protocol use'''

        version = str(format(ProtocolVersion.V1.value,'b').zfill(2)) 
        headerType = str(format(header.value,'b').zfill(6)) 

        byte1 = format(int((version+headerType),2),'x').zfill(2) 

        payloadSize = str(format(size,'x').zfill(4)) 
        
        return bytes.fromhex(byte1 + payloadSize)

    @staticmethod
    def decode(headerBytes):
        '''Header decoder for to know how more bytes are needed to read'''
        
        headerBytes = headerBytes.hex()

        byte1 = format(int(headerBytes[0:2],16),'b').zfill(8)
        version = int(byte1[0:2],2)
        headerType = Header(int(byte1[2:8],2))

        if version != ProtocolVersion.V1.value:
            raise ValueError('Invalid version')

        payloadSize = int(headerBytes[2:6],16)   

        return headerType,payloadSize

class Protocol(object):
    @staticmethod
    def encode(headerType: Header, **kwargs):
        '''Encoding data to return a specified header and data to be send'''

        data = ''
        msg = kwargs.get('msg',None)
        login = kwargs.get('login',None)
        password = kwargs.get('password',None)

        if headerType == Header.ACK or headerType == Header.ERR or headerType == Header.DIS or headerType == Header.NXT or headerType == Header.ALI or headerType == Header.STR:
            data = {'msg': msg}
        elif headerType == Header.LOG:
            if login != None or password != None:
                data = {'login': login, 'password': password}
            else:
                raise TypeError('-LOG- Missing login or password')
        elif headerType == Header.REG:
            if login != None or password != None:
                data = {'login': login, 'password': password}
            else:
                raise TypeError('-REG- Missing login or password')
        elif headerType == Header.SES:
            session = kwargs.get('session',None)
            if session != None:
                data = {'session': session}
            else:
                raise TypeError('-SES- Missiong session id')       
        elif headerType == Header.LIS:
            quizes = kwargs.get('quizes',[])
            if quizes != []:
                data = {'quizes': quizes}
            else:
                raise TypeError('-LIS- Missing quiz')
        elif headerType == Header.QUI:
            category = kwargs.get('category',None)
            if category != None:
                data = {'category': category}
            else:
                raise TypeError('-QUI- Missing category')
        elif headerType == Header.QUE:
            question = kwargs.get('question',None)
            a1 = kwargs.get('a1',None)
            a2 = kwargs.get('a2',None)
            a3 = kwargs.get('a3',None)
            a4 = kwargs.get('a4',None)
            correct = kwargs.get('correct',None)
            if question != None and a1 != None and a2 != None and a3 != None and a4 != None and correct != None:
                data = {'question':question,'a1':a1,'a2':a2,'a3':a3,'a4':a4,'correct':correct}
            else:
                raise TypeError('-QUE- Missing data')
        elif headerType == Header.END:
            score = kwargs.get('score',None)
            if score != None:
                data = {'score':score}
            else:
                raise TypeError('-END- Missing score')
        elif headerType == Header.STA:
            stats = kwargs.get('stats', [])
            if stats != []:
                data = {'stats':stats}
            else:
                raise TypeError('-STA- Missing stats')

        encodedData = json.dumps(data).encode()
        header = HeaderParser.encode(headerType,len(encodedData))

        return header,encodedData

    @staticmethod
    def decode(data: str):
        '''Decoding data into dict array which should be used based on header'''

        return json.loads(data.decode())

