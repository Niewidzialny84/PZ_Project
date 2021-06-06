using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace DesktopApp
{
    public partial class MainForm : Form
    {

        User user = new User();
        NetworkStream stream;
        public MainForm()
        {
            InitializeComponent();
            user.login = "";
            user.password = "";
            this.usernameLabel.Text = "Witaj " + user.login;
        }
        public MainForm(User user, NetworkStream stream)
        {
            InitializeComponent();
            this.user = user;
            this.usernameLabel.Text = "Witaj " + user.login;
            this.stream = stream;
        }
        private void logoutButton_Click(object sender, EventArgs e)
        {
            QuizClient.Disconnect(stream);
            Application.Restart();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm(user, stream);
            categoryForm.Show();
            this.Close();
        }

        private void statsButton_Click(object sender, EventArgs e)
        {
            StatsForm statsForm = new StatsForm(user,stream);
            statsForm.Show();
            this.Close();
        }

        private void rankingButton_Click(object sender, EventArgs e)
        {
            RankingForm rankingForm = new RankingForm(user,stream);
            rankingForm.Show();
            this.Close();
        }

        private void usernameLabel_Click(object sender, EventArgs e)
        {

        }
        private void Disconnect(object sender, FormClosingEventArgs e)
        {
            QuizClient.Disconnect(stream);
        }
    }
}
  