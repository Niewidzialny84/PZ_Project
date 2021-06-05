using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp
{
    public class User
    {
        public string login { get; set; }
        public string password { get; set; }

        public User(string username, string password)
        {
            this.login = username;
            this.password = password;
        }
        
    }
}
