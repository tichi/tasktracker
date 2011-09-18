using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using NUnit.Framework;

using TaskTracker.Controllers;

namespace TaskTrackerTests.Controller
{
    /**
     * \brief Unit tests for the HomeController class.
     * \author Katharine Gillis
     * \date 2011-09-10
     * 
     * Defines the unit tests for the HomeController class.
     */
    [TestFixture]
    class HomeControllerTests
    {
        /**
         * \brief Test for the Index method returning the Index view.
         * 
         * Defines the unit test that runs the Index method, expecting the Index view to be returned.
         */
        [Test]
        public void Index_Get_ReturnsIndexView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ActionResult result = controller.Index();

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));

            ViewResult viewResult = result as ViewResult;

            Assert.That(viewResult.ViewName, Is.EqualTo("Index"));
        }
    }
}
