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
        private 
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

    }
}
