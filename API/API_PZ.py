from flask import Flask, request, jsonify, make_response
from flask_sqlalchemy import SQLAlchemy 
from flask_marshmallow import Marshmallow
from flask_restful import Resource, Api
from sqlalchemy.orm import validates, Session
from marshmallow import fields, validate


app = Flask(__name__) 
api = Api(app) 
app.config["SQLALCHEMY_DATABASE_URI"] = "mysql+mysqlconnector://root:admin@localhost/pzdatabase"
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = True 
db = SQLAlchemy(app) 
ma = Marshmallow(app)

####### User ##############

class User(db.Model): 
    user  = db.Table('user', 
    db.Column('id', db.Integer, primary_key=True),
    db.Column('username', db.String(64), unique=True),
    db.Column('email', db.String(320)),
    db.Column('password', db.String(128)))

    def __init__(self, username, email, password):
        self.username = username
        self.email = email
        self.password = password

class UserSchema(ma.Schema):
    class Meta:
        fields = ('id', 'username', 'email', 'password')

user_schema = UserSchema() 
users_schema = UserSchema(many=True)

####### Stats ##############

class Stats(db.Model): 
    stats  = db.Table('stats', 
    db.Column('id', db.Integer, primary_key=True),
    db.Column('userid', db.Integer, db.ForeignKey('user.id')),
    db.Column('quizid', db.Integer, db.ForeignKey('quiz.id')),
    db.Column('score', db.Integer))

    def __init__(self, userid, quizid, score):
        self.userid = userid
        self.quizid = quizid
        self.score = score

class StatsSchema(ma.Schema):
    class Meta:
        fields = ('id', 'userid', 'quizid', 'score')

stat_schema = StatsSchema() 
stats_schema = StatsSchema(many=True)

####### Quiz ##############

class Quiz(db.Model): 
    quiz  = db.Table('quiz',
    db.Column('id', db.Integer, primary_key=True), 
    db.Column('name', db.String(200)),
    db.Column('categoryid', db.Integer, db.ForeignKey('category.id')),
    db.Column('difficultyid', db.Integer, db.ForeignKey('difficulty.id')),
    db.Column('timeperquestion', db.Integer))

    def __init__(self, name, categoryid, difficultyid, timeperquestion):
        self.name = name
        self.categoryid = categoryid
        self.difficultyid = difficultyid
        self.timeperquestion = timeperquestion

class QuizSchema(ma.Schema):
    class Meta:
        fields = ('id', 'name', 'categoryid', 'difficultyid', 'timeperquestion')

quiz_schema = QuizSchema() 
quizzes_schema = QuizSchema(many=True)

####### Difficulty ##############

class Difficulty(db.Model): 
    difficulty  = db.Table('difficulty',
    db.Column('id', db.Integer, primary_key=True), 
    db.Column('name', db.String(50)),
    db.Column('points', db.Integer))

    def __init__(self, name, points):
        self.name = name
        self.points = points

class DifficultySchema(ma.Schema):
    class Meta:
        fields = ('id', 'name', 'points')

difficulty_schema = DifficultySchema() 
difficulties_schema = DifficultySchema(many=True)

####### Category ##############

class Category(db.Model): 
    category  = db.Table('category',
    db.Column('id', db.Integer, primary_key=True), 
    db.Column('name', db.String(32)))

    def __init__(self, name):
        self.name = name

class CategorySchema(ma.Schema):
    class Meta:
        fields = ('id', 'name')

category_schema = CategorySchema() 
categories_schema = CategorySchema(many=True)

####### Questions ##############

class Questions(db.Model): 
    questions  = db.Table('questions',
    db.Column('id', db.Integer, primary_key=True), 
    db.Column('quizid', db.Integer, db.ForeignKey('quiz.id')),
    db.Column('question', db.String(200)),
    db.Column('correct_answer', db.Integer, db.ForeignKey('answers.id')))

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
    db.Column('id', db.Integer, primary_key=True), 
    db.Column('questionid', db.Integer, db.ForeignKey('questions.id')),
    db.Column('answer', db.String(200)))

    def __init__(self, questionid, answer):
        self.questionid = questionid
        self.answer = answer

class AnswersSchema(ma.Schema):
    class Meta:
        fields = ('id', 'questionid', 'answer')

answer_schema = AnswersSchema() 
answers_schema = AnswersSchema(many=True)


# if __name__ == '__main__':
#     app.run(debug=True)