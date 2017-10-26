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
            return View(admin.GetAllDepartures());
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
            return RedirectToAction("UpdateDeparture");
        }

        public ActionResult Airport()
        {
            var admin = new Administrator();
            return View(admin.GetAllAirports());
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
            var admin = new Administrator();
            admin.UpdateAirport(airport);
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
        public ActionResult UpdateOrder(Model.Order order)
        {
            var admin = new Administrator();
            admin.UpdateOrder(order);
            return RedirectToAction("Order");
        }

    }
}