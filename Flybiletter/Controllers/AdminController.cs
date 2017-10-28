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

            //adminBLL.DeleteAirport("CAN");

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

            return View(AdminDepVM);
        }

        [HttpPost]
        public ActionResult Departure(Model.AdminDepartureViewModel dep)
        {
            var admin = new Administrator();
            if (ModelState.IsValid)
            {
                admin.AddDeparture(dep);
                return RedirectToAction("Departure");
            }
            return RedirectToAction("Departure");
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

            var flight = admin.GetDeparture(id);
            ViewData["AllAirports"] = admin.GetAllAirports();
            
            if (flight == null) {
                ModelState.AddModelError("Cancelled", "Denne avgangen eksister ikke i systemet");
            }
            return View(flight);
        }

        //POST
        [HttpPost]
        public ActionResult UpdateDeparture(Model.Departure departure)
        {
            var admin = new Administrator();
            admin.UpdateDeparture(departure);
            return RedirectToAction("Departure");
        }

        public ActionResult Airport()
        {
            var admin = new Administrator();
            ViewData["AllAirports"] = admin.GetAllAirports();
            var airport = new Model.Airport();
            return View(airport);
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
            return RedirectToAction("Airport");
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
            if (ModelState.IsValid) {
                var admin = new Administrator();
                admin.UpdateAirport(airport);
            }
            return RedirectToAction("Airport");
        }

        //GET
        public ActionResult Order()
        {
            var admin = new Administrator();
            var AdminOrderVM = new Model.AdminOrderViewModel();
            AdminOrderVM.Order = admin.GetAllOrders();
            AdminOrderVM.Departure = admin.GetAllDepartures();
            return View(AdminOrderVM);
        }
        

        [HttpPost]
        public ActionResult Order(Model.AdminOrderViewModel order)
        {
            var admin = new Administrator();
            if (ModelState.IsValid)
            {
                admin.AddOrder(order);
                return RedirectToAction("Order");
            }
            return RedirectToAction("Order");
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
            var admin = new Administrator();
            admin.UpdateOrder(order);
            return RedirectToAction("Order");
        }
        
    }
}