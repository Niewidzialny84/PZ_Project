using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    public class GlobalStats

    {
      public UserStat[] stats { get; set; }
    }
    public class UserStat   
    {
        public string username { get; set; }
        public int score { get; set; }
    }
}
