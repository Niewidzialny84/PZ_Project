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
    public partial class RankingForm : Form
    {
        public RankingForm()
        {
            InitializeComponent();
        }
        User user;
        NetworkStream stream;
        public RankingForm(User user, NetworkStream stream)
        {
            this.user = user;
            this.stream = stream;
            InitializeComponent();
            LoadData();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(user,stream);
            mainForm.Show();
            this.Close();
        }

        private void findButton_Click(object sender, EventArgs e)
        {

        }
        public void LoadData()
        {
            Categories categories = QuizClient.GetCategories(stream);
            comboBox1.Items.AddRange(categories.quizes);
        }

        private void myRankingButton_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var category = comboBox1.SelectedItem.ToString();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            //var category = comboBox1.SelectedIndex
            if (category.Length>0)
            {
                var table =QuizClient.GetRanking(stream, category);
                if(table.stats!=null)
                {
                    for (int i = 0; i < table.stats.Length; i++)
                    {

                        DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                        row.Cells[0].Value = table.stats[i].username;
                        row.Cells[1].Value = table.stats[i].score;
                        dataGridView1.Rows.Add(row);
                    }
                }
               
            }
            
        }
    }
}
