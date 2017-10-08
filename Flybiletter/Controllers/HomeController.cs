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
            var IndexVM = new IndexViewModel();
            IndexVM.FromAirport = DB.getAllAirports();
            IndexVM.ToAirport = DB.getAllAirports();

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
            
            List<Departure> departures = GenerateDepartures.CreateDepartures(indexObject.FromAirportID, indexObject.ToAirportID, indexObject.TravelDate);

            Session["Prices"] = GenerateDepartures.GeneratePrice(departures.Count);
            Session["Departures"] = departures;

            return RedirectToAction("FlightDetails");
        }



        public string GetSelectedFlight(string flightID)
        {
            DB.FindDeparture(flightID);

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
            Order order = DB.FindOrder(orderNumber);
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(order);
            return json;      
        }
        
        //?????????????????????
        public string RegisterOrder(Order insertOrder)
        {
            DB.AddOrder(insertOrder);
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

            var fromAirport = DB.FindAirport(indexView.FromAirportID);
            var toAirport = DB.FindAirport(indexView.ToAirportID);

            Departure dep = new Departure
            {
                FlightId = departure[0],
                From = departure[4],
                To = departure[5],
                Date = departure[3],
                DepartureTime = departure[2],
                Airport = fromAirport
            };

            DB.AddDeparture(dep);

            order.OrderNumber = GenerateInvoice.UniqueReference();
            DB.AddOrder(new Order
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

            var invoice = DB.getInvoiceInformation(dep.FlightId ,order.OrderNumber);

            GenerateInvoice.SendEmail(invoice);

            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {

            return View();
        }

    }
}