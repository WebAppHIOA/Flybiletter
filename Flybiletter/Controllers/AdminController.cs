using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Flybiletter.Models;

namespace Flybiletter.Controllers
{
    public class AdminController : Controller
    {
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
            var admin = new Administrator();

            if (admin.GetUser(login))
            {
                Session["LoggedIn"] = true;
                return RedirectToAction("Home");
            }
            else
            {
                Session["LoggedIn"] = false;
                return Login();
            }
        }

        public ActionResult Home()
        {
            var adminBLL = new Administrator();

            if(IsLoggedIn())
            {
                ViewData["CountData"] = adminBLL.TableCounts();
                return View();
            }
            return RedirectToAction("Login","Admin");
        }

        //GET
        public ActionResult Departure()
        {
            if (IsLoggedIn())
            {
                var admin = new Administrator();
                return View(admin.GetAllDepartures());
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult DeleteDeparture(string id)
        {
            if (IsLoggedIn())
            {
                var admin = new Administrator();
                admin.DeleteDeparture(id);
                return RedirectToAction("Departure");

            }
            return RedirectToAction("Login", "Admin");
        }

            // GET
        public ActionResult UpdateDeparture(string id)
        {
            if (IsLoggedIn())
            {
                var admin = new Administrator();
                return View(admin.GetDeparture(id));
            }
            return RedirectToAction("Login", "Admin");
        }

        //POST
        [HttpPost]
        public ActionResult UpdateDeparture(Model.Departure departure)
        {
                return RedirectToAction("Departure");    
        }

        public ActionResult Airport()
        {
            if (IsLoggedIn())
            {
                var admin = new Administrator();
                return View(admin.GetAllAirports());
            }
            return RedirectToAction("Login", "Admin");

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
            return RedirectToAction("Airport");
        }


        public ActionResult Order()
        {

            var admin = new Administrator();
            return View(admin.GetAllOrders());
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
        public ActionResult UpdateOrder(Model.Order airport)
        {
            return RedirectToAction("Order");
        }

        public ActionResult Logout()
        {
            Session["LoggedIn"] = false;
            return RedirectToAction("Index", "Home");
        }
    }
}