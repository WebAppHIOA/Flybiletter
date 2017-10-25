using Model;
using System;
using Flybiletter.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BLL;

namespace Flybiletter.Controllers
{
    public class HomeController : Controller
    {
        BLL.Order OrderBLL;

        public HomeController()
        {
            OrderBLL = new BLL.Order();
        }

        public HomeController(BLL.Order testOrder)
        {
            OrderBLL = testOrder;
        }

        public ActionResult Index()
        {
            
            var airports = OrderBLL.getAllAirports();

            var IndexVM = new IndexViewModel();
            IndexVM.FromAirport = airports;
            IndexVM.ToAirport = airports;

            return View(IndexVM);
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel indexView)
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


        public ActionResult Order(Model.Order order)
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
        public JsonResult Departures1(String id,
                                             String time,
                                             String date, String from,
                                             String to, String price)
        {
            System.Diagnostics.Debug.WriteLine("Departure data blei sjekka 1");
            //Validerer JSON parameter server side
            if (id == "" || id == null)
            {
                System.Diagnostics.Debug.WriteLine("Departure data blei sjekka 2");
                return Json(new { success = false, response = "Vennligst velg en reise" });
            }
            else
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

                if (selectedDeparture.Id == null)
                {
                    return Json(new { success = false, response = "Vennligst velg en reise" });
                }
                else
                {
                    Session["SelectedDeparture"] = selectedDeparture;

                    return Json(new { success = true, Response = "Success" });
                }
            }
        }


        public ActionResult Departures()
        {
            var indexObject = Session["IndexObject"] as IndexViewModel;

            List<Departure> departures = OrderBLL.CreateDepartures(indexObject.FromAirportID, indexObject.ToAirportID, indexObject.TravelDate.ToShortDateString());

            Session["Prices"] = OrderBLL.GeneratePrice(departures.Count);
            Session["Departures"] = departures;

            return RedirectToAction("FlightDetails");
        }

        public ActionResult Passenger()
        {
            ViewData["SelectedDeparture"] = Session["SelectedDeparture"];
            return View();
        }


        [HttpPost]
        public ActionResult Passenger(Model.Order order)
        {
            var departure = Session["SelectedDeparture"] as DepartureViewModel;
            
        
         //   var toAirport = orderBLL.FindAirport(departure.To);

            Departure dep = new Departure
            {
                FlightId = departure.Id,
                From = departure.From,
                To = departure.To,
                Date = departure.Date,
                DepartureTime = departure.Time,
                Airport = OrderBLL.FindAirport(departure.From)
            };

            OrderBLL.AddDeparture(dep);

            order.OrderNumber = GenerateInvoice.UniqueReference();
            OrderBLL.AddOrder(new Model.Order
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

            GenerateInvoice.SendEmail(OrderBLL.GetInvoiceInformation(dep.FlightId, order.OrderNumber));

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

        public ActionResult Login()
        {
            return RedirectToAction("Index", "Admin");
        }

    }
}