using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Flybiletter.Models;

namespace Flybiletter.Controllers
{
    public class AdminController : Controller
    {
    
        // GET: Admin
        public ActionResult Login()
        {
            if(Session["LoggetInn"] ==null)
            {
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
            }
            else
            {
                ViewBag.Innlogget = (bool)Session["LoggetInn"];
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(Model.Login user)
        {
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            if (Session["LoggetInn"] == null || false)
            {
                Session["LoggetInn"] = false;
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                ViewBag.Innlogget = (bool)Session["LoggetInn"];
            }
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


    private static bool LoginCheck(LoginModel user)
    {
        using(var db = BLL.Administrator)
        {
            byte[] passordDb = makeHash(user.Password);
            dbUser = foundUser = db.

        }
        


    }
}