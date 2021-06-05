using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DesktopApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics gradientg = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(0, 0, 0), 1);
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush lgb = new LinearGradientBrush(rect, Color.FromArgb(185, 26, 26), Color.FromArgb(66, 21, 191), LinearGradientMode.Vertical);
            gradientg.FillRectangle(lgb, rect);
            gradientg.DrawRectangle(pen, rect);

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (login(log_box.Text,pass_box.Text))
            {
                MainForm mainForm = new MainForm(log_box.Text, pass_box.Text);
                this.Hide();
                mainForm.Show();
                //
            }
        }
       //OBSŁUGA LOGOWANIA 
        private bool login(string username, string password)
        {

            return true;
        }

        private void top_lbl_Click(object sender, EventArgs e)
        {

        }
    }
}
