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
            FirefoxBinary binary = new FirefoxBinary("C:\\Program Files\\Mozilla Firefox 3.6\\firefox.exe");
            FirefoxProfile profile = new FirefoxProfile();
            driver = new FirefoxDriver(binary, profile);

            base.DefaultURL_RoutesToHomeIndex(driver);
        }
    }
}
