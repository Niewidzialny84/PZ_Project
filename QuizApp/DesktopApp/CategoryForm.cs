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
    public partial class CategoryForm : Form
    {
        private string username;
        private string password;
        public CategoryForm()
        {
            InitializeComponent();
        }
        public CategoryForm(string username, string password)
        {
            this.username = username;
            this.password = password;
            InitializeComponent();
        }

        private void catButton1_Click(object sender, EventArgs e)
        {
            QuestionForm questionForm = new QuestionForm(username, password);
            questionForm.Show();
            this.Close();
           
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(username, password);
            mainForm.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
