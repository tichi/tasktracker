using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TaskTrackerIntegrationTests.FireFox5
{
    /**
     * \brief User detail view integration tests for FireFox 5.0.
     * \author Katharine Gillis
     * \date 2011-09-10
     * 
     * Runs the user detail view integration tests on the FireFox 5.0 browser.
     */
    [TestFixture]
    [Category("UserDetailView")]
    class UserDetailViewTests : TaskTrackerIntegrationTests.Base.UserDetailViewTests
    {
        IWebDriver driver;

        /**
         * \brief Sets the browser type.
         * 
         * This sets the browser type to FireFox 5.0 for use with all the tests in this test fixture.
         */
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.browserType = Base.BROWSER_TYPE.FireFox5;
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
         * This runs the user detail view from navigation bar test with a user that has no roles on the FireFox 5.0 browser.
         */
        [Test]
        public void AnyUser_CanNavigateToOwnUserDetailViewFromNavigationBar()
        {
            this.driver = this.CreateDriver();

            base.AnyUser_CanNavigateToOwnUserDetailViewFromNavigationBar(this.driver);
        }

        /**
         * \brief Accesses the user detail view of another user.
         * 
         * Accesses the user detail view of another user on the Firefox 5.0 browser.
         */
        [Test]
        public void AnyUser_CanNavigateToAnyOtherUserDetailView()
        {
            this.driver = this.CreateDriver();

            base.AnyUser_CanNavigateToAnyOtherUserDetailView(this.driver);
        }

        /**
         * \brief Unauthenticated users are redirected to the Log On page when accessing a user detail view.
         * 
         * Unauthenticated users are redirected to the Log On page when accessing a user detail view on FireFox 5.0.
         */
        [Test]
        public void UnauthenticatedUser_IsRedirectedToLogOn()
        {
            this.driver = this.CreateDriver();

            base.UnauthenticatedUser_IsRedirectedToLogOn(this.driver);
        }

        /**
         * \brief Attempting to access the user detail view for an id that doesn't exist results in a user-friendly error.
         * 
         * When logged in, the user sees a user-friendly error message of "This record does not exist." when attempting to access the
         * detail view of a user with an id that doesn't exist in the database or can't be parsed into a guid.
         */
        [Test]
        public void AnyUser_NavigatingToUserDetailWithUnknownId_ShowsUserFriendlyError()
        {
            this.driver = this.CreateDriver();

            base.AnyUser_NavigatingToUserDetailWithUnknownId_ShowsUserFriendlyError(driver);
        }
    }
}
