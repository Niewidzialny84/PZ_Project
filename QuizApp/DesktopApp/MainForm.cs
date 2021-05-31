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
    public partial class MainForm : Form
    {

        private string username;
        private string password;
        public MainForm()
        {
            InitializeComponent();
            this.username = "";
            this.password = "";
            this.usernameLabel.Text = "Witaj " + username;
        }
        public MainForm(string username,string password)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
            this.usernameLabel.Text = "Witaj " + username;
        }
        private void logoutButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm(username, password);
            categoryForm.Show();
            this.Close();
        }

        private void statsButton_Click(object sender, EventArgs e)
        {
            StatsForm statsForm = new StatsForm(username, password);
            statsForm.Show();
            this.Close();
        }

        private void rankingButton_Click(object sender, EventArgs e)
        {
            RankingForm rankingForm = new RankingForm(username, password);
            rankingForm.Show();
            this.Close();
        }

        private void usernameLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
  