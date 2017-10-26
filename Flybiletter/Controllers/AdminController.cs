using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL;
using System.Security.Principal;
using static BLL.Administrator;

namespace Flybiletter.Controllers
{
    public class AdminController : Controller
    {
    
        // GET: Admin
        public ActionResult Login()
        {/*
            var adminBLL = new Administrator();
            adminBLL.DeleteAirport("CAN");*/

            if (Session["LoggedIn"] == null)
            {
                Session["LoggedIn"] = false;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Model.Login login)
        {
            Administrator admin = new Administrator();
            
            if (admin.UserLogin(login))
            {
                Session["LoggedIn"] = true;
                return RedirectToAction("Home", "Admin");
            }
            else
            {
                Session["LoggedIn"] = false;
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["LoggedIn"] = false;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Home()
        {
            if (Session["LoggedIn"] != null)
            {
                bool loggedIn = (bool)Session["LoggedIn"];
                if (loggedIn)
                {
                    return View();
                }
            }
            return RedirectToAction("Home");
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