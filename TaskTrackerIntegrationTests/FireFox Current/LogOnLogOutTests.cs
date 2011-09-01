using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace TaskTrackerIntegrationTests.FireFoxCurrent
{
    [TestFixture]
    [Category("FireFox")]
    class LogOnLogOutTests : TaskTrackerIntegrationTests.Base.LogOnLogOutTests
    {
        IWebDriver driver;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.browserType = Base.BROWSER_TYPE.FireFoxCurrent;
        }

        [TearDown]
        public void TearDown()
        {
            this.driver.Quit();
        }

        [Test]
        public void HomeIndex_LogIn_LogOut()
        {
            this.driver = new FirefoxDriver();

            base.HomeIndex_LogIn_LogOut(this.driver);
        }

        [Test]
        public void HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication()
        {
            this.driver = new FirefoxDriver();

            base.HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication(this.driver);
        }
    }
}
