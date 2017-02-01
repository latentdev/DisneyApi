using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ridetimes.Models
{
    public class FastPass
    {
        public String start { get; set; }
        public String end {get; set; }
        public bool available { get; set; }

        public FastPass ()
        {
            available = false;
        }
    }
}