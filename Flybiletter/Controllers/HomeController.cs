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
                if ((indexView.ToAirportID).Equals(indexView.FromAirportID))
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

        
        public JsonResult FlightDetailsTest(String id, String time,
                                            String date, String from,
                                            String to, String price)
        {
            System.Diagnostics.Debug.WriteLine(id
                                              + time
                                             + date + from
                                             + to + price);
            System.Diagnostics.Debug.WriteLine("Burde være JSON over denne meldingen");
            /*  
            Session["From"] = Request.Form["from"];
            Session["To"] = Request.Form["to"];
            Session["Date"] = Request.Form["avreise"];

            return RedirectToAction("Index", "Departure");
            */

            Dictionary<String, String> DepartureDict = new Dictionary<string, string>();
            DepartureDict.Add("id", id);
            DepartureDict.Add("date", date);
            DepartureDict.Add("to", to);
            DepartureDict.Add("from", from);
            DepartureDict.Add("price", price);

            List<String> DepartureString = new List<string>();
            DepartureString.Add(id);
            DepartureString.Add(name);
            DepartureString.Add(time);
            DepartureString.Add(date);
            DepartureString.Add(from);
            DepartureString.Add(to);
            DepartureString.Add(price);
            Session["DepartureDataList"] = DepartureString;
            Session["DepartureDataDict"] = DepartureDict;
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

            List<Departure> departures = GenerateDepartures.CreateDepartures(indexObject.FromAirportID, indexObject.ToAirportID, indexObject.TravelDate, times);

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



        public string GetSelectedFlight(string flightID)
        {
            var db = new DB();
            db.FindDeparture(flightID);

            /*

            List<Departure> selectDep = db.GetDePInfo();
            var flightDetails = new List<Departure>();
            foreach(Departure d in selectDep)
            {
                var flight = new Departure();
                flight.FlightId = d.FlightId;
                flight.DepartureTime = d.DepartureTime;
                flight.From = d.From;
                flight.To = d.To;

                flightDetails.Add(flight);
            }*/
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(flightID);
            return json;
        }

        

        //???????????????????
        public string GetOrderInfo(string orderNumber)
        {
            var db = new DB();
            Order order = db.FindOrder(orderNumber);
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(order);
            return json;

            
        }
        
        //?????????????????????
        public string RegisterOrder(Order insertOrder)
        {
            var db = new DB();
            db.AddOrder(insertOrder);
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize("OK");
        }



        public ActionResult Passenger()
        {
            var DB = new DB();
            var Dep = new Departure();
            List<Airport> Airports = DB.getAllAirports();
            List<String> DepList = (List<String>)Session["DepartureDataList"];
            Dictionary<string, string> DepartureDict = (Dictionary < string, string>)Session["DepartureDataDict"];

            foreach(Airport airport in Airports)
            {
                if(airport.AirportId == DepartureDict["from"])
                {
                    Dep.Airport = airport;
                }
            }

            foreach (Airport airport in Airports)
            {
                if (airport.AirportId == DepartureDict["from"])
                {
                    Dep.From = airport.Name;
                }
            }

            foreach (Airport airport in Airports)
            {
                if (airport.AirportId == DepartureDict["to"])
                {
                    Dep.To = airport.Name;
                }
            }


            if (DepartureDict != null)
            {
                ViewData["DepartureDataList"] = DepartureDict;
            }

            
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