using Flybiletter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flybiletter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new DB();
            var IndexVM = new ViewModels.IndexViewModel();
            IndexVM.FromAirport = db.getAllAirports();
            IndexVM.ToAirport = db.getAllAirports();

            return View(IndexVM);
        }

        [HttpPost]
        public ActionResult Index(ViewModels.IndexViewModel indexView)
        {
            if (ModelState.IsValid)
            {

                if ((indexView.ToAirportID).Equals(indexView.FromAirportID))
                {
                    ModelState.AddModelError("ToAirportID", "Destinasjon og avreise må være forskjellig");
                    ModelState.AddModelError("FromAirportID", "Destinasjon og avreise må være forskjellig");
                    return Index();
                }

                Session["IndexObject"] = indexView;
                return RedirectToAction("FlightDetails");
            }

            ModelState.AddModelError("TravelDate", "Noe gikk feil, vennsligst prøv igjen");
            return RedirectToAction("Index");
        }

      
        public ActionResult FlightDetails()
        {
            var model = (ViewModels.IndexViewModel)Session["IndexObject"];
            string test = Convert.ToString(Session["travelDate"]);

            ViewData["ToAirport"] = Convert.ToString(model.ToAirportID);
            return View();
        }
      

        public ActionResult Passenger()
        {
            /*TODO VALIDERING
            if (ModelState.IsValid)
            {
                return RedirectToAction("");
            }*/
            return View();

        }

        [HttpPost]
        public ActionResult Confirmation()
        {
            return View();
        }

        public ActionResult OrderTest2()
        {
            return View();
        }

        public ActionResult ViewModelTest()
        {
            var db = new DB();
            var IndexVM = new ViewModels.IndexViewModel();
            IndexVM.FromAirport = db.getAllAirports();
            IndexVM.ToAirport = db.getAllAirports();
           
            return View(IndexVM);
        }

        [HttpPost]
        public ActionResult ViewModelTest(ViewModels.IndexViewModel indexView)
        {
            /*
           string airportFrom = Convert.ToString(form["fromAirport"]);
           string airportTo = Convert.ToString(form["toAirport"]);
           string travelDate = Convert.ToString(form["TravelDate"]);
           */
            if (ModelState.IsValid)
            {

                if ((indexView.ToAirportID).Equals(indexView.FromAirportID))
                {
                    TempData["Error_string"] = "Destinasjon og avreise må være forskjellig";
                    ModelState.AddModelError("ToAirportID", "Destinasjon og avreise må være forskjellig");
                    ModelState.AddModelError("FromAirportID", "Destinasjon og avreise må være forskjellig");
                    ModelState.AddModelError("TravelDate", "Destinasjon og avreise må være forskjellig");
                    return ViewModelTest();
                }
                Session["IndexObject"] = indexView;
                Session["fromAirport"] = indexView.FromAirportID;
                Session["toAirport"] = indexView.ToAirportID;
                Session["travelDate"] = indexView.TravelDate;
                return RedirectToAction("FlightDetails");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ModelView not valid");
            }
            return RedirectToAction("Index");

        }

    }
}