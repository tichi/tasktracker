using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;

namespace TaskTrackerIntegrationTests
{
    abstract class HomeTests
    {
        protected void DefaultURL_RoutesToHomeIndex(IWebDriver driver)
        {
            // Arrange and Act
            driver.Navigate().GoToUrl("http://localhost:8085/");

            // Assert
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - Home"));
        }
    }
}
