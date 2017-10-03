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
            
            /*
            var deppa = new Departure();
            deppa.FlightId = "SK3254";
            deppa.From = "Amsterdam";
            deppa.To = "Oslo";
            deppa.Arrival = "21.20";
            deppa.Date = "23.12.17";
            deppa.Airport = allAirports[0];

            db.AddDeparture(deppa);
            var ord = new Order();
            ord.OrderNumber = UniqueReference();
            ord.Date = "20.09.2018";
            ord.Price = "4095";
            ord.Travellers = "3";
            ord.RoundTrip = "Yes";
            db.AddOrder(ord);

            var pass = new Passenger();
            pass.PassengerId = "ugh";
            pass.Firstname = "Bob";
            pass.Surname = "Bobbert";
            pass.Tlf = 12345678;
            pass.Email = "123@gmail.com";
            pass.Class = "First";
            pass.Category = "Voksen";
            pass.Departure = deppa;
            pass.Order = ord;
            

            db.AddPassenger(pass);
            */

            return View(allAirports);
        }

        /* Navn på metode, parameter og evt innhold må endres når index er klar
         *
         */
        public ActionResult Order(Order order)
        {

            return View();
        }
        public ActionResult FlightDetails()
        {
           
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

   
        private string UniqueReference()
        {
            var guid = System.Guid.NewGuid().ToString();

            return guid;
        }

    }
}