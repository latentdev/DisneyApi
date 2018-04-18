using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ridetimes.Models
{
    public enum Parks {Disneyland = 1, CaliforniaAdventure = 2 };
    public enum RideType : int {Attraction = 1, Entertainment = 2 };

    public class Rootobject
    {
        public Links links { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
        public Entry[] entries { get; set; }
    }

    public class Links
    {
        public Themepark themePark { get; set; }
        public Self self { get; set; }
    }

    public class Themepark
    {
        public string href { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Entry
    {
        public Links1 links { get; set; }
        public string id { get; set; }
        [Display(Name = "Ride Name")]
        public string name { get; set; }
        public string type { get; set; }
        public WaitTime waitTime { get; set; }

        public Entry()
        {
            waitTime = new WaitTime();
        }
    }

    public class Links1
    {
        public Self1 self { get; set; }
        public Attractions attractions { get; set; }
        public Entertainments entertainments { get; set; }
    }

    public class Self1
    {
        public string href { get; set; }
    }

    public class Attractions
    {
        public string href { get; set; }
    }

    public class Entertainments
    {
        public string href { get; set; }
    }

    public class WaitTime
    {
        public Fastpass fastPass { get; set; }
        public string status { get; set; }
        public bool singleRider { get; set; }
        [Display(Name = "Wait Time")]
        public int postedWaitMinutes { get; set; }
        [Display(Name = "Status")]
        public string rollUpStatus { get; set; }
        public string rollUpWaitTimeMessage { get; set; }

        public WaitTime()
        {
            fastPass = new Fastpass();
        }
    }

    public class Fastpass
    {
        public bool available { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}