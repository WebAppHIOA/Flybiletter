using Flybiletter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Flybiletter.ViewModels;

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
                DateTime now = new DateTime();
                now = DateTime.Now;
               // var time = indexView.TravelDate;
                //indexView.TravelDate = time.Date;
                if ((indexView.TravelDate) < now.Date)
                {
                    ModelState.AddModelError("TravelDate", "Avreise dato kan ikke være tilbake i tid");
                    return Index();
                }
                if((indexView.ToAirportID).Equals(indexView.FromAirportID))
                {
                    
                    ModelState.AddModelError("FromAirportID", "Destinasjon og avreise må være forskjellig");
                    return Index();
                }

                Session["IndexObject"] = indexView;
                // return RedirectToAction("Index", "Departure", new { area = "" });
                //  return RedirectToAction("FlightDetails");
                return RedirectToAction("Departures");
            }

            ModelState.AddModelError("TravelDate", "Noe gikk feil, vennsligst prøv igjen");
            return RedirectToAction("Index");
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
            /*
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

            var content = GenerateInvoice.NewInvoice(invoice);
            var streamContent = GenerateInvoice.ConvertHtmlToPDF(content);
            GenerateInvoice.SendEmail(streamContent, invoice);

           */

            return View();
        }

        public ActionResult FlightDetails()
        {

            /*  
            Session["From"] = Request.Form["from"];
            Session["To"] = Request.Form["to"];
            Session["Date"] = Request.Form["avreise"];

            return RedirectToAction("Index", "Departure");
            */
            var departures = Session["Departures"];
            ViewData["Price"] = Session["Prices"];

            return View(departures);
        }

        public JsonResult FlightDetailsTest(String id,
                                             String time,
                                             String date, String from,
                                             String to, String price)
        {

            /*  
            Session["From"] = Request.Form["from"];
            Session["To"] = Request.Form["to"];
            Session["Date"] = Request.Form["avreise"];
            return RedirectToAction("Index", "Departure");
            */
            List<String> DepartureString = new List<string>();
            DepartureString.Add(id);
            DepartureString.Add(time);
            DepartureString.Add(date);
            DepartureString.Add(from);
            DepartureString.Add(to);
            DepartureString.Add(price);
            Session["DepartureDataList"] = DepartureString;
            return Json("Success");
        }







        public ActionResult Departures()
        {
            /*   string from = Request.Form["from"];
               string to = Request.Form["to"];
               string date = Request.Form["avreise"];*/

            var indexObject = Session["IndexObject"] as IndexViewModel;

            Random random = new Random();
            int number = random.Next(8);
            string[] times = GenerateDepartures.GenerateTimes(number);

            List<Departure> departures = GenerateDepartures.CreateDepartures(indexObject.FromAirportID, indexObject.ToAirportID, indexObject.TravelDate.ToShortDateString(), times);

            Session["Prices"] = GenerateDepartures.GeneratePrice(number);
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
      
        public string RegisterFlight(Departure departure)
        {
            var db = new DB();
            db.AddDeparture(departure);
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize("OK");
        }



        public ActionResult Passenger()
        {
            ViewData["DepartureDataList"] = Session["DepartureDataList"];
            return View();
        }


        public ActionResult Confirmation()
        {
            /*
            Invoice invoice = new Invoice
            {
                InvoiceId = "TestID",
                OrderReferance = "OrderReference",
                Date = "12.03.2019",
                From = "Oslo",
                Destination = "Dubai",
                Price = "12345",
                Email = "katrinealmas@gmail.com",
            };

            var content = GenerateInvoice.NewInvoice(invoice);
            var streamContent = GenerateInvoice.ConvertHtmlToPDF(content);
            GenerateInvoice.SendEmail(streamContent, invoice);
            */
            return View();
        }
    }
}