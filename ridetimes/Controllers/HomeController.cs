using ridetimes.Helper;
using ridetimes.Models;
using ridetimes.ViewModel;
using System.Web.Mvc;

namespace ridetimes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var Rides = new RideTimesViewModel();
            Rides.DisneyLand = CurrentRides.Disneyland;
            Rides.CaliforniaAdventure = CurrentRides.CaliforniaAdventure;
            
            return View(Rides);
        }

        public ActionResult GetRide(string ride)
        {
            Entry entry = CurrentRides.Disneyland.Find(x => x.name.Equals(ride));
            if (entry == null)
            {
                entry = CurrentRides.CaliforniaAdventure.Find(x => x.name.Equals(ride));
            }

            return View(HTMLScraper.ScrapeWikipedia(entry));
        }
    }

}
