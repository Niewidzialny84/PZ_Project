from flask import Flask, request, jsonify, make_response
from flask_sqlalchemy import SQLAlchemy
from flask_marshmallow import Marshmallow
from flask_restful import Resource, Api
from sqlalchemy.orm import validates, Session
from marshmallow import fields, validate
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import relationship
import json, random
from apispec import APISpec
from apispec.ext.marshmallow import MarshmallowPlugin
from apispec_webframeworks.flask import FlaskPlugin

app = Flask(__name__) 
api = Api(app) 
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///pzDatabase.db' 
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

# Create an APISpec
spec = APISpec(
    title="Swagger Quiz",
    version="1.0.0",
    openapi_version="3.0.2",
    plugins=[FlaskPlugin(), MarshmallowPlugin()],
)

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

################### RESOURCES #######################

####### User Manager ##############
class UserManager(Resource):
    @staticmethod
    def get():
        try: username = request.args['username']
        except Exception as _: username = None

        if not username:
            users = User.query.all()
            return make_response(jsonify(users_schema.dump(users)), 200)
        elif User.query.filter_by(username = username).first() != None:
            user = User.query.filter_by(username = username).first()
            return make_response(jsonify(user_schema.dump(user)), 200)
        else:
            return make_response(jsonify({'Message': 'NOT FOUND'}), 404)
        
        return make_response(jsonify({'Message': 'BAD REQUEST'}), 400)

    @staticmethod
    def post():
        try:  
            username = request.json['username']
            password = request.json['password']
        except Exception as _:
            username = None
            password = None

        if (username and password) != None and User.query.filter_by(username = username).first() == None:
            user = User(username, password)
            db.session.add(user)
            db.session.commit()
            return make_response(jsonify({'Message': f'User {username} inserted.'}), 201)
        elif User.query.filter_by(username = username).first() != None:
            return make_response(jsonify({'Message': f'User {username} exist.'}), 409)
        else:
            return make_response(jsonify({'Message': 'BAD_REQUEST'}), 400)
                  
    @staticmethod
    def put():
        try: username = request.args['username']
        except Exception as _: username = None

        try:  
            username_new = request.json['username']
            password_new = request.json['password']
        except Exception as _:
            username_new = None
            password_new = None

        if not username or username_new == None or password_new == None:
            return make_response(jsonify({ 'Message': 'Must provide the proper data' }), 400)

        user = User.query.filter_by(username = username).first()

        if user == None:
            return make_response(jsonify({ 'Message': 'User not exist!' }), 404)

        user.password = password_new
        user.username = username_new

        db.session.commit()
        return make_response(jsonify({'Message': f'User {user.username} altered.'}), 200)

    @staticmethod    
    def patch():
        try:  username = request.args['username']
        except Exception as _: username = None
            
        try: username_new = request.json['username']
        except Exception as _: username_new = None

        try: password_new = request.json['password']
        except Exception as _: password_new = None

        if username == None:
            return make_response(jsonify({ 'Message': 'Must provide the proper username' }), 400)

        user = User.query.filter_by(username = username).first()

        if user == None:
            return make_response(jsonify({ 'Message': 'User not exist!' }), 404)

        if username_new != None:
            user.username = username_new

        if password_new != None:
            user.password = password_new 

        db.session.commit()
        return make_response(jsonify({'Message': f'User {user.username} altered.'}), 200)

    @staticmethod
    def delete():
        try: username = request.args['username']
        except Exception as _: username = None

        if not username:
            return make_response(jsonify({ 'Message': 'Must provide the user username' }), 400)

        user = User.query.filter_by(username = username).first()

        if user == None:
            return make_response(jsonify({ 'Message': 'User not exist!' }), 404)

        stats = Stats.query.filter_by(userid = user.id).all()
        
        db.session.delete(user)
        db.session.commit()

        if stats != None:
            for s in stats:
                db.session.delete(s)
                db.session.commit()

        return make_response(jsonify({'Message': f'User {username} deleted.'}), 200)

####### Quiz Manager ##############
class QuizManager(Resource):

    @staticmethod
    def get():
        try: category_name = request.args['category_name']
        except Exception as _: category_name = None

        if category_name != None:
            # result = db.session.query(Quiz, Questions, Answers).join(Questions, Questions.id == Quiz.id).join(Answers, Answers.questionid == Questions.id).filter_by(Quiz.category_name == category_name).all()
            if  Quiz.query.filter_by(category_name = category_name).first() != None:
                return make_response(jsonify(getQuiz(category_name)), 200)
            else:
                return make_response(jsonify({ 'Message': 'NOT FOUND' }), 404)
        else:
            return make_response(jsonify({ 'Message': 'BAD REQUEST' }), 400)

####### Quiz Category ##############
class QuizCategory(Resource):
    
    @staticmethod
    def get():
        quizzes = Quiz.query.all()
        return make_response(jsonify(quizzes_schema.dump(quizzes)), 200)

####### Stats Manager ##############
class StatsManager(Resource):
    
    @staticmethod
    def get():
        try: userid = request.args['userid']  
        except Exception as _: userid = None

        if userid != None:
            stats = Stats.query.filter_by(userid = userid).all()
            return make_response(jsonify(stats_schema.dump(stats)), 200)
        else:
            stats = Stats.query.all()
            return make_response(jsonify(users_schema.dump(stats)), 200)
        
        return make_response(jsonify({'Message': 'BAD REQUEST'}), 400)
                  
    @staticmethod
    def patch():
        try: 
            userid = request.json['userid']
            quizid = request.json['quizid']
            score = request.json['score']
        except Exception as _: 
            userid = None
            quizid = None
            score = None
            
        if (userid and quizid and score) != None:
            stat = Stats.query.filter_by(userid = userid, quizid = quizid).first()
            if stat == None:
                stat = Stats(userid, quizid, score)
                db.session.add(stat)
                db.session.commit()
            else:    
                if score > stat.score:
                    stat.score = score
                    db.session.commit()
            return make_response(jsonify({ 'Message': 'OK' }), 200)
        else:
            return make_response(jsonify({'Message': 'BAD REQUEST'}), 400)

####### Functions ##############

def getQuiz(category_name):
    quiz = Quiz.query.filter_by(category_name = category_name).first()
    questions = Questions.query.filter_by(quizid = quiz.id).all()
    result = []

    random.shuffle(questions)

    if len(questions) >= 10:
        questions = questions[:10]

    for question in questions:
        tmpDict = {}
        tmpDict['Question'] = question_schema.dump(question)
        answersArr = Answers.query.filter_by(questionid = question.id).all()
        random.shuffle(answersArr)
        tmpDict['Answers'] = answers_schema.dump(answersArr)
        result.append(tmpDict)
    return result

def getQuizCategoryNames():
    quizzes = Quiz.query.all()
    if quizzes != None:
        return [quiz.category_name for quiz in quizzes]
    else:
        return "[]"

####### Resource Mapping ##############

api.add_resource(UserManager, '/api/users')
api.add_resource(QuizManager, '/api/quizzes')
api.add_resource(QuizCategory, '/api/quizzes-categories')
api.add_resource(StatsManager, '/api/stats')

if __name__ == '__main__':
    app.run(debug=True)