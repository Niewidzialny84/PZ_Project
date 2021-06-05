using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
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
        private User user;
        private string message;
        private int correctQuestions;
        private double time;
        NetworkStream stream;
        public EndForm(User user, NetworkStream stream)
        {
            this.user = user;
            InitializeComponent();
        }
        public EndForm(User user, string message, int correctQuestions, double timeleft, NetworkStream stream)
        {
            this.message = message;
            this.user = user;
            this.correctQuestions = correctQuestions;
            this.time=timeleft;
            this.stream = stream;
           
            InitializeComponent();
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(user,stream);
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
