from flask import Flask, request, jsonify, make_response
from flask_sqlalchemy import SQLAlchemy
from flask_marshmallow import Marshmallow
from flask_restful import Resource, Api
from sqlalchemy.orm import validates, Session
from marshmallow import fields, validate
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import relationship
import json, random, re

app = Flask(__name__) 
api = Api(app) 
# app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///pzDatabase.db' 
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = True 
db = SQLAlchemy(app) 
ma = Marshmallow(app)

####### User ##############

class User(db.Model): 
    user  = db.Table('user',
    db.Column('id', db.Integer, primary_key=True),
    db.Column('username', db.String(64), unique=True),
    db.Column('password', db.String(128)),
    sqlite_autoincrement=True)

    def __init__(self, username, password):
        self.username = username
        self.password = password

class UserSchema(ma.Schema):
    class Meta:
        fields = ('id', 'username', 'password')
        session = db.Session

user_schema = UserSchema() 
users_schema = UserSchema(many=True)

####### Stats ##############

class Stats(db.Model): 
    stats  = db.Table('stats',
    db.Column('id', db.Integer, primary_key=True),
    db.Column('userid', db.Integer, db.ForeignKey('user.id')),
    db.Column('quizid', db.Integer, db.ForeignKey('quiz.id')),
    db.Column('score', db.Integer),
    sqlite_autoincrement=True)

    def __init__(self, userid, quizid, score):
        self.userid = userid
        self.quizid = quizid
        self.score = score

class StatsSchema(ma.Schema):
    class Meta:
        fields = ('id', 'userid', 'quizid', 'score')
        session = db.Session

stat_schema = StatsSchema() 
stats_schema = StatsSchema(many=True)

####### Quiz ##############

class Quiz(db.Model): 
    quiz  = db.Table('quiz',
    db.Column('id', db.Integer, primary_key=True),
    db.Column('category_name', db.String(200)),
    sqlite_autoincrement=True)

    questions = relationship("Questions", primaryjoin="and_(Quiz.id == Questions.quizid)", backref=db.backref("quiz", lazy="joined"))

    def __init__(self, category_name):
        self.category_name = category_name
class QuizSchema(ma.Schema):
    class Meta:
        fields = ('id', 'category_name')

quiz_schema = QuizSchema() 
quizzes_schema = QuizSchema(many=True)

####### Questions ##############

class Questions(db.Model): 
    questions  = db.Table('questions',
    db.Column('id', db.Integer, primary_key=True),
    db.Column('quizid', db.Integer, db.ForeignKey('quiz.id')),
    db.Column('question', db.String(200)),
    db.Column('correct_answer', db.Integer, db.ForeignKey('answers.id')),
    sqlite_autoincrement=True)

    answers = relationship("Answers", primaryjoin="and_(Questions.id == Answers.questionid)", backref=db.backref("questions", lazy="joined"))

    def __init__(self, quizid, question, correct_answer):
        self.quizid = quizid
        self.question = question
        self.correct_answer = correct_answer

class QuestionsSchema(ma.Schema):
    class Meta:
        fields = ('id', 'quizid', 'question', 'correct_answer')

question_schema = QuestionsSchema() 
questions_schema = QuestionsSchema(many=True)

####### Answers ##############

class Answers(db.Model): 
    answers  = db.Table('answers',
    db.Column('id', db.Integer, primary_key=True, autoincrement=True), 
    db.Column('questionid', db.Integer, db.ForeignKey('questions.id')),
    db.Column('answer', db.String(200)),
    sqlite_autoincrement=True)

    def __init__(self, questionid, answer):
        self.questionid = questionid
        self.answer = answer

class AnswersSchema(ma.Schema):
    class Meta:
        fields = ('id', 'questionid', 'answer')
        session = db.Session

answer_schema = AnswersSchema() 
answers_schema = AnswersSchema(many=True)

##############################
####### Methods ##############

class DBManager():
    def __init__(self):
        self.quizArr = []
        self.questionsArr = []
        self.answerArr = []
        self.mergedDbContent = []
        self.matchedData = []

######### Set db path #########
    def setDbPath(self, path):
        try:
            app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///' + path 
            return True
        except:
            return False
######### Pattern part #########    
    def filterData(self, pattern):
        self.matchedData.clear()
        for d in self.mergedDbContent:
            if re.match('.*('+ pattern +').*', d[2]) != None:
                self.matchedData.append(d)

