using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ridetimes.ViewModel
{
    public class RideViewModel
    {
        public String Name { get; set; }
        public int WaitTime { get; set; }
        public String Status { get; set; }
        public String Description { get; set; }
        public List<String> History { get; set; }

    }
}