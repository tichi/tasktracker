using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TaskTrackerIntegrationTests.InternetExplorer
{
    /**
     * \brief User detail view integration tests for Internet Explorer 8.0 browser.
     * \author Katharine Gillis
     * \date 2011-09-10
     * 
     * Runs the user detail view integration tests on Internet Explorer 8.0 browser.
     */
    [TestFixture]
    [Category("UserDetailView")]
    class UserDetailViewTests : TaskTrackerIntegrationTests.Base.UserDetailViewTests
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
         * \brief Runs the user detail view from navigation bar test with a user that has no roles.
         * 
         * This runs the user detail view from navigation bar test with a user that has no roles on Internet Explorer 8.0 browser.
         */
        [Test]
        public void AnyUser_CanNavigateToOwnUserDetailViewFromNavigationBar()
        {
            this.driver = this.CreateDriver();
            base.EnsureLoggedOut(this.driver);
            base.AnyUser_CanNavigateToOwnUserDetailViewFromNavigationBar(this.driver);
        }

        /**
         * \brief Runs the user detail view from navigation bar test with a user that has roles.
         * 
         * This runs the user detail view from navigation bar test with a user that has roles on Internet Explorer 8.0 browser.
         */
        [Test]
        public void AnyUser_CanNavigateToAnyOtherUserDetailView()
        {
            this.driver = this.CreateDriver();
            base.EnsureLoggedOut(this.driver);
            base.AnyUser_CanNavigateToAnyOtherUserDetailView(this.driver);
        }
    }
}
