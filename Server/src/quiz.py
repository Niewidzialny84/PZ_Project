class Question(object):
    def __init__(self,question,a1,a2,a3,a4,correct):
        super().__init__()
        self.question = question
        self.a1 = a1
        self.a2 = a2
        self.a3 = a3
        self.a4 = a4
        self.correct = correct
        

class Quiz(object):
    def __init__(self, questions, quizid):
        super().__init__()
        self.iter = 0
        self.quizid = quizid
        self.questions = questions

    def next(self):
        ret = self.questions[self.iter]
        self.iter += 1
        return ret

