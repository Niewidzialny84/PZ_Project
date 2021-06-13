using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace DesktopApp
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if(loginBox.Text.Length>0 && passBox.Text==repPassBox.Text && passBox.Text.Length>0)
            {
                QuizClient quizClient = new QuizClient("127.0.0.1", 7777);
                quizClient.username = loginBox.Text;
                var stream = quizClient.Start();
                if (QuizClient.Register(loginBox.Text, passBox.Text,stream))
                {
                    QuizClient.Disconnect(stream);
                    stream.Close();
                    MessageBox.Show("Rejestracja przebiegła pomyślnie.");
                    GoBack();
                }
                else
                {
                    QuizClient.Disconnect(stream);
                    stream.Close();
                    MessageBox.Show("Wystąpił błąd. Sprawdź poprawnośc danych.");
                }
            }
            else
            {
                MessageBox.Show("Niepoprawne dane");
            }
        }
        private void GoBack()
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            GoBack();
        }
    }
}
