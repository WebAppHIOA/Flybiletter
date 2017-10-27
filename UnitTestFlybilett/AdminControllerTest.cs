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
        [TestMethod]
        public void LoginTest()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new Administrator(new DBstub()));
            SessionMock.InitializeController(controller);

            controller.Session["LoggedIn"] = true;
            var resultat = (ViewResult)controller.Login();
            // Assert
            Assert.AreEqual(resultat.ViewName, "");
        }

        [TestMethod]
        public void LoginPostTest()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new Administrator(new DBstub()));
            SessionMock.InitializeController(controller);

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
    }
}
