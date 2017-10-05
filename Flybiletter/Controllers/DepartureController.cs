using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flybiletter.Models;

namespace Flybiletter.Controllers
{
    public class DepartureController : Controller
    {
        // GET: Departure
        public ActionResult Index()
        {
            
            GenerateDepartures genDep = new GenerateDepartures();
            var form = Session["From"] as String;
            var to = Session["To"] as String;
            var date = Session["Date"] as String;

            List<Departure> departures = genDep.CreateDepartures(form, to, date);

                return View(departures);
          
        }
    }
}