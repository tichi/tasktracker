using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace TaskTrackerIntegrationTests.FireFox3_6
{
    [TestFixture]
    [Category("FireFox")]
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
            FirefoxBinary binary = new FirefoxBinary("C:\\Program Files\\Mozilla Firefox 3.6\\firefox.exe");
            FirefoxProfile profile = new FirefoxProfile();
            driver = new FirefoxDriver(binary, profile);

            base.HomeIndex_LogIn_LogOut(driver);
        }

        [Test]
        public void HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication()
        {
            FirefoxBinary binary = new FirefoxBinary("C:\\Program Files\\Mozilla Firefox 3.6\\firefox.exe");
            FirefoxProfile profile = new FirefoxProfile();
            driver = new FirefoxDriver(binary, profile);

            base.HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication(driver);
        }
    }
}
