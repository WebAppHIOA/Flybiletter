using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flybiletter.Controllers;
using BLL;
using DAL;
using System.Web.Mvc;

namespace UnitTestFlybilett
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            var controller = new HomeController(new Order( new DBstub()));

            var resultat = (ViewResult)controller.Index();

            // Assert
            Assert.AreEqual(resultat.ViewName,"");
        }


    }
}
