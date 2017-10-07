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
                return RedirectToAction("Departures");
            }

            ModelState.AddModelError("TravelDate", "Noe gikk feil, vennsligst prøv igjen");
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
                                             String time,
                                             String date, String from,
                                             String to, String price)
        {
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
            var indexObject = Session["IndexObject"] as IndexViewModel;

            List<Departure> departures = GenerateDepartures.CreateDepartures(indexObject.FromAirportID, indexObject.ToAirportID, indexObject.TravelDate);

            Session["Prices"] = GenerateDepartures.GeneratePrice(departures.Count);
            Session["Departures"] = departures;

            return RedirectToAction("FlightDetails");
        }

        public ActionResult Passenger()
        {
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
                From = departure[3],
                To = departure[4],
                Date = departure[2],
                DepartureTime = departure[1],
                Airport = fromAirport
            };

            DB.AddDeparture(dep);

            order.OrderNumber = GenerateInvoice.UniqueReference();
            DB.AddOrder(new Order
            {
                OrderNumber = order.OrderNumber,
                Date = departure[2],
                Firstname = order.Firstname,
                Surname = order.Surname,
                Tlf = order.Tlf,
                Email = order.Email,
                Price = departure[5],
                Departure = dep
            });

            var indexObjekt = Session["IndexObject"] as IndexViewModel;

            Invoice invoice = new Invoice
            {
                InvoiceId = order.OrderNumber,
                OrderReferance = order.OrderNumber,
                Date = indexObjekt.TravelDate,
                From = fromAirport.Name,
                Destination = toAirport.Name,
                Price = departure[5],
                Email = order.Email
            };

            GenerateInvoice.SendEmail(invoice);

            return RedirectToAction("Confirmation");
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
        public string UniqueReference()
        {
            var guid = System.Guid.NewGuid().ToString();

            return guid;
        }

    }
}