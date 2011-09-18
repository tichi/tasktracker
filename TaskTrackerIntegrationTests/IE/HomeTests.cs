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
    /**
     * \brief Home page integration tests for Internet Explorer 8.0.
     * \author Katharine Gillis
     * \date 2011-09-10
     * 
     * Runs the Home page integration tests on the Internet Explorer 8.0 browser.
     */
    [TestFixture]
    [Category("Home")]
    class HomeTests : TaskTrackerIntegrationTests.Base.HomeTests
    {
        IWebDriver driver;

        /**
         * \brief Sets the browser type.
         * 
         * This sets the browser type to Internet Explorer 8.0 for use with all the tests in this test fixture.
         */
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.browserType = Base.BROWSER_TYPE.InternetExplorer;
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
         * This runs the default url test with the Internet Explorer 8.0 browser.
         */
        [Test]
        public void DefaultURL_RoutesToHomeIndex()
        {
            this.driver = this.CreateDriver();
            base.EnsureLoggedOut(this.driver);
            base.DefaultURL_RoutesToHomeIndex(this.driver);
        }
    }
}
