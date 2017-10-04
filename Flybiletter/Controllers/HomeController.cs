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
            /*TODO VALIDERING
            if (ModelState.IsValid)
            {
                return RedirectToAction("");
            }*/
            var db = new DB();
            List<Airport> allAirports = db.getAllAirports();

            return View(allAirports);
        }

    
        public ActionResult Order(Order order)
        {

            return View();
        }

  
        public ActionResult FlightDetails()
        {
            TempData["form"] = Request.Form;

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

        /* Hjelpe metode for å generere unike referanser til feks orderNumber.
        *  Legges her inntil videre da man trenger å lagre referansen dersom man skal søke spesifikt i db.
        */
        private string UniqueReference()
        {
            var guid = System.Guid.NewGuid().ToString();

            return guid;
        }

    }
}