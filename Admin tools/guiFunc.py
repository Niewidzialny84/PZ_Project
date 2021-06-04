import sys, path, os, asyncio, threading, socket, time
from cmds import cmds

sys.path.append(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))

from PyQt5 import QtCore, QtGui, QtWidgets 
from PyQt5.QtWidgets import QFileDialog
from PyQt5.QtCore import pyqtSlot
from dbModels import *

def handleSelectDb(self):
        file_filter = 'Database File (*.db)'
        response = QFileDialog.getOpenFileName(
            caption='Select a data file',
            directory=os.getcwd(),
            filter=file_filter,
            initialFilter='Database File (*.db)'
        )
       
        if response[0] != "":
            self.dbPath = response[0]
            self.locationField.setText(response[0])
            print("RESPONSE: " + response[0])
        if self.dbPath != "": 
            self.refreshTableContent()
            self.clearUpdateFields()

def clearUpdateFields(self):
    self.label_idContent.setText("")
    self.label_categoryContent.setText("")
    self.field_question.setText("")
    self.field_correct.setText("")
    self.field_answer2.setText("")
    self.field_answer3.setText("")
    self.field_answer4.setText("")
    self.label_update.hide()
    self.label_create.hide()

def handleGetBtn(self):
    if self.dbPath != "": 
        self.refreshTableContent()
        
def handleTableSelection(self):
    if self.dbTable.selectionModel().hasSelection():
        row = self.dbTable.currentRow()
        self.label_idContent.setText(self.dbTable.item(row, 0).text())
        self.label_categoryContent.setText(self.dbTable.item(row, 1).text())
        self.field_question.setText(self.dbTable.item(row, 2).text())
        self.field_correct.setText(self.dbTable.item(row, 3).text())
        self.field_answer2.setText(self.dbTable.item(row, 4).text())
        self.field_answer3.setText(self.dbTable.item(row, 5).text())
        self.field_answer4.setText(self.dbTable.item(row, 6).text())
        for i in range(0, 7):
            self.dbTable.item(row, i).setSelected(True)

def handleFindBtn(self):
    if self.dbPath != "": 
       self.filterTable()

def filterTable(self):
    self.dbManager.setDbPath(self.dbPath)
    self.dbManager.getDBContent()
    self.dbManager.mergeDBcontent()
    self.dbManager.filterData(self.findField.text())
    self.dbTable.setRowCount(0)
    self.dbTable.setRowCount(len(self.dbManager.matchedData))
    for i in range(0, len(self.dbManager.matchedData)):
        for j, item in enumerate(self.dbManager.matchedData[i]):
            self.dbTable.setItem(i, j, QtWidgets.QTableWidgetItem(str(item)))
            if i % 2 == 0:
                self.dbTable.item(i, j).setBackground(QtGui.QColor(230, 241, 255))
    self.dbTable.move(0,0)

def refreshTableContent(self):
    self.dbManager.setDbPath(self.dbPath)
    self.dbManager.getDBContent()
    self.dbManager.mergeDBcontent()
    self.dbTable.setRowCount(0)
    self.dbTable.setRowCount(len(self.dbManager.mergedDbContent))
    for i in range(0, len(self.dbManager.mergedDbContent)):
        for j, item in enumerate(self.dbManager.mergedDbContent[i]):
            self.dbTable.setItem(i, j, QtWidgets.QTableWidgetItem(str(item)))
            if i % 2 == 0:
                self.dbTable.item(i, j).setBackground(QtGui.QColor(230, 241, 255))
    self.dbTable.move(0,0)

def handleAddBtn(self):
    paramsArr = []
    paramsArr.append(self.field_categoryA.text())
    paramsArr.append(self.field_questionA.toPlainText())
    paramsArr.append(self.field_correctA.text())
    paramsArr.append(self.field_answer2A.text())
    paramsArr.append(self.field_answer3A.text())
    paramsArr.append(self.field_answer4A.text())

    if self.dbPath != "": 
        if self.dbManager.addToDB(paramsArr):
            self.label_create.setText("CREATED")
            self.label_create.show()
            self.field_categoryA.setText("")
            self.field_questionA.setText("")
            self.field_correctA.setText("")
            self.field_answer2A.setText("")
            self.field_answer3A.setText("")
            self.field_answer4A.setText("")
            self.refreshTableContent()
        else:
            self.label_create.setText("FAILED")
            self.label_create.show()
    else:
        self.label_create.setText("FAILED")
        self.label_create.show()

def handleUpdateBtn(self):
    paramsArr = []
    paramsArr.append(self.label_idContent.text())
    paramsArr.append(self.label_categoryContent.text())
    paramsArr.append(self.field_question.toPlainText())
    paramsArr.append(self.field_correct.text())
    paramsArr.append(self.field_answer2.text())
    paramsArr.append(self.field_answer3.text())
    paramsArr.append(self.field_answer4.text())

    if self.dbPath != "": 
        if self.dbManager.updateDB(paramsArr):
            self.label_update.show()
            self.label_update.setText("ALTERED")
            self.label_idContent.setText("")
            self.label_categoryContent.setText("")
            self.field_question.setText("")
            self.field_correct.setText("")
            self.field_answer2.setText("")
            self.field_answer3.setText("")
            self.field_answer4.setText("")
            self.refreshTableContent()
        else:
            self.label_update.show()
            self.label_update.setText("FAILED")
    else:
        self.label_update.show()
        self.label_update.setText("FAILED")

def handleDeleteBtn(self):
    paramsArr = []
    paramsArr.append(self.label_idContent.text())
    paramsArr.append(self.label_categoryContent.text())
    paramsArr.append(self.field_question.toPlainText())
    paramsArr.append(self.field_correct.text())
    paramsArr.append(self.field_answer2.text())
    paramsArr.append(self.field_answer3.text())
    paramsArr.append(self.field_answer4.text())

    if self.dbPath != "": 
        print("DELETE")
        if self.dbManager.removeFromDb(paramsArr):
            self.label_update.show()
            self.label_update.setText("DELETED")
            self.label_idContent.setText("")
            self.label_categoryContent.setText("")
            self.field_question.setText("")
            self.field_correct.setText("")
            self.field_answer2.setText("")
            self.field_answer3.setText("")
            self.field_answer4.setText("")
            self.refreshTableContent()
        else:
            self.label_update.show()
            self.label_update.setText("FAILED")
    else:
        self.label_update.show()
        self.label_update.setText("FAILED")

