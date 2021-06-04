using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp
{
    class Question
    {
        private string question;
        private string[] answers= new string[4];
        public Question(string question, string A, string B, string C, string D)
        {
            this.answers[0] = A;
            this.answers[1] = B;
            this.answers[2] = C;
            this.answers[3] = D;
            this.question = question;
        }

        public string[] GetAnswers()
        {
            return answers;
        }
        public string GetQuestion()
        {
            return question;
        }

            
        
    }
}
