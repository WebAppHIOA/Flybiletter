using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flybiletter.Controllers;
using BLL;
using DAL;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using Model;
using System.Collections.Generic;

namespace UnitTestFlybilett
{
    [TestClass]
    public class AdminControllerTest
    {
        public AdminController setupController()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new Administrator(new DBstub()));
            SessionMock.InitializeController(controller);
            return controller;
        }

        [TestMethod]
        public void LoginTest()
        {
            var controller = setupController();

            controller.Session["LoggedIn"] = true;
            var resultat = (ViewResult)controller.Login();
            // Assert
            Assert.AreEqual(resultat.ViewName, "");
        }

        [TestMethod]
        public void LoginPostInvalidTest()
        {
            var controller = setupController();

            //controller.ModelState.AddModelError("test", "test");
            var adminTest = new Login
            {
                Username = "airzureadmin",
                Password = "FailPasswordTest"
            };

            var resultat = (RedirectToRouteResult)controller.Login(adminTest);
            // Assert
            Assert.AreEqual(resultat.RouteName, "");
        }

        [TestMethod]
        public void LoginPostValidTest()
        {
            var controller = setupController();

            //controller.ModelState.AddModelError("test", "test");
            var adminTest = new Login
            {
                Username = "airzureadmin",
                Password = "passord"
            };

            var resultat = (RedirectToRouteResult)controller.Login(adminTest);
            // Assert
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Home");
        }

        [TestMethod]
        public void LoginPostModelStateTest()
        {
            var controller = setupController();
            controller.ViewData.ModelState.AddModelError("Username","Ikke oppgitt brukernavn");

            var adminTest = new Login
            {
                Username = "airzureadmin",
                Password = "passord"
            };
            var resultat = (ViewResult)controller.Login(adminTest);

            Assert.IsTrue(resultat.ViewData.ModelState.Count == 1);
            Assert.AreEqual(resultat.ViewName, "");
        }

        [TestMethod]
        public void HomeInvalidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = false;

            var resultat = (RedirectToRouteResult)controller.Home();
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void HomeValidTest()
        {
            var _admin = new Administrator(new DBstub());
            var controller = setupController();
            controller.Session["LoggedIn"] = true;
            controller.ViewData["CountData"] = _admin.TableCounts();

            var resultat = (ViewResult)controller.Home();
            Assert.AreEqual(resultat.ViewName, "");
            Assert.AreEqual(controller.ViewData["CountData"], 1);
        }

        [TestMethod]
        public void DepartureValidTest()
        {
            var controller = setupController();
            var _admin = new Administrator(new DBstub());
            var departureListe = (List<Departure>)_admin.GetAllDepartures();
            controller.Session["LoggedIn"] = true;
            var resultat = (ViewResult)controller.Departure();
            var resultatListe = (List<Departure>)resultat.Model;


            Assert.AreEqual(resultat.ViewName, "");
            for (var i = 0; i < departureListe.Count; i++)
            {
                Assert.AreEqual(departureListe[i].FlightId, resultatListe[i].FlightId);
                Assert.AreEqual(departureListe[i].Cancelled, resultatListe[i].Cancelled);
                Assert.AreEqual(departureListe[i].Airport, resultatListe[i].Airport);
                Assert.AreEqual(departureListe[i].Date, resultatListe[i].Date);
                Assert.AreEqual(departureListe[i].DepartureTime, resultatListe[i].DepartureTime);
                Assert.AreEqual(departureListe[i].DepartureTime, resultatListe[i].DepartureTime);
                Assert.AreEqual(departureListe[i].From, resultatListe[i].From);
            }
            
        }

        [TestMethod]
        public void DepartureInvalidTest()
        {
            var controller = setupController();
            var _admin = new Administrator(new DBstub());
            var departureListe = (List<Departure>)_admin.GetAllDepartures();
            controller.Session["LoggedIn"] = false;
            var resultat = (RedirectToRouteResult)controller.Departure();

            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void DeleteDepartureValidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = true;
            var resultat = (RedirectToRouteResult)controller.DeleteDeparture("Test");
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Departure");
        }

        [TestMethod]
        public void DeleteDepartureInvalidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = false;
            var resultat = (RedirectToRouteResult)controller.DeleteDeparture("Test");
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void UpdateDepartureValidTest()
        {
            var _admin = new Administrator(new DBstub());
            var expectedModel = _admin.UpdateDeparture("Test");
            var controller = setupController();
            controller.Session["LoggedIn"] = true;
            var resultat = (ViewResult)controller.UpdateDeparture("Test");
            var resultatModel = (Departure)resultat.Model;

            Assert.AreEqual(resultatModel.FlightId, resultatListe[i].FlightId);
            Assert.AreEqual(resultatModel.Cancelled, resultatListe[i].Cancelled);
            Assert.AreEqual(resultatModel.Airport, resultatListe[i].Airport);
            Assert.AreEqual(resultatModel.Date, resultatListe[i].Date);
            Assert.AreEqual(resultatModel.DepartureTime, resultatListe[i].DepartureTime);
            Assert.AreEqual(resultatModel.DepartureTime, resultatListe[i].DepartureTime);
            Assert.AreEqual(resultatModel.From, resultatListe[i].From);
        }


    }
}
