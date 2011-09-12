using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

using Moq;

using NUnit.Framework;

using TaskTracker.Controllers;
using TaskTracker.Models.ViewModels;

namespace TaskTrackerTests.Controller
{
    /**
     * \brief Unit tests for the UserController.
     * \author Katharine Gillis
     * \2011-09-11
     * 
     * Defines the unit tests for the UserController class.
     */
    [TestFixture]
    class UserControllerTests
    {
        /**
         * \brief Test for the LogOn method returning the LogOn View.
         * 
         * Defines the unit test that runs the LogOn method, expecting the LogOn View to be returned.
         */
        [Test]
        public void LogOn_Get_ReturnsLogOnView()
        {
            // Arrange
            UserController controller = new UserController();

            // Act
            ActionResult result = controller.LogOn();

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));

            ViewResult viewResult = result as ViewResult;

            Assert.That(viewResult.ViewName, Is.EqualTo("LogOn"));
            Assert.That(viewResult.ViewData.ModelState.IsValid, Is.True);
        }

        /**
         * \brief Test for the LogOn method returning the LogOn View with model errors.
         * 
         * Defines the unit test that runs the LogOn method with an invalid model, expecting the LogOn view to be returned with model errors.
         */
        [Test]
        public void LogOn_PostWithInvalidModelState_ReturnsLogOnViewWithModelErrors()
        {
            // Arrange
            UserLogOnViewModel model = new UserLogOnViewModel();
            UserController controller = new UserController();
            controller.ModelState.Clear();
            controller.ModelState.AddModelError("UserName", "The field UserName is required.");
            controller.ModelState.AddModelError("Password", "The field Password is required.");

            // Act
            ActionResult result = controller.LogOn(model);

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));

            ViewResult viewResult = result as ViewResult;

            Assert.That(viewResult.ViewName, Is.EqualTo("LogOn"));
            Assert.That(viewResult.ViewData.ModelState.IsValid, Is.False);
        }

        /**
         * \brief Test for the LogOn method returning the LogOn View with model errors.
         * 
         * Defines the unit test that runs the LogOn method with an invalid user, expecting the LogOn view to be returned with model errors.
         */
        [Test]
        public void LogOn_PostWithInvalidUserNameValidPassword_ReturnsLogOnViewWithModelErrors()
        {
            // Arrange
            UserLogOnViewModel model = new UserLogOnViewModel
            {
                UserName = "invaliduser",
                Password = "password"
            };
            Mock<IFormsAuthentication> mockIFormsAuthentication = new Mock<IFormsAuthentication>();

            Mock<IMembershipService> mockIMembershipService = new Mock<IMembershipService>();
            mockIMembershipService.Setup(m => m.ValidateUser("invaliduser", "password")).Returns(false);

            UserController controller = new UserController(mockIFormsAuthentication.Object, mockIMembershipService.Object);
            controller.ModelState.Clear();

            // Act
            ActionResult result = controller.LogOn(model);

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));

            ViewResult viewResult = result as ViewResult;

            Assert.That(viewResult.ViewName, Is.EqualTo("LogOn"));
            Assert.That(viewResult.ViewData.ModelState.IsValid, Is.False);
        }

        /**
         * \brief Test for the LogOn method returning the LogOn view with model errors.
         * 
         * Defines the unit test that runs the LogOn method with a valid user and an invalid password, expecting the LogOn view to be returned with model errors.
         */
        [Test]
        public void LogOn_PostWithValidUserNameInvalidPassword_ReturnsLogOnViewWithModelErrors()
        {
            // Arrange
            UserLogOnViewModel model = new UserLogOnViewModel
            {
                UserName = "testuser",
                Password = "invalidpassword"
            };

            Mock<IFormsAuthentication> mockIFormsAuthentication = new Mock<IFormsAuthentication>();

            Mock<IMembershipService> mockIMembershipService = new Mock<IMembershipService>();
            mockIMembershipService.Setup(m => m.ValidateUser("testuser", "invalidpassword")).Returns(false);

            UserController controller = new UserController(mockIFormsAuthentication.Object, mockIMembershipService.Object);
            controller.ModelState.Clear();

            // Act
            ActionResult result = controller.LogOn(model);

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));

            ViewResult viewResult = result as ViewResult;

            Assert.That(viewResult.ViewName, Is.EqualTo("LogOn"));
            Assert.That(viewResult.ViewData.ModelState.IsValid, Is.False);
        }

        /**
         * \brief Test for the LogOn method redirecting to the default url.
         * 
         * Defines the unit test that runs the LogOn method with a valid user and a valid password, expecting a redirect to the default url.
         */
        [Test]
        public void LogOn_PostWithValidUserAndPassword_RedirectsToDefault()
        {
            // Arrange
            UserLogOnViewModel model = new UserLogOnViewModel
            {
                UserName = "testuser",
                Password = "password"
            };

            Mock<IFormsAuthentication> mockIFormsAuthentication = new Mock<IFormsAuthentication>();

            Mock<IMembershipService> mockIMembershipService = new Mock<IMembershipService>();
            mockIMembershipService.Setup(m => m.ValidateUser("testuser", "password")).Returns(true);

            UserController controller = new UserController(mockIFormsAuthentication.Object, mockIMembershipService.Object);
            controller.ModelState.Clear();

            // Act
            ActionResult result = controller.LogOn(model);

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(RedirectResult)));

            RedirectResult redirectResult = result as RedirectResult;

            Assert.That(redirectResult.Permanent, Is.False);
            Assert.That(redirectResult.Url, Is.EqualTo("~/"));
        }

        /**
         * \brief Test for the LogOff method redirecting to the default url.
         * 
         * Defines the unit test that runs the LogOff method when a user is not authenticated, expecting a redirect to the default url.
         */
        [Test]
        public void LogOff_GetWithUnauthenticatedUser_RedirectsToDefault()
        {
            // Arrange
            Mock<IFormsAuthentication> mockIFormsAuthentication = new Mock<IFormsAuthentication>();
            Mock<IMembershipService> mockIMembershipService = new Mock<IMembershipService>();
            UserController controller = new UserController(mockIFormsAuthentication.Object, mockIMembershipService.Object);

            Mock<HttpContextBase> mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(c => c.User.Identity.IsAuthenticated).Returns(false);
            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), controller);

            // Act
            ActionResult result = controller.LogOff();

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(RedirectResult)));

            RedirectResult redirectResult = result as RedirectResult;

            Assert.That(redirectResult.Permanent, Is.False);
            Assert.That(redirectResult.Url, Is.EqualTo("~/"));
        }

        /**
         * \brief Test for the LogOff method redirecting to the default url.
         * 
         * Defines the unit test that runs the LogOff method when a user is authenticated, expecting a redirect to the default url.
         */
        [Test]
        public void LogOff_GetWithAuthenticatedUser_RedirectsToDefault()
        {
            // Arrange
            Mock<IFormsAuthentication> mockIFormsAuthentication = new Mock<IFormsAuthentication>();
            Mock<IMembershipService> mockIMembershipService = new Mock<IMembershipService>();
            UserController controller = new UserController(mockIFormsAuthentication.Object, mockIMembershipService.Object);

            Mock<HttpContextBase> mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(c => c.User.Identity.IsAuthenticated).Returns(true);
            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), controller);

            // Act
            ActionResult result = controller.LogOff();

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(RedirectResult)));

            RedirectResult redirectResult = result as RedirectResult;

            Assert.That(redirectResult.Permanent, Is.False);
            Assert.That(redirectResult.Url, Is.EqualTo("~/"));
        }
    }
}
