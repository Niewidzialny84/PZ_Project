using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp
{
    public class Question
    {
        public string question { get; set; }
        public string[] answers { get; set; }
        public string correct { get; set; }
        public Question(string question, string A, string B, string C, string D)
        {
            this.answers = new string[4];
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
