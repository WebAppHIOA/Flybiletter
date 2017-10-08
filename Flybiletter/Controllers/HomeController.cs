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
                //Validering av Dato og Flyplass data
                DateTime now = new DateTime();
                now = DateTime.Now;
                if ((indexView.TravelDate) < now.Date)
                {
                    //Legger til Error tekst hvis TravelDate er tidligere en Dagens dato
                    ModelState.AddModelError("TravelDate", "Avreise dato kan ikke være tilbake i tid");
                    return Index();
                }
                if((indexView.ToAirportID).Equals(indexView.FromAirportID))
                {
                    //Legger til Error tekst hvis fra og til AirportID er det samme
                    ModelState.AddModelError("FromAirportID", "Destinasjon og avreise må være forskjellig");
                    return Index();
                }

                //Setter session og sender brukeren til Departures
                Session["IndexObject"] = indexView;
                return RedirectToAction("Departures");
            }

            //Hvis ModelState ikke er valid, setter Error og sender til Index
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

        [HttpPost]
        public JsonResult Departures(String id,
                                             String time,
                                             String date, String from,
                                             String to, String price)
        {

            var selectedDeparture = new DepartureViewModel
            {
                Id = id,
                Time = time,
                Date = date,
                From = from,
                To = to,
                Price = price
            };

            Session["SelectedDeparture"] = selectedDeparture;

            return Json("Success");
        }


        public ActionResult Departures()
        {
            var indexObject = Session["IndexObject"] as IndexViewModel;

            List<Departure> departures = GenerateDepartures.CreateDepartures(indexObject.FromAirportID, indexObject.ToAirportID, indexObject.TravelDate.ToShortDateString());

            Session["Prices"] = GenerateDepartures.GeneratePrice(departures.Count);
            Session["Departures"] = departures;

            return RedirectToAction("FlightDetails");
        }

        public ActionResult Passenger()
        {
            ViewData["SelectedDeparture"] = Session["SelectedDeparture"];
            return View();
        }


        [HttpPost]
        public ActionResult Passenger(Order order)
        {
            var departure = Session["SelectedDeparture"] as DepartureViewModel;

            var fromAirport = DB.FindAirport(departure.From);
            var toAirport = DB.FindAirport(departure.To);

            Departure dep = new Departure
            {
                FlightId = departure.Id,
                From = departure.From,
                To = departure.To,
                Date = departure.Date,
                DepartureTime = departure.Time,
                Airport = fromAirport
            };

            DB.AddDeparture(dep);

            order.OrderNumber = GenerateInvoice.UniqueReference();
            DB.AddOrder(new Order
            {
                OrderNumber = order.OrderNumber,
                Date = departure.Date,
                Firstname = order.Firstname,
                Surname = order.Surname,
                Tlf = order.Tlf,
                Email = order.Email,
                Price = departure.Price,
                Departure = dep
            });

            GenerateInvoice.SendEmail(DB.getInvoiceInformation(dep.FlightId, order.OrderNumber));

            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}