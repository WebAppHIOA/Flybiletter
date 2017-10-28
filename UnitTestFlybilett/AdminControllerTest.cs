using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flybiletter.Controllers;
using BLL;
using DAL;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using Model;
using System.Collections.Generic;
using Flybiletter.Models;

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
            controller.ViewData.ModelState.AddModelError("Username", "Ikke oppgitt brukernavn");

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

            var cd = (Dictionary<string, int>)controller.ViewData["CountData"];
            var DepartureCount = cd["Departure"];
            var OrderCount = cd["Order"];
            var AirportCount = cd["Airport"];

            Assert.AreEqual(DepartureCount, 1);
            Assert.AreEqual(OrderCount, 10);
            Assert.AreEqual(AirportCount, 10);
        }

        [TestMethod]
        public void DepartureValidTest()
        {
            var controller = setupController();
            var _admin = new Administrator(new DBstub());
            var departureVM = new Model.AdminDepartureViewModel();
            departureVM.DepartureDetails = (List<Departure>)_admin.GetAllDepartures();
            departureVM.Airport = _admin.GetAllAirports();
            controller.Session["LoggedIn"] = true;

            var resultat = (ViewResult)controller.Departure();
            var resultatListe = (Model.AdminDepartureViewModel)resultat.Model;
            var resultatArray = departureVM.DepartureDetails.ToArray();
            var departureArray = departureVM.DepartureDetails.ToArray();

            Assert.AreEqual(resultat.ViewName, "");
            for (var i = 0; i < departureVM.DepartureDetails.Count; i++)
            {
                Assert.AreEqual(departureArray[i].FlightId, resultatArray[i].FlightId);
                Assert.AreEqual(departureArray[i].Cancelled, departureArray[i].Cancelled);
                Assert.AreEqual(departureArray[i].Airport, resultatArray[i].Airport);
                Assert.AreEqual(departureArray[i].Date, resultatArray[i].Date);
                Assert.AreEqual(departureArray[i].DepartureTime, resultatArray[i].DepartureTime);
                Assert.AreEqual(departureArray[i].From, resultatArray[i].From);
            }
            Assert.AreEqual(departureVM.Airport.Count, resultatListe.Airport.Count);
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
            var controller = setupController();
            controller.Session["LoggedIn"] = true;

            var resultat = (RedirectToRouteResult)controller.UpdateDeparture(_admin.GetDeparture("Test"));
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Departure");
        }

        [TestMethod]
        public void UpdateDepartureInvalidTest()
        {
            var _admin = new Administrator(new DBstub());
            var controller = setupController();
            controller.Session["LoggedIn"] = false;

            var resultat = (RedirectToRouteResult)controller.UpdateDeparture(_admin.GetDeparture("Test"));
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void AirportValidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = true;

            var resultat = (ViewResult)controller.Airport();

            Assert.AreEqual(resultat.ViewName, "");
        }

        [TestMethod]
        public void AirportInValidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = false;

            var resultat = (RedirectToRouteResult)controller.Airport();
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void PostAirportInValidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = true;
            var _admin = new Administrator(new DBstub());

            var resultat = (RedirectToRouteResult)controller.Airport(_admin.GetAirport("Test"));
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Airport");
        }

        [TestMethod]
        public void PostAirportInInvalidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = false;
            var _admin = new Administrator(new DBstub());

            var resultat = (RedirectToRouteResult)controller.Airport(_admin.GetAirport("Test"));
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void DeleteAirportValidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = true;

            var resultat = (RedirectToRouteResult)controller.DeleteAirport("Test");
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Airport");
        }

        [TestMethod]
        public void DeleteAirportInvalidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = false;

            var resultat = (RedirectToRouteResult)controller.DeleteAirport("Test");
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void UpdateAirportValidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = true;

            var resultat = (ViewResult)controller.UpdateAirport("Test");
            Assert.AreEqual(resultat.ViewName, "");
        }

        [TestMethod]
        public void UpdateAirportInvalidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = false;

            var resultat = (RedirectToRouteResult)controller.UpdateAirport("Test");
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void PostUpdateAirportValidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = true;
            var _admin = new Administrator(new DBstub());
            var resultat = (RedirectToRouteResult)controller.UpdateAirport((Airport)_admin.GetAirport("Test"));
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Airport");
        }

        [TestMethod]
        public void PostUpdateAirportInvalidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = false;
            var _admin = new Administrator(new DBstub());
            var resultat = (RedirectToRouteResult)controller.UpdateAirport((Airport)_admin.GetAirport("Test"));
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void OrderValidTest()
        {
            var _admin = new Administrator(new DBstub());
            var CheckOrderVM = new Model.AdminOrderViewModel();
            CheckOrderVM.Order = _admin.GetAllOrders();
            CheckOrderVM.Departure = _admin.GetAllDepartures();

            var controller = setupController();
            controller.Session["LoggedIn"] = true;

            var resultat = (ViewResult)controller.Order();
            Assert.AreEqual(resultat.ViewName, "");

            var resultatVM = (AdminOrderViewModel)resultat.Model;

            Assert.AreEqual(resultatVM.Firstname, CheckOrderVM.Firstname);
            Assert.AreEqual(resultatVM.Surname, CheckOrderVM.Surname);
            Assert.AreEqual(resultatVM.Cancelled, CheckOrderVM.Cancelled);
            Assert.AreEqual(resultatVM.Date, CheckOrderVM.Date);
            Assert.AreEqual(resultatVM.Departure.Count, CheckOrderVM.Departure.Count);
            Assert.AreEqual(resultatVM.Email, CheckOrderVM.Email);
        }

        [TestMethod]
        public void OrderInvalidTest()
        {
            var _admin = new Administrator(new DBstub());
            var CheckOrderVM = new Model.AdminOrderViewModel();
            CheckOrderVM.Order = _admin.GetAllOrders();
            CheckOrderVM.Departure = _admin.GetAllDepartures();

            var controller = setupController();
            controller.Session["LoggedIn"] = false;

            var resultat = (RedirectToRouteResult)controller.Order();
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void PostOrderValidTest()
        {

            var _admin = new Administrator(new DBstub());
            var CheckOrderVM = new Model.AdminOrderViewModel();
            CheckOrderVM.Order = _admin.GetAllOrders();
            CheckOrderVM.Departure = _admin.GetAllDepartures();

            var controller = setupController();
            controller.Session["LoggedIn"] = true;

            var resultat = (RedirectToRouteResult)controller.Order(CheckOrderVM);
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Order");
        }

        [TestMethod]
        public void PostOrderInvalidTest()
        {
            var _admin = new Administrator(new DBstub());
            var CheckOrderVM = new Model.AdminOrderViewModel();
            CheckOrderVM.Order = _admin.GetAllOrders();
            CheckOrderVM.Departure = _admin.GetAllDepartures();

            var controller = setupController();
            controller.Session["LoggedIn"] = false;

            var resultat = (RedirectToRouteResult)controller.Order(CheckOrderVM);
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void DeleteOrderInvalidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = false;

            var resultat = (RedirectToRouteResult)controller.DeleteOrder("Test");
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");

        }

        [TestMethod]
        public void DeleteOrderValidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = true;

            var resultat = (RedirectToRouteResult)controller.DeleteOrder("Test");
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Order");
        }

        [TestMethod]
        public void UpdateOrderValidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = true;
            var _admin = new Administrator(new DBstub());

            var testModel = _admin.GetOrder("Test");
            var resultat = (ViewResult)controller.UpdateOrder("Test");
            Assert.AreEqual(resultat.ViewName, "");
            var restultatModel = (Model.Order)resultat.Model;

            Assert.AreNotEqual(restultatModel.OrderNumber, testModel.OrderNumber);
            Assert.AreEqual(restultatModel.Price, testModel.Price);
        }

        [TestMethod]
        public void UpdateOrderInvalidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = false;

            var resultat = (RedirectToRouteResult)controller.UpdateOrder("Test");
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void PostUpdateOrderValidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = true;
            var testModel = new Model.Order();
            var resultat = (RedirectToRouteResult)controller.UpdateOrder(testModel);
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Order");
        }

        [TestMethod]
        public void PostUpdateOrderInvalidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = false;
            var testModel = new Model.Order();
            var resultat = (RedirectToRouteResult)controller.UpdateOrder(testModel);
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Login");
        }

        [TestMethod]
        public void LogoutValidTest()
        {
            var controller = setupController();
            controller.Session["LoggedIn"] = true;

            var resultat = (RedirectToRouteResult)controller.Logout();
            Assert.AreEqual(resultat.RouteName, "");
            var e = resultat.RouteValues.Values.GetEnumerator();
            e.MoveNext();
            Assert.AreEqual(e.Current, "Index");
        }

    }
}
