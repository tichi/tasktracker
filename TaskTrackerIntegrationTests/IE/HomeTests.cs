using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace TaskTrackerIntegrationTests.IE
{
    [TestFixture]
    [Category("IE")]
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
            driver = new InternetExplorerDriver();

            base.DefaultURL_RoutesToHomeIndex(driver);
        }
    }
}
