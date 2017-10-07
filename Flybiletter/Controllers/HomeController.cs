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
            var IndexVM = new IndexViewModel();
            IndexVM.FromAirport = db.getAllAirports();
            IndexVM.ToAirport = db.getAllAirports();

            return View(IndexVM);
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel indexView)
        {
            if (ModelState.IsValid)
            {
                if ((indexView.ToAirportID).Equals(indexView.FromAirportID))
                {
                    ModelState.AddModelError("FromAirportID", "Destinasjon og avreise må være forskjellig");
                    return Index();
                }

                Session["IndexObject"] = indexView;
                return RedirectToAction("Departures");
            }

            ModelState.AddModelError("TravelDate", "Noe gikk feil, vennligst prøv igjen");
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

        public ActionResult FlightDetails()
        {

            var departures = Session["Departures"];
            ViewData["Price"] = Session["Prices"];

            return View(departures);
        }

        
        public JsonResult FlightDetailsTest(String id, 
                                            String name , String time,
                                            String date, String from,
                                            String to, String price)
        {
            System.Diagnostics.Debug.WriteLine(id
                                             + name + time
                                             + date + from
                                             + to + price);
            System.Diagnostics.Debug.WriteLine("Burde være JSON over denne meldingen");
         
            List<String> DepartureString = new List<string>();
            DepartureString.Add(id);
            DepartureString.Add(name);
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
            /*            List<String> DepartureString = new List<string>();
                            DepartureString.Add(id);
                            DepartureString.Add(name);
                            DepartureString.Add(time);
                             DepartureString.Add(date);
                             DepartureString.Add(from);
                             DepartureString.Add(to);
                              DepartureString.Add(price);
             * 
             */
            ViewData["DepartureDataList"] = Session["DepartureDataList"];

            return View();
        }


        [HttpPost]
        public ActionResult Passenger(Order order)
        {
            var departure = Session["DepartureDataList"] as List<String>;
            var indexView = Session["IndexObject"] as IndexViewModel;
            List<Airport> airports = indexView.FromAirport;

            DB db = new DB();
            
            var fromAirport = db.FindAirport(indexView.FromAirportID);
            var toAirport = db.FindAirport(indexView.ToAirportID);

            Departure dep = new Departure
            {
                FlightId = departure[0],
                From = departure[4],
                To = departure[5],
                Date = departure[3],
                DepartureTime = departure[2],
                Airport = fromAirport
            };

            db.AddDeparture(dep);

            order.OrderNumber = UniqueReference();
            db.AddOrder(new Order
            {
                OrderNumber = order.OrderNumber,
                Date = departure[4],
                Firstname = order.Firstname,
                Surname = order.Surname,
                Tlf = order.Tlf,
                Email = order.Email,
                Price = departure[6],
                Departure = dep
            });

            var indexObjekt = Session["IndexObject"] as IndexViewModel;

            Invoice invoice = new Invoice
            {
                InvoiceId = UniqueReference(),
                OrderReferance = UniqueReference(),
                Date = indexObjekt.TravelDate,
                From = fromAirport.Name,
                Destination = toAirport.Name,
                Price = departure[6],
                Email = order.Email
            };

            var content = GenerateInvoice.NewInvoise(invoice);
            var streamContent = GenerateInvoice.ConvertHtmlToPDF(content);
            GenerateInvoice.SendEmail(streamContent, invoice);

            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {

            return View();
        }

        public string UniqueReference()
        {
            var guid = System.Guid.NewGuid().ToString();

            return guid;
        }

    }
}