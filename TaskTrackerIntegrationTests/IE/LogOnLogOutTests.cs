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

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.browserType = Base.BROWSER_TYPE.InternetExplorer;
        }

        [TearDown]
        public void TearDown()
        {
            this.driver.Quit();
        }

        [Test]
        public void HomeIndex_LogIn_LogOut()
        {
            this.driver = this.CreateDriver();

            base.HomeIndex_LogIn_LogOut(this.driver);
        }

        [Test]
        public void HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication()
        {
            this.driver = this.CreateDriver();

            base.HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication(this.driver);
        }
    }
}
