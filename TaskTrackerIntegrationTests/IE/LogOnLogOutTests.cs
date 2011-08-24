using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace TaskTrackerIntegrationTests.IE
{
    [TestFixture]
    [Category("IE")]
    class LogOnLogOutTests : TaskTrackerIntegrationTests.Base.LogOnLogOutTests
    {
        IWebDriver driver;

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void HomeIndex_LogIn_LogOut()
        {
            driver = new InternetExplorerDriver();

            base.HomeIndex_LogIn_LogOut(driver);
        }

        [Test]
        public void HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication()
        {
            driver = new InternetExplorerDriver();

            base.HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication(driver);
        }
    }
}
