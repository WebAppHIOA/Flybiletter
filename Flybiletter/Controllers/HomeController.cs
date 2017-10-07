using Flybiletter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public ActionResult Test()
        {
            var db = new DB();
            var objekt = db.FindDeparture("TestId");
            String test = objekt.Airport.City;
            return View(test);
        }

    
        public ActionResult Order(Order order)
        {

            return View();
        }

        public ActionResult TestEmail()
        {
            Invoice invoice = new Invoice
            {
                InvoiceId = "TestID",
                OrderReferance = "908497532",
                Date = "12.03.2019",
                From = "Oslo",
                Destination = "Dubai",
                Price = "12345",
                Email = "katrinealmas@gmail.com",
            };

            var content = GenerateInvoice.NewInvoise(invoice);
            var streamContent = GenerateInvoice.ConvertHtmlToPDF(content);
            GenerateInvoice.SendEmail(streamContent, invoice);

            return View();
        }

        public ActionResult FlightDetails()
        {
            var departures = Session["Departures"];
            ViewData["Price"] = Session["Prices"];

            return View(departures);
        }

        public ActionResult Departures()
        {
            string from = Request.Form["from"];
            string to = Request.Form["to"];
            string date = Request.Form["avreise"];

            Random random = new Random();
            int antall = random.Next(8);
            string[] tider = GenerateDepartures.GenerateTimes(antall);

            List<Departure> departures = GenerateDepartures.CreateDepartures(from, to, date, tider);

            Session["Prices"] = GenerateDepartures.GeneratePrice(antall);
            Session["Departures"] = departures;

            return RedirectToAction("FlightDetails");
        }

        public ActionResult AddDeparture()
        {
            //Test av ny db insert
            DB db = new DB();

            List<Airport> allAirports = db.getAllAirports();

            Departure dep = new Departure
            {
                FlightId = "TestId",
                From = "Amsterdam",
                To = "Oslo",
                Date = "21.12",
                DepartureTime = "20.30",
                Airport = allAirports[0]
            };

            db.AddDeparture(dep);

            return RedirectToAction("Passenger");
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

       /* Skal få inn fra formet i passenger.cshtml, httpPost Order order som parameter?. Destinasjon ligger i modelView session
        * blir lagt inn ved merge
        */
      
        public ActionResult Confirmation(Order order)
        {
            /*Invoice invoice = new Invoice
            {
                InvoiceId = "TestID",
                OrderReferance = "OrderReference",
                Date = "12.03.2019",
                From = "Oslo",
                Destination = "Dubai",
                Price = "12345",
                Email = "katrinealmas@gmail.com",
            };*/

            // var indexObjekt = Session["IndexObject"];

            Invoice invoice = new Invoice
            {
                InvoiceId = UniqueReference(),
                OrderReferance = order.OrderNumber,
                Date = order.Date,
                From = "TBD", // indexObjekt.FromAirportID
                Destination = "TBD", // index.Objekt.ToAirportID 
                Price = order.Price,
                Email = order.Email,
            };

            var content = GenerateInvoice.NewInvoise(invoice);
            var  streamContent = GenerateInvoice.ConvertHtmlToPDF(content);
            GenerateInvoice.SendEmail(streamContent, invoice);

            return View();
        }
        public string UniqueReference()
        {
            var guid = System.Guid.NewGuid().ToString();

            return guid;
        }

    }
}