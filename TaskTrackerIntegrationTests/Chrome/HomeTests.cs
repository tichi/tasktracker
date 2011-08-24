using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TaskTrackerIntegrationTests.Chrome
{
    [TestFixture]
    [Category("Chrome")]
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
            driver = new ChromeDriver();

            base.DefaultURL_RoutesToHomeIndex(driver);
        }
    }
}