######### Merge elems into arr of arr presented in type like in data readed from excel #########    

    def mergeDBcontent(self):
        for q in self.questionsArr:
            tmp = []
            tmp.append(q.id)
            tmp.append(self.getCategoryNameById(q.quizid))
            tmp.append(q.question)
            for a in self.answerArr:
                if a.questionid == q.id:
                    tmp.append(a.answer)
            self.mergedDbContent.append(tmp)
        print(self.mergedDbContent)

######### Auxiliary functions #########

    def getCategoryNameById(self, categoryId):
        for q in self.quizArr:
            if q.id == categoryId:
                return q.category_name 
    
    def getQuizIdByName(self, category_name):
        for q in self.quizArr:
            if q.category_name == category_name:
                return q.id 

######### Getting DB elements #########
    def getDBContent(self):
        with app.app_context():
            self.answerArr.clear()
            self.questionsArr.clear()
            self.quizArr.clear()
            self.mergedDbContent.clear()

            self.getQuizzes()
            self.getQuestions()
            self.getAnswers()
    
    def getQuizzes(self):
        with app.app_context():
            quizzes = Quiz.query.all()
            self.quizArr = quizzes
    
    def getQuestions(self):
        with app.app_context():
            questions = Questions.query.all()
            self.questionsArr = questions

    def getAnswers(self):
        with app.app_context():
            answers = Answers.query.all()
            self.answerArr += answers

######### CRUD operations #########
    def addToDB(self, params):
        quiz = Quiz.query.filter_by(category_name = params[0]).first()
        if quiz != None and "" not in params:
            self.add(quiz, params)
            return True
        elif quiz == None and "" not in params:
            db.session.add(Quiz(params[0]))
            db.session.commit()
            quiz = Quiz.query.filter_by(category_name = params[0]).first()
            self.add(quiz, params)
            return True
        else:
            return False
           
    def add(self, quiz, params):
        db.session.add(Questions(quiz.id, params[1], None))
        db.session.commit()
        questionId = Questions.query.filter_by(quizid = quiz.id, question = params[1]).first().id
        for i in range(2, 6):
            db.session.add(Answers(questionId, params[i]))
            db.session.commit()
        correctId = Answers.query.filter_by(questionid = questionId, answer = params[2]).first().id
        question = Questions.query.filter_by(quizid = quiz.id, question = params[1]).first()
        question.correct_answer = correctId
        db.session.commit()

    def updateDB(self, params):
        if params[0] != "":
            params[0] = int(params[0])
        if params in self.mergedDbContent or "" in params:
            print("UPDATE FALSE")
            return False
        else:
            question = Questions.query.filter_by(id = params[0]).first()
            answers = Answers.query.filter_by(questionid = question.id).all()
            question.question = params[2]
            db.session.commit()
            j = 3
            for answer in answers:
                answer.answer = params[j]
                db.session.commit()
                j += 1
            return True
    
    def removeFromDb(self, params):
        if params[0] != "":
            params[0] = int(params[0])
        db.session.execute('pragma foreign_keys=OFF')
        db.session.commit()

        if params in self.mergedDbContent and "" not in params:
            print("DELETE")
            quiz = Quiz.query.filter_by(category_name = params[1]).first()
            question = Questions.query.filter_by(question = params[2]).first()
            self.deleteAnswer(question.id)
            db.session.delete(question)
            db.session.commit()
            
            print(Questions.query.filter_by(quizid = quiz.id).all())
            if Questions.query.filter_by(quizid = quiz.id).all() == []:
                print("Usuwanie quizu")
                self.deleteStats(quiz.id)
                quiz = Quiz.query.filter_by(category_name = params[1]).first()
                db.session.delete(quiz)
                db.session.commit()
            db.session.execute('pragma foreign_keys=ON')
            db.session.commit()
            return True
        else:
            db.session.execute('pragma foreign_keys=ON')
            db.session.commit()
            return False
      
    def deleteAnswer(self, questionId):
        answers = Answers.query.filter_by(questionid = questionId).all()                 
        for a in answers:
            db.session.delete(a)
            db.session.commit()      
    
    def deleteStats(self, quizId):
        stats = Stats.query.filter_by(quizid = quizId).all()
        for s in stats:
            db.session.delete(s)
            db.session.commit()
    