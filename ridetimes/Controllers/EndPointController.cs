using Flurl.Http;
using Newtonsoft.Json;
using ridetimes.Helper;
using ridetimes.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ridetimes.Controllers
{
    public class EndPointController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Test()
        {
            CurrentRides.Update();
            //Database.InsertRideTime(CurrentRides.Disneyland, DateTime.Now);
            return Json(0);
        }

        [HttpGet]
        public IHttpActionResult DisneyLandTimes()
        {
            return Json(CurrentRides.Disneyland);
        }

        [HttpGet]
        public IHttpActionResult CaliforniaAdventureTimes()
        {
            return Json(CurrentRides.CaliforniaAdventure);
        }
    }
}
