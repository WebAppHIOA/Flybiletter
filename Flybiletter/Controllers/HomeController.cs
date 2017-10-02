using Flybiletter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flybiletter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new DB();
            List<Airport> allAirports = db.getAllAirports();
            return View(allAirports);
        }

        public ActionResult Reise()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Passasjer()
        {
            //TODO
            if (ModelState.IsValid)
            {
                return RedirectToAction("");
            }
            return View();
        }
        
        public ActionResult Bekreftelse()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}