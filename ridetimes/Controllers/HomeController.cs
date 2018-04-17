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
            Rides.CaliforniaAdventure = CurrentRides.Disneyland;

            ViewBag.DisneyLand = CurrentRides.Disneyland;
            ViewBag.CaliforniaAdventure = CurrentRides.CaliforniaAdventure;
            
            return View(Rides);
        }
    }

}
