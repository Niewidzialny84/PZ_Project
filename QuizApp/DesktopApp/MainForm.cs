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
            this.usernameLabel.Text = "Hello " + username;
        }
        public MainForm(string username,string password)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
            this.usernameLabel.Text = "Hello " + username;
        }
        private void logoutButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void playButton_Click(object sender, EventArgs e)
        {

        }

        private void statsButton_Click(object sender, EventArgs e)
        {

        }

        private void rankingButton_Click(object sender, EventArgs e)
        {

        }
    }
}
  