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
     * \brief User detail view integration tests for Chrome.
     * \author Katharine Gillis
     * \date 2011-09-10
     * 
     * Runs the user detail view integration tests on the latest Chrome browser.
     */
    [TestFixture]
    [Category("UserDetailView")]
    class UserDetailViewTests : TaskTrackerIntegrationTests.Base.UserDetailViewTests
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
         * \brief Runs the user detail view from navigation bar test with a user that has no roles.
         * 
         * This runs the user detail view from navigation bar test with a user that has no roles on the Chrome browser.
         */
        [Test]
        public void AnyUserWithoutRoles_CanNavigateToOwnUserDetailViewFromNavigationBar()
        {
            this.driver = this.CreateDriver();

            base.AnyUserWithoutRoles_CanNavigateToOwnUserDetailViewFromNavigationBar(this.driver);
        }

        /**
         * \brief Runs the user detail view from navigation bar test with a user that has roles.
         * 
         * This runs the user detail view from navigation bar test with a user that has roles on the Chrome browser.
         */
        [Test]
        public void AnyUserWithRoles_CanNavigateToOwnUserDetailViewFromNavigationBar()
        {
            this.driver = this.CreateDriver();

            base.AnyUserWithRoles_CanNavigateToOwnUserDetailViewFromNavigationBar(this.driver);
        }
    }
}
