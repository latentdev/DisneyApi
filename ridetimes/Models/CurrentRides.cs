using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ridetimes.Models
{
    public class CurrentRides
    {
        //public static Parks parks;
        public static List<Entry> Disneyland { get; private set; }
        public static List<Entry> CaliforniaAdventure { get; private set; }

        private static CurrentRides instance;

        private CurrentRides()
        {
            //parks = Parks.Instance;
            //write fetch
            Disneyland = Helper.Ride.Fetch("330339").Result;
            CaliforniaAdventure = Helper.Ride.Fetch("336894").Result;
        }

        public static CurrentRides Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentRides();
                }
                return instance;
            }
        }

        public static void Update()
        {
            Disneyland = Helper.Ride.Fetch("330339").Result;
            CaliforniaAdventure = Helper.Ride.Fetch("336894").Result;
        }
    }
}