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
    public partial class CategoryForm : Form
    {

        private User user;
        NetworkStream stream;
        List<Button> categories = new List<Button>();
        public CategoryForm()
        {
            InitializeComponent();
        }
        public CategoryForm(User user, NetworkStream stream)
        {
            this.user = user;
            this.stream = stream;
            InitializeComponent();

        }
        private List<Button> getCategories()
        {
            List<Button> myCategories = new List<Button>();
            return myCategories;
        }
        private void catButton1_Click(object sender, EventArgs e)
        {
            QuestionForm questionForm = new QuestionForm(user,stream);
            questionForm.Show();
            this.Close();
           
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(user,stream);
            mainForm.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
