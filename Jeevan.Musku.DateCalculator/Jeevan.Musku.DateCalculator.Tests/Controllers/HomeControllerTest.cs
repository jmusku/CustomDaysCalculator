using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jeevan.Musku.DateCalculator;
using Jeevan.Musku.DateCalculator.Controllers;
using Jeevan.Musku.DateCalculator.Models;

namespace Jeevan.Musku.DateCalculator.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [AcceptVerbs(HttpVerbs.Post)]
        public void IndexPostTest()
        {
            // Arrange
            HomeController controller = new HomeController();
            var date = new CustomDate("20/02/2010", "19/06/2019");

            // Act
            ViewResult result = (ViewResult) controller.Index(date);

            // Assert
            if (result != null) Assert.AreEqual("Total number of days is 3405", (string) controller.ViewBag.message);
        }
    }
}
