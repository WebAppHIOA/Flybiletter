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
        public ActionResult Index()
        {
            var adminBLL = new Administrator();
            adminBLL.UpdateAirport(new Model.Airport
            {
                AirportId = "CAN",
                City = "Cancon",
                Country = "Mexico",
                Continent = "North-America",
                Fee = "23.45"
            });
            return View();
        }
    }
}