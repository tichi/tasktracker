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
using TaskTracker.Models.Domain;
using TaskTracker.Models.ViewModels;
using TaskTracker.Utils;

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
            Mock<IAuthentication> mockIAuthentication = new Mock<IAuthentication>();

            Mock<IMembershipService> mockIMembershipService = new Mock<IMembershipService>();
            mockIMembershipService.Setup(m => m.ValidateUser("invaliduser", "password")).Returns(false);

            UserController controller = new UserController(mockIAuthentication.Object, mockIMembershipService.Object);
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

            Mock<IAuthentication> mockIAuthentication = new Mock<IAuthentication>();

            Mock<IMembershipService> mockIMembershipService = new Mock<IMembershipService>();
            mockIMembershipService.Setup(m => m.ValidateUser("testuser", "invalidpassword")).Returns(false);

            UserController controller = new UserController(mockIAuthentication.Object, mockIMembershipService.Object);
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

            Mock<IAuthentication> mockIAuthentication = new Mock<IAuthentication>();

            Mock<IMembershipService> mockIMembershipService = new Mock<IMembershipService>();
            mockIMembershipService.Setup(m => m.ValidateUser("testuser", "password")).Returns(true);

            UserController controller = new UserController(mockIAuthentication.Object, mockIMembershipService.Object);
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
            Mock<IAuthentication> mockIAuthentication = new Mock<IAuthentication>();
            Mock<IMembershipService> mockIMembershipService = new Mock<IMembershipService>();
            UserController controller = new UserController(mockIAuthentication.Object, mockIMembershipService.Object);

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
            Mock<IAuthentication> mockIAuthentication = new Mock<IAuthentication>();
            Mock<IMembershipService> mockIMembershipService = new Mock<IMembershipService>();
            UserController controller = new UserController(mockIAuthentication.Object, mockIMembershipService.Object);

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

        /**
         * \brief Test that the Detail method throws a NoSuchRecordException with no id.
         * 
         * Defines the unit test that runs the Detail method with no id, expecting a NoSuchRecordException to be thrown.
         */
        [Test]
        public void Detail_GetWithNoId_ThrowsNoSuchRecordException()
        {
            // Arrange
            Mock<IAuthentication> mockIAuthentication = new Mock<IAuthentication>();
            Mock<IMembershipService> mockIMembershipService = new Mock<IMembershipService>();
            UserController controller = new UserController(mockIAuthentication.Object, mockIMembershipService.Object);

            Mock<HttpContextBase> mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(c => c.User.Identity.IsAuthenticated).Returns(true);
            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), controller);

            // Act and assert.
            Assert.Throws<NoSuchRecordException>(() => controller.Detail(null));
        }

        /**
         * \brief Test that the Detail method returns a ViewResult.
         * 
         * Defines the unit test that runs the Detail method, expecting a ViewResult with the requested user's information.
         */
        [Test]
        public void Detail_GetWithId_ReturnsViewResult()
        {
            // Arrange
            Mock<IAuthentication> mockIAuthentication = new Mock<IAuthentication>();
            Mock<IMembershipService> mockIMembershipService = new Mock<IMembershipService>();

            Mock<IUser> mockIUser = new Mock<IUser>();
            mockIUser.Setup(m => m.UserName).Returns("testuser");
            mockIUser.Setup(m => m.Email).Returns("testuser@test.com");
            mockIUser.Setup(m => m.FirstName).Returns("test");
            mockIUser.Setup(m => m.LastName).Returns("user");
            mockIUser.Setup(m => m.TimeZone).Returns("US Mountain Standard Time");
            mockIUser.Setup(m => m.Id).Returns("11111111-1111-1111-1111-111111111111");

            mockIMembershipService.Setup(m => m.GetUser("11111111-1111-1111-1111-111111111111", false)).Returns(mockIUser.Object);

            UserController controller = new UserController(mockIAuthentication.Object, mockIMembershipService.Object);

            Mock<HttpContextBase> mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(c => c.User.Identity.IsAuthenticated).Returns(true);
            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), controller);

            UserDetailViewModel model = new UserDetailViewModel()
            {
                UserName = "testuser",
                Email = "testuser@test.com",
                FirstName = "test",
                LastName = "user",
                TimeZone = "(UTC-07:00) Arizona",
                Id = "11111111-1111-1111-1111-111111111111"
            };

            // Act
            ActionResult result = controller.Detail("11111111-1111-1111-1111-111111111111");

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));

            ViewResult viewResult = result as ViewResult;

            Assert.That(viewResult.ViewName, Is.EqualTo("Detail"));

            Assert.That(viewResult.Model, Is.InstanceOf(typeof(UserDetailViewModel)));

            UserDetailViewModel userDetailViewModel = viewResult.Model as UserDetailViewModel;
            Assert.That(userDetailViewModel.UserName, Is.EqualTo(model.UserName));
            Assert.That(userDetailViewModel.Email, Is.EqualTo(model.Email));
            Assert.That(userDetailViewModel.FirstName, Is.EqualTo(model.FirstName));
            Assert.That(userDetailViewModel.LastName, Is.EqualTo(model.LastName));
            Assert.That(userDetailViewModel.TimeZone, Is.EqualTo(model.TimeZone));
        }
    }
}
