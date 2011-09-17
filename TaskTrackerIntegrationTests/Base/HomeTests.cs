using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;

namespace TaskTrackerIntegrationTests.Base
{
    /**
     * \brief Integration tests for the Home page.
     * \author Katharine Gillis
     * \date 2011-09-10
     * 
     * Defines the integration tests run for the Home page.
     */
    abstract class HomeTests : BasicTestFixture
    {
        /**
         * \brief Default url test.
         * 
         * Tests that the default url properly goes to /Home/Index.
         * 
         * Steps:
         *  -# Navigate to http://localhost:8085.
         *  -# The page title should be "Task Tracker - Home".
         *  
         * \param driver The browser driver to use.
         */
        protected void DefaultURL_RoutesToHomeIndex(IWebDriver driver)
        {
            // Navigate to the base url.
            driver.Navigate().GoToUrl("http://localhost:8085/");
            Wait();

            // Assert that the title is that of the Home/Index page and that the welcome message appears.
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - Home"));
            IWebElement welcomeMessage = driver.FindElement(By.XPath("//span[@welcome=''][text()='Welcome to Task Tracker!']"));
        }
    }
}
