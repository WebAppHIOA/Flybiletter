using Flybiletter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Flybiletter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*TODO VALIDERING
            if (ModelState.IsValid)
            {
                return RedirectToAction("");
            }*/
            var db = new DB();
            List<Airport> allAirports = db.getAllAirports();

            /*
            Departure dep = new Departure
            {
                FlightId = "TestId",
                From = "Amsterdam",
                To = "Oslo",
                Date = "21.12",
                DepartureTime = "20.30",
                Airport = allAirports[0]
            };

            db.AddDeparture(dep);

            Order ord = new Order();
            ord.OrderNumber = UniqueReference();
            ord.Date = "23.09.2018";

            ord.PassengerCount = "2";
            ord.Price = "3223";

            db.AddOrder(ord);

            Customer pass = new Customer();
            pass.CustomerId = "123";
            pass.Firstname = "Olga";
            pass.Surname = "Olgson";
            pass.Tlf = 12345678;
            pass.Email = "katrinealmas@gmail.com";

            pass.Order = ord;
            pass.Departure = dep;
            db.AddCustomer(pass);
            */

            return View(allAirports);
        }

        public ActionResult Test()
        {
            var db = new DB();
            var objekt = db.FindDeparture("TestId");
            String test = objekt.Airport.City;
            return View(test);
        }


        public ActionResult Order(Order order)
        {

            return View();
        }

        public ActionResult TestEmail()
        {
            GenerateInvoice invoice = new GenerateInvoice();
            invoice.SendEmail();
            /*   MailMessage mail = new MailMessage();
               mail.From = new System.Net.Mail.MailAddress("katrinealmastest@gmail.com");

               // The important part -- configuring the SMTP client
               SmtpClient smtp = new SmtpClient();
               smtp.Port = 587;   // [1] You can try with 465 also, I always used 587 and got success
               smtp.EnableSsl = true;
               smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this
               smtp.UseDefaultCredentials = false; // [3] Changed this
               smtp.Credentials = new NetworkCredential("katrinealmastest@gmail.com", "K2s0G1a7");  // [4] Added this. Note, first parameter is NOT string.
               smtp.Host = "smtp.gmail.com";

               //recipient address
               mail.To.Add(new MailAddress("katrinealmas@gmail.com"));

               //Formatted mail body
               mail.IsBodyHtml = true;
               string st = "Prøver nok en gang";

               mail.Body = st;
               smtp.Send(mail); */

            return View();
        }

        public ActionResult FlightDetails()
        {

            /*   Dictionary<string, string> form = new Dictionary<string, string>();
               foreach (string key in Request.Form.AllKeys)
                   form.Add(key, Request.Form[key]);
               Session["Index"] = form;*/

            Session["From"] = Request.Form["from"];
            Session["To"] = Request.Form["to"];
            Session["Date"] = Request.Form["avreise"];

            return RedirectToAction("Index", "Departure");

            // return View();
        }

        /*
                [HttpPost]
                public ActionResult FlightDetails()
                {
                    Dictionary<string, string> form = new Dictionary<string, string>();
                    foreach (string key in Request.Form.AllKeys)
                        form.Add(key, Request.Form[key]);
                    Session["Index"] = form;

                    return RedirectToAction("Index", "Departure");
                }
                */

        public ActionResult Passenger()
        {
            

            return View();

        }


        public ActionResult Confirmation()
        {
            return View();
        }

        /* Hjelpe metode for å generere unike referanser til feks orderNumber.
        *  Legges her inntil videre da man trenger å lagre referansen dersom man skal søke spesifikt i db.
        */
        private string UniqueReference()
        {
            var guid = System.Guid.NewGuid().ToString();

            return guid;
        }

    }
}