using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace TaskTrackerIntegrationTests.FireFoxCurrent
{
    /**
     * \brief Home page integration tests for the latest version of FireFox.
     * \author Katharine Gillis
     * \date 2011-09-10
     * 
     * Runs the Home page integration tests on the latest version of FireFox browser.
     */
    [TestFixture]
    [Category("Home")]
    class HomeTests : TaskTrackerIntegrationTests.Base.HomeTests
    {
        IWebDriver driver;

        /**
         * \brief Sets the browser type.
         * 
         * This sets the browser type to the latest version of FireFox for use with all the tests in this test fixture.
         */
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.browserType = Base.BROWSER_TYPE.FireFoxCurrent;
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
         * This runs the default url test with the latest FireFox browser.
         */
        [Test]
        public void DefaultURL_RoutesToHomeIndex()
        {
            this.driver = new FirefoxDriver();

            base.DefaultURL_RoutesToHomeIndex(this.driver);
        }
    }
}
