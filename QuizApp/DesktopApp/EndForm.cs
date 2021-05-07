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
        public EndForm(string username, string password)
        {
            this.username = username;
            this.password = password;
            InitializeComponent();
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(username, password);
            this.Close() ;
            mainForm.Show();
        }
    }
}
