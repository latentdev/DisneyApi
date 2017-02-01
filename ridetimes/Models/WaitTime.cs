using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ridetimes.Models
{
    public class WaitTime
    {
        public FastPass fastPass { get; set; }
        public String status { get; set; }
        public Boolean singleRider { get; set; }
        public int postedWaitMinutes { get; set; }
        public string rollUpStatus { get; set; }
        public string rollUpWaitTimeMessage { get; set; }

        public WaitTime()
        {
            fastPass = new FastPass();
        }
    }
}