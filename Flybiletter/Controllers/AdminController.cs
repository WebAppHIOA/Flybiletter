using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using System.Text.RegularExpressions;

namespace Flybiletter.Controllers
{
    public class AdminController : Controller
    {
        // GET
        public ActionResult Home()
        {
            var adminBLL = new Administrator();

            ViewData["CountData"] = adminBLL.TableCounts();
            return View();
        }

        //GET
        public ActionResult Departure()
        {
            var admin = new Administrator();
            var AdminDepVM = new Model.AdminDepartureViewModel();
            AdminDepVM.DepartureDetails = admin.GetAllDepartures();
            AdminDepVM.Airport = admin.GetAllAirports();
            Session["Departure"] = AdminDepVM; 
            return View(AdminDepVM);
        }

        [HttpPost]
        public ActionResult Departure(Model.AdminDepartureViewModel departure)
        {
            if (ModelState.IsValid)
            {
                DateTime now = new DateTime();
                now = DateTime.Now;
                DateTime dt = DateTime.Parse(departure.Date);

                if ((departure.From).Equals(departure.To))
                {
                    ModelState.AddModelError("From", "Destinasjon og avreise må være forskjellig");
                    return PartialView("DepartureForm", Session["Departure"] as Model.AdminDepartureViewModel);
                }
                if (dt < now.Date)
                {
                    //Legger til Error tekst hvis Date er før dagens dato
                    ModelState.AddModelError("Date", "Avreise dato kan ikke være tilbake i tid");
                    return PartialView("DepartureForm", Session["Departure"] as Model.AdminDepartureViewModel);
                }
               
                    var admin = new Administrator();
                    admin.AddDeparture(departure);
                    return Json(new { result = true });
            }
                return PartialView("DepartureForm", Session["Departure"] as Model.AdminDepartureViewModel);
            }
        
    


        public ActionResult DeleteDeparture(string id)
        {
            var admin = new Administrator();
            admin.DeleteDeparture(id);
            return RedirectToAction("Departure");
        }

        // GET
        public ActionResult UpdateDeparture(string id)
        {
            var admin = new Administrator();

            if (id == null)
            {
                ModelState.AddModelError("Cancelled", "Denne avgangen eksister ikke i systemet");
            }
            var flight = admin.GetDeparture(id);
            Session["AllAirports"] = admin.GetAllAirports();
            ViewData["AllAirports"] = Session["AllAirports"];

            return View(flight);
        }

        //POST
        [HttpPost]
        public ActionResult UpdateDeparture(Model.Departure departure)
        {
            if (ModelState.IsValid) {
                if ((departure.From).Equals(departure.To))
                {
                    ModelState.AddModelError("To", "Destinasjon og avreise må være forskjellig");
                    ViewData["AllAirports"] = Session["AllAirports"] as List<Model.Airport>;
                    return View();
                }
                
                DateTime now = new DateTime();
                now = DateTime.Now;
  
                DateTime dt = DateTime.Parse(departure.Date);
                if (dt < now.Date)
                   {
                    ViewData["AllAirports"] = Session["AllAirports"] as List<Model.Airport>;
                    ModelState.AddModelError("Date", "Avreise dato kan ikke være tilbake i tid");
                       return View();
                   }
                   
                var admin = new Administrator();
                admin.UpdateDeparture(departure);
                return RedirectToAction("Departure");
            }
            ViewData["AllAirports"] = Session["AllAirports"] as List<Model.Airport>;
            return View();
        }

        public ActionResult Airport()
        {
            var admin = new Administrator();
            Session["Airport"] = admin.GetAllAirports();
            ViewData["AllAirports"] = Session["Airport"];
            return View();
        }

        [HttpPost]
        public ActionResult Airport(Model.Airport airport)
        {
            var admin = new Administrator();
            if (ModelState.IsValid)
            {
                ViewData["AllAirports"] = Session["Airport"];
                admin.AddAirport(airport);
                
                return Json(new { result = true});
            }
            ViewData["AllAirports"] = Session["Airport"];
            return PartialView("AirportForm");
        }

        public ActionResult DeleteAirport(string id)
        {
            var admin = new Administrator();
            admin.DeleteAirport(id);
            return RedirectToAction("Airport");
        }

        // GET
        public ActionResult UpdateAirport(string id)
        {
            var admin = new Administrator();
            return View(admin.GetAirport(id));
        }

        //POST
        [HttpPost]
        public ActionResult UpdateAirport(Model.Airport airport)
        {
            if (ModelState.IsValid)
            {
                var admin = new Administrator();
                admin.UpdateAirport(airport);
                return RedirectToAction("Airport");
            }
            return View();
        }

        //GET
        public ActionResult Order()
        {
            var admin = new Administrator();
            var AdminOrderVM = new Model.AdminOrderViewModel();
            AdminOrderVM.Order = admin.GetAllOrders();
            AdminOrderVM.Departure = admin.GetAllDepartures();
            Session["Order"] = AdminOrderVM;
            return View(AdminOrderVM);
        }

        
        [HttpPost]
        public ActionResult Order(Model.AdminOrderViewModel order)
        {
            var admin = new Administrator();
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(order.FlightId))
                {
                    ModelState.AddModelError("FlightId", "Vennligst oppgi FlightId");
                    return PartialView("OrderForm", Session["Order"] as Model.AdminOrderViewModel);
                }

                DateTime now = new DateTime();
                now = DateTime.Now;

                DateTime dt = DateTime.Parse(order.Date);
                if (dt < now.Date)
                {
                    ModelState.AddModelError("Date", "Avreise dato kan ikke være tilbake i tid");
                    return PartialView("OrderForm", Session["Order"] as Model.AdminOrderViewModel);
                }
                admin.AddOrder(order);
                return Json(new { result = true });
            }

            return PartialView("OrderForm", Session["Order"] as Model.AdminOrderViewModel);
        }
       

        public ActionResult DeleteOrder(string id)
        {
            var admin = new Administrator();
            admin.DeleteOrder(id);
            return RedirectToAction("Order");
        }

        // GET
        public ActionResult UpdateOrder(string id)
        {
            var admin = new Administrator();
            return View(admin.GetOrder(id));
        }

        //POST
        [HttpPost]
        public ActionResult UpdateOrder(Model.Order order)
        {
            if (ModelState.IsValid)
            {
                var admin = new Administrator();
                admin.UpdateOrder(order);
                return RedirectToAction("Order");
            }
            return View();
        }
    }
}