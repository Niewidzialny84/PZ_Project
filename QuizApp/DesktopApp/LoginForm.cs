using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DesktopApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        NetworkStream stream;
        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics gradientg = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(0, 0, 0), 1);
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush lgb = new LinearGradientBrush(rect, Color.FromArgb(185, 26, 26), Color.FromArgb(66, 21, 191), LinearGradientMode.Vertical);
            gradientg.FillRectangle(lgb, rect);
            gradientg.DrawRectangle(pen, rect);

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (login(log_box.Text,pass_box.Text))
            {
                MainForm mainForm = new MainForm(new User(log_box.Text,pass_box.Text),stream);
                this.Hide();
                mainForm.Show();
                
            }
            else
            {
                MessageBox.Show("Login unsuccessful");
            }
        }
       //OBSŁUGA LOGOWANIA 
        private bool login(string username, string password)
        {
            
            QuizClient quizClient = new QuizClient("127.0.0.1", 7777);
            quizClient.username = username;
            stream =quizClient.Start();

            if(quizClient.Login(password,stream))
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }
    }
}
