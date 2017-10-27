using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flybiletter.Controllers;
using BLL;
using DAL;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using Model;

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

        
    }
}
