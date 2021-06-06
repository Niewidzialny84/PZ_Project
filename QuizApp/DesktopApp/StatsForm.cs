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
    public partial class StatsForm : Form
    {
        public StatsForm()
        {
            InitializeComponent();
        }
        private User user;
        private NetworkStream stream;
        public StatsForm(User user, NetworkStream stream)
        {
            this.user = user;
            this.stream = stream;
            InitializeComponent();
            loadData();
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(user,stream);
            mainForm.Show();
            this.Close();
        }
        private void loadData()
        {
            PersonalStats personalStats= QuizClient.GetPersonalStats(stream);
           
            for(int i =0; i<personalStats.stats.Length;i++)
            {
                
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = personalStats.stats[i].category;
                row.Cells[1].Value = personalStats.stats[i].score;
                dataGridView1.Rows.Add(row);
            }
        }

    }
}
