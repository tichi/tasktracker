using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TaskTrackerIntegrationTests.Chrome
{
    /**
     * \brief Home page integration tests for Chrome.
     * \author Katharine Gillis
     * \date 2011-09-10
     * 
     * Runs the Home page integration tests on the latest Chrome browser.
     */
    [TestFixture]
    [Category("Chrome")]
    class HomeTests : TaskTrackerIntegrationTests.Base.HomeTests
    {
        IWebDriver driver;

        /**
         * \brief Sets the browser type.
         * 
         * This sets the browser type to the latest version of Chrome for use with all the tests in this test fixture.
         */
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.browserType = Base.BROWSER_TYPE.Chrome;
        }

        /**
         * \brief Closes the browser.
         * 
         * This closes the browser driver once the test has been run.
         */
        [TearDown]
        public void TearDown()
        {
            this.driver.Quit();
        }

        /**
         * \brief Runs the default url test.
         * 
         * This runs the default url test with the Chrome browser.
         */
        [Test]
        public void DefaultURL_RoutesToHomeIndex()
        {
            this.driver = this.CreateDriver();

            base.DefaultURL_RoutesToHomeIndex(this.driver);
        }
    }
}
