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
     * \brief Authentication integration tests for the latest version of FireFox.
     * \author Katharine Gillis
     * \date 2011-09-10
     * 
     * Runs the authentication integration tests on the latest FireFox browser.
     */
    [TestFixture]
    [Category("LogOnLogOff")]
    class LogOnLogOutTests : TaskTrackerIntegrationTests.Base.LogOnLogOutTests
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
         * \brief Runs the valid log in and log out test.
         * 
         * This runs the valid log in and log out test on the latest FireFox browser.
         */
        [Test]
        public void HomeIndex_LogIn_LogOut()
        {
            this.driver = new FirefoxDriver();

            base.HomeIndex_LogIn_LogOut(this.driver);
        }

        /**
         * \brief Runs the invalid log in test.
         * 
         * This runs the invalid log in test on the latest FireFox browser.
         */
        [Test]
        public void HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication()
        {
            this.driver = new FirefoxDriver();

            base.HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication(this.driver);
        }
    }
}
