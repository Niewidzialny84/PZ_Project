using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;

namespace DesktopApp
{
    public partial class QuestionForm : Form
    {
        public QuestionForm()
        {
            InitializeComponent();

        }
        List<Question> questions = new List<Question>();

        User user;
        private string correctAnswer;
        NetworkStream stream;
        ThreadStart timerThreadStart;
        Thread timerThread;
        int answeredQuestions, correctAnswers;
        double timer = 100.0;
        //
        public delegate void delUpdateUITextBox(string text);
        public QuestionForm(User user,NetworkStream stream)
        {
            this.user = user;
            this.stream = stream;
            InitializeComponent();
            timerThreadStart = new ThreadStart(CountDown);
         
            timerThread = new Thread(timerThreadStart);
            timerThread.Start();
            timerThread.IsBackground = true;
            answeredQuestions = correctAnswers = 0;
            GetQuestions();
            correctAnswer=LoadQuestion();
            numberLabel.Text = answeredQuestions+1 + " z 10";
            
        }

        private void resignButton_Click(object sender, EventArgs e)
        {
            timer = 0.0;
            MainForm mainForm = new MainForm(user,stream);
            
            mainForm.Show();
            this.Close();
        }
        private void CountDown()
        {
            while (timer > 0.0)
            {
                try
                {
                    Thread.Sleep(100);
                    timer -= 0.1;
                    // timer -= 1;
                    delUpdateUITextBox DelUpdateUITextBox = new delUpdateUITextBox(UpdateClock);
                    this.timeLabel.BeginInvoke(DelUpdateUITextBox, Math.Round(timer, 1).ToString());
                }
                catch (Exception e) { } 
            }
            EndForm endForm = new EndForm(user,"Czas się skończył!\n"+ "Odpowiedziałęś na "+ correctAnswers.ToString()+ " z 10 pytań poprawnie!", correctAnswers,0.0,stream);

            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                
                    endForm.Show();
                    this.Close();
                });
            }
            catch(Exception e)
            { }
           
            //var delUpdateBox = new delUpdateUITextBox(UpdateClock);
        }
        private void UpdateClock(string time)
        {
            this.timeLabel.Text = "Pozostały czas: " + time + " sekund";
        }

        private void aButton_Click(object sender, EventArgs e)
        {
           Answer(aButton);
        }

        private void bButton_Click(object sender, EventArgs e)
        {
            Answer(bButton);
        }

        private void cButton_Click(object sender, EventArgs e)
        {
            Answer(cButton);
        }

        private void dButton_Click(object sender, EventArgs e)
        {
            Answer(dButton);
        }
        //SPRAWDZANIE ODPOWIEDZI
        
        private void Answer(Button button)
        {
            answeredQuestions++;
            aButton.Enabled = false;
            bButton.Enabled = false;
            cButton.Enabled = false;
            dButton.Enabled = false;

            button.Enabled = true;

            if (button.Text==correctAnswer)
            {
                button.BackColor = Color.Green;
                correctAnswers++;
            }
            else
            {       
                button.BackColor = Color.Red;

            }
            button.Update();
            Thread.Sleep(20);
            Thread.Sleep(2000);
            string timeLeft = timeLabel.Text.Replace("Pozostały czas: ", "").Replace(" sekund","");
            timer = Convert.ToDouble(timeLeft);


            aButton.Enabled = true;
            bButton.Enabled = true;
            cButton.Enabled = true;
            dButton.Enabled = true;
            if (answeredQuestions == 10)
            {
                 timeLeft = timeLabel.Text;
                
                //timerThread.Abort();
                EndForm endForm = new EndForm(user,"Odpowiedziano na "+ correctAnswers.ToString()+ " z 10 pytań poprawnie!", correctAnswers,timer,stream);
                endForm.Show();
                this.Close();
            }
            else
            {

               correctAnswer= LoadQuestion();
            }


        }
        private void GetQuestions()
        {
            questions.Clear();
            Question question = new Question("Ile mam rąk", "2", "3", "4", "5");
            questions.Add(question);
            question = new Question("Ile mam włosów", "Duuuużo", "3", "4", "5");
            questions.Add(question);
            question = new Question("Ile mam nóg", "2", "3", "4", "5");
            questions.Add(question);
            question = new Question("Ile mam oczu", "2", "3", "4", "5");
            questions.Add(question);
            question = new Question("Ile mam uszu", "2", "3", "4", "5");
            questions.Add(question);
            question = new Question("Ile mam dłoni", "2", "3", "4", "5");
            questions.Add(question);
            question = new Question("Ile mam stóp", "2", "3", "4", "5");
            questions.Add(question);
            
            question = new Question("Ile mam nosów", "1", "3", "4", "5");
            questions.Add(question);
            question = new Question("Ile mam kolan", "2", "3", "4", "5");
            questions.Add(question);
            question = new Question("Dzik jest dziki, dzik jest... ", "zły", "duży", "fajny", "niefajny");
            questions.Add(question);
        }

        private void timeLabel_Click(object sender, EventArgs e)
        {

        }

        private string LoadQuestion()
        {
            numberLabel.Text = answeredQuestions+1 + " z 10";
            Question question = questions[answeredQuestions];
            string[] answers = question.GetAnswers();
            string answer = answers[0];
            Random rnd = new Random();
            answers = answers.OrderBy(x => rnd.Next()).ToArray();
            questionLabel.Text = question.GetQuestion();
            aButton.Text = answers[0];
            bButton.Text = answers[1];
            cButton.Text = answers[2];
            dButton.Text = answers[3];
            aButton.BackColor =  default(Color);
            bButton.BackColor =  default(Color);
            cButton.BackColor =  default(Color);
            dButton.BackColor = default(Color);
            return answer;
        }
    }
}
