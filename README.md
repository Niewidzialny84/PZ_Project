# PZ_Project

A school project of a quiz app where user can solve quizes made up from 10 questions from specified category. Users can also view stats for themselves and the top 10 of selected category.

The project contains a series of services and tools in corresponding folders, all of them have corresponding README file inside them:

- Admin tools - this folder contains a tool for adding, editing, deleting questions using special interface do database file, made using PyQt5 and SQLAlchemy in Python 3.9
- API - interface between database file and server for requesting special data run locally, made using Flask and SQLAlchemy frameworks in Python 3.9
- PHPClient - test grounds in PHP where the basic server functions were testes and later implemented in the desktop application
- QuizApp - a desktop application for user to solve quizzes and check stats, made in C# with .Net Core framework
- Server - a TCP server for handling connections incoming from client side, made using Python 3.9
