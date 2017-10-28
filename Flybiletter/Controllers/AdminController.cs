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
            if (IsLoggedIn())
            {
                var AdminDepVM = new Model.AdminDepartureViewModel();
                AdminDepVM.DepartureDetails = _admin.GetAllDepartures();
                AdminDepVM.Airport = _admin.GetAllAirports();
                return View(AdminDepVM);
            }
            return RedirectToAction("Login", "Admin");
        }
            
        

        [HttpPost]
        public ActionResult Departure(Model.AdminDepartureViewModel dep)
        {
            if (ModelState.IsValid)
            {
                _admin.AddDeparture(dep);
                return RedirectToAction("Departure");
            }
            return RedirectToAction("Departure");
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

                if (flight == null)
                {
                    ModelState.AddModelError("Cancelled", "Denne avgangen eksister ikke i systemet");
                }
                return View(flight);
            }
            return RedirectToAction("Login", "Admin");

            
        }

        //POST
        [HttpPost]
        public ActionResult UpdateDeparture(Model.Departure departure)
        {
            if (IsLoggedIn())
            {
                _admin.UpdateDeparture(departure);
                return RedirectToAction("Departure");
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult Airport()
        {
            ViewData["AllAirports"] = _admin.GetAllAirports();
            var airport = new Model.Airport();
            return View(airport);
        }

        [HttpPost]
        public ActionResult Airport(Model.Airport airport)
        {
            
            if (IsLoggedIn())
            {
                if (ModelState.IsValid)
                {
                    _admin.AddAirport(airport);
                    return RedirectToAction("Airport");
                }
                return RedirectToAction("Airport");
                //return View(_admin.GetAllAirports());
            }
            return RedirectToAction("Login", "Admin");

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
            if (IsLoggedIn())
            {
                var AdminOrderVM = new Model.AdminOrderViewModel();
                AdminOrderVM.Order = _admin.GetAllOrders();
                AdminOrderVM.Departure = _admin.GetAllDepartures();
                return View(AdminOrderVM);
            }
            return RedirectToAction("Login", "Admin");
        }
        

        [HttpPost]
        public ActionResult Order(Model.AdminOrderViewModel order)
        {
            if (IsLoggedIn())
            {
                if (ModelState.IsValid)
                {
                    _admin.AddOrder(order);
                }
                return RedirectToAction("Order");
            }
            return RedirectToAction("Login", "Admin");
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