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

            //  GenerateDepartures genDep = new GenerateDepartures();
            var form = Session["From"] as String;
            var to = Session["To"] as String;
            var date = Session["Date"] as String;

            Random random = new Random();
            int antall = random.Next(8);
            string[] tider = GenerateDepartures.GenerateTimes(antall);

            List<Departure> departures = GenerateDepartures.CreateDepartures(form, to, date, tider);

            ViewData["Price"] = GenerateDepartures.GeneratePrice(antall);

            return View(departures);

        }
    }
}