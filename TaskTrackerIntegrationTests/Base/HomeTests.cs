using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;

namespace TaskTrackerIntegrationTests.Base
{
    abstract class HomeTests : BasicTestFixture
    {
        protected void DefaultURL_RoutesToHomeIndex(IWebDriver driver)
        {
            // Navigate to the base url.
            driver.Navigate().GoToUrl("http://localhost:8085/");
            Wait();

            // Assert that the title is that of the Home/Index page.
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - Home"));
        }
    }
}
