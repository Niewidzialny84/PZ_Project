using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class EndForm : Form
    {
       
        public EndForm()
        {
            InitializeComponent();
        }
        private string username;
        private string password;
        private string message;
        private int correctQuestions;
        private double time;
        public EndForm(string username, string password)
        {
            this.username = username;
            this.password = password;
            InitializeComponent();
        }
        public EndForm(string username, string password, string message, int correctQuestions, double timeleft)
        {
            this.message = message;
            this.username = username;
            this.password = password;
            this.correctQuestions = correctQuestions;
            this.time=timeleft;
           
            InitializeComponent();
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(username, password);
            this.Close() ;
            mainForm.Show();
        }

        private void EndForm_Load(object sender, EventArgs e)
        {
            this.messageLabel.Text = message;
            this.timeLeft.Text = "Pozostały czas:\n" + time.ToString();
            this.totalResult.Text = "Uzyskane punkty:\n" + countResult(correctQuestions,Convert.ToInt32(time)).ToString();
        }
        int countResult(int correctAnswers,int timeLeft) => (correctAnswers* 7 + 2 * (timeLeft / 5));

        private void messageLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
