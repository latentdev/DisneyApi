using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ridetimes.Models
{
    public class Ride
    {
        public int id { get; set; }
        public String name { get; set; }
        public String type { get; set; }
        public WaitTime waitTime { get; set; }

        public Ride ()
        {
            waitTime = new WaitTime();
        }
    }
}