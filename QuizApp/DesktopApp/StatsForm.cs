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
    public partial class StatsForm : Form
    {
        public StatsForm()
        {
            InitializeComponent();
        }
        private string username;
        private string password;
        public StatsForm(string username, string password)
        {
            this.username = username;
            this.password = password;
            InitializeComponent();
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(username, password);
            mainForm.Show();
            this.Close();
        }
    }
}
