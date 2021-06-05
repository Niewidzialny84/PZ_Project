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
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(user,stream);
            mainForm.Show();
            this.Close();
        }
    }
}
