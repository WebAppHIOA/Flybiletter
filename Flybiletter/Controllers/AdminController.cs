using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace Flybiletter.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Home()
        {
            var adminBLL = new Administrator();
            //adminBLL.DeleteAirport("CAN");
           
           // adminBLL.UpdateAirport();

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
            return View(admin.GetDeparture(id));
        }

        //POST
        [HttpPost]
        public ActionResult UpdateDeparture(Model.Departure departure)
        {
            return RedirectToAction("Departure");
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

    }
}