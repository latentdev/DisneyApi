using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Flurl;
using Flurl.Http;
using ridetimes.Models;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Threading.Tasks;
using ridetimes.Helper;

namespace ridetimes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            
            return View(Auth_Token.Instance.rides);
        }

        public void FillTable()
        {
            var token = Auth_Token.Instance;
           // Control control = FindControl("table1");
            //Table table = (Table)FindControl("table1");
            if (token.rides!= null)
            {
                foreach (Ride ride in token.rides)
                {
                    TableRow row = new TableRow();
                    TableCell name = new TableCell();
                    name.Text = ride.name;
                    TableCell time = new TableCell();
                    time.Text = ride.waitTime.postedWaitMinutes.ToString();
                    TableCell status = new TableCell();
                    status.Text = ride.waitTime.rollUpStatus;
                    row.Cells.Add(name);
                    row.Cells.Add(time);
                    row.Cells.Add(status);
                    //table.Rows.Add(row);
                }
            }
        }
    }

}
