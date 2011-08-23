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
    class HomeTests : TaskTrackerIntegrationTests.HomeTests
    {
        IWebDriver driver;

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void DefaultURL_RoutesToHomeIndex()
        {
            driver = new FirefoxDriver();

            base.DefaultURL_RoutesToHomeIndex(driver);
        }
    }
}
