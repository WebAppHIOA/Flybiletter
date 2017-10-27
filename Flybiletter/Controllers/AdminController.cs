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
                return View(_admin.GetAllDepartures());
            }
            return RedirectToAction("Login", "Admin");
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
            _admin.UpdateDeparture(departure);
            return RedirectToAction("Departure");
        }

        public ActionResult Airport()
        {
            if (IsLoggedIn())
            {
                return View(_admin.GetAllAirports());
            }
            return RedirectToAction("Login", "Admin");

        }

        public ActionResult DeleteAirport(string id)
        {
            _admin.DeleteAirport(id);
            return RedirectToAction("Airport");
        }

        // GET
        public ActionResult UpdateAirport(string id)
        {
            return View(_admin.GetAirport(id));
        }

        //POST
        [HttpPost]
        public ActionResult UpdateAirport(Model.Airport airport)
        {
            _admin.UpdateAirport(airport);
            return RedirectToAction("Airport");
        }


        public ActionResult Order()
        {
            return View(_admin.GetAllOrders());
        }

        public ActionResult DeleteOrder(string id)
        {
            _admin.DeleteOrder(id);
            return RedirectToAction("Order");
        }

        // GET
        public ActionResult UpdateOrder(string id)
        {
            return View(_admin.GetOrder(id));
        }

        //POST
        [HttpPost]
        public ActionResult UpdateOrder(Model.Order order)
        {
            _admin.UpdateOrder(order);
            return RedirectToAction("Order");
        }

        public ActionResult Logout()
        {
            Session["LoggedIn"] = false;
            return RedirectToAction("Index", "Home");
        }
    }
}