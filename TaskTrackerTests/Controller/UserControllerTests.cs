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
    [TestFixture]
    class UserControllerTests
    {
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
