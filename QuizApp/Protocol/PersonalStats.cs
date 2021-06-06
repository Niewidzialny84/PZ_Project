using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    public class PersonalStats
    {
      public Stat[] stats { get; set; }
    }
    public class Stat   
    {
        public string category { get; set; }
        public int score { get; set; }
    }
}
