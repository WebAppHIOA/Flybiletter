using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using System.Text.RegularExpressions;
using Model;
using Flybiletter.Models;

namespace Flybiletter.Controllers
{
    public class AdminController : Controller
    {
        Administrator _admin;

        public AdminController()
        {
            _admin = new Administrator();
        }

        public AdminController(Administrator testAdmin)
        {
            _admin = testAdmin;
        }

        private bool IsLoggedIn()
        {
            if (Session["LoggedIn"] == null)
            {
                Session["LoggedIn"] = false;
            }
            if (Session["LoggedIn"] is true)
            {
                return true;
            }
            return false;
        }
        // GET: Admin
        public ActionResult Login()
        {
            if (Session["LoggedIn"] == null)
            {
                Session["LoggedIn"] = false;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {

                if (_admin.GetUser(login))
                {
                    Session["LoggedIn"] = true;
                    return RedirectToAction("Home");
                }
                else
                {
                    ModelState.AddModelError("Username", "Wrong username or password");
                    Session["LoggedIn"] = false;
                    return Login();
                }
            }
            
            return Login();

        }

        public ActionResult Home()
        {
            if(IsLoggedIn())
            {
                ViewData["CountData"] = _admin.TableCounts();
                return View();
            }
            return RedirectToAction("Login","Admin");
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
                if ((departure.From).Equals(departure.To))
                {
                    ModelState.AddModelError("From", "Destinasjon og avreise må være forskjellig");
                    return Departure();
                }
                //Validering av Dato og Flyplass data
                DateTime now = new DateTime();
                now = DateTime.Now;

                DateTime dt = DateTime.Parse(departure.Date);
                if (dt < now.Date)
                {
                    //Legger til Error tekst hvis Date er før dagens dato
                    ModelState.AddModelError("Date", "Avreise dato kan ikke være tilbake i tid");
                    return Departure();
                }
                var admin = new Administrator();
                admin.AddDeparture(departure);
                return RedirectToAction("Departure");
            }

            return View(Session["Departure"] as Model.AdminDepartureViewModel);
        }


        public ActionResult DeleteDeparture(string id)
        {
            if (IsLoggedIn())
            {
                _admin.DeleteDeparture(id);
                return RedirectToAction("Departure");

            }
            return RedirectToAction("Login", "Admin");
        }

            // GET
        public ActionResult UpdateDeparture(string id)
        {
            if (IsLoggedIn())
            {
                var flight = _admin.GetDeparture(id);
                ViewData["AllAirports"] = _admin.GetAllAirports();

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
                admin.AddAirport(airport);
                return RedirectToAction("Airport");
            }
            ViewData["AllAirports"] = Session["Airport"];
            return View();
        }

        public ActionResult DeleteAirport(string id)
        {
            if (IsLoggedIn())
            {
                _admin.DeleteAirport(id);
                return RedirectToAction("Airport");
            }
            return RedirectToAction("Login", "Admin");
        }

        // GET
        public ActionResult UpdateAirport(string id)
        {
            if (IsLoggedIn())
            {
                return View(_admin.GetAirport(id));
            }
            return RedirectToAction("Login", "Admin");
        }

        //POST
        [HttpPost]
        public ActionResult UpdateAirport(Model.Airport airport)
        {

            if (IsLoggedIn())
            {
                if (ModelState.IsValid)
                {
                    _admin.UpdateAirport(airport);
                }
                return RedirectToAction("Airport");
            }
            return RedirectToAction("Login", "Admin");        
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
                    return View(Session["Order"] as Model.AdminOrderViewModel);
                }

                DateTime now = new DateTime();
                now = DateTime.Now;

                DateTime dt = DateTime.Parse(order.Date);
                if (dt < now.Date)
                {
                    ModelState.AddModelError("Date", "Avreise dato kan ikke være tilbake i tid");
                    return View(Session["Order"] as Model.AdminOrderViewModel);
                }
                admin.AddOrder(order);
                return RedirectToAction("Order");
            }

            return View(Session["Order"] as Model.AdminOrderViewModel);
        }

        public ActionResult DeleteOrder(string id)
        {
            if (IsLoggedIn())
            {
                _admin.DeleteOrder(id);
                return RedirectToAction("Order");
            }
            return RedirectToAction("Login", "Admin");
        }

        // GET
        public ActionResult UpdateOrder(string id)
        {
            if (IsLoggedIn())
            {
                return View(_admin.GetOrder(id));
            }
            return RedirectToAction("Login", "Admin");
        }

        //POST
        [HttpPost]
        public ActionResult UpdateOrder(Model.Order order)
        {
            if (IsLoggedIn())
            {
                _admin.UpdateOrder(order);
                return RedirectToAction("Order");
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult Logout()
        {
            Session["LoggedIn"] = false;
            return RedirectToAction("Index", "Home");
        }
    }
}