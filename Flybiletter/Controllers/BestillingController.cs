using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flybiletter.Models;

namespace Flybiletter.Controllers
{
    public class BestillingController : Controller
    {
        // GET: Bestilling
        public ActionResult Bestilling()
        {
            var flyselskap = new Avgang() { Flyselskap = "KLM" };
            return View();
        }
    }
}