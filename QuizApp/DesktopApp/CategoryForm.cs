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
using Protocol;

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
            LoadObjects();
            getCategories();

        }
        private void LoadObjects()
        {
            categories.Add(catButton1);
            categories.Add(catButton2);
            categories.Add(catButton3);
            categories.Add(catButton4);
            categories.Add(catButton5);
            categories.Add(catButton6);
            categories.Add(catButton7);
            categories.Add(catButton8);
            categories.Add(catButton9);
        }
        private void getCategories()
        {
            Categories temp = QuizClient.GetCategories(stream);
            for(int i=0;i<9;i++)
            {
                if(i<temp.quizes.Count())
                {
                    categories[i].Text = temp.quizes[i];
                }
                else
                {
                    categories[i].Visible = false;
                }
            }

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

    }
}
