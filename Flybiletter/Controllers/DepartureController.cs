using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flybiletter.Models;
using Flybiletter.ViewModels;

namespace Flybiletter.Controllers
{
    public class DepartureController : Controller
    {
        // GET: Departure
        public ActionResult Index()
        {

            //  GenerateDepartures genDep = new GenerateDepartures();
           

            var IndexObject = (IndexViewModel)Session["IndexObject"];
            var form = IndexObject.FromAirportID as String;
            var to = IndexObject.ToAirportID as String;
            var date = IndexObject.TravelDate as String;

            Random random = new Random();
            int antall = random.Next(8);
            string[] tider = GenerateDepartures.GenerateTimes(antall);

            List<Departure> departures = GenerateDepartures.CreateDepartures(form, to, date, tider);

            ViewData["Price"] = GenerateDepartures.GeneratePrice(antall);

            return View(departures);

        }
    }
}