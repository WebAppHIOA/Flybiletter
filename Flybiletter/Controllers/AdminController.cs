using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flybiletter.Controllers
{
    public class AdminController : Controller
    {
    
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        /*
        [HttpPost]
        public ActionResult Login()
        {
            return RedirectToAction("Home");
        }*/

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Departure()
        {
            return View();
        }

        public ActionResult Airport()
        {
            return View();
        }


        public ActionResult Order()
        {
            return View();
        }
    }
}