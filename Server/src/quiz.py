class Question(object):
    def __init__(self,question,answers,correct):
        super().__init__()
        self.question = question
        self.answers = answers
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

