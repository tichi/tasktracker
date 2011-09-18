using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenQA.Selenium;

using NUnit.Framework;

namespace TaskTrackerIntegrationTests.Base
{
    /**
     * \brief Integration tests for user detail view.
     * \author Katharine Gillis
     * \date 2011-09-17
     * 
     * Defines the integration tests for the user detail view.
     */
    abstract class UserDetailViewTests : BasicTestFixture
    {
        /**
         * \brief Any user can get to their own user detail view from all pages.
         * 
         * When logged in, any user can navigate to their own user's detail view using the link in the navigation bar.
         * 
         * Steps
         *  -# Navigate to http://localhost:8085.
         *  -# Click on the Log On link.
         *  -# Enter the following information:
         *      - User Name: testuser
         *      - Password: password
         *  -# Click the Log On button.
         *  -# The Profile link should appear.
         *  -# Click the Profile link.
         *  -# The page title should be "Task Tracker - User Details - test user".
         *  -# The User Name should be "testuser".
         *  -# The First Name should be "test".
         *  -# The Last Name should be "user".
         *  -# The Email should be "testuser@test.com".
         *  -# The Time Zone should be "(GMT-07:00) Arizona".
         *  -# Click the Log Off link.
         */
        protected void AnyUser_CanNavigateToOwnUserDetailViewFromNavigationBar(IWebDriver driver)
        {
            // Navigate to the base url.
            driver.Navigate().GoToUrl("http://localhost:8085/");
            Wait();

            // Delete all cookies on the profile.
            driver.Manage().Cookies.DeleteAllCookies();
            Wait();

            // Click on the Log On link.
            IWebElement logOnLink = driver.FindElement(By.XPath("//a[text()='Log On']"));
            logOnLink.Click();
            Wait();

            // Enter the username and password.
            IWebElement userName = driver.FindElement(By.Id("UserName"));
            userName.SendKeys("testuser");

            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("password");

            // Submit the form.
            IWebElement logOn = driver.FindElement(By.Id("LogOn"));
            logOn.Click();
            Wait();

            // Check that the Profile link appeared.
            IWebElement profileLink = driver.FindElement(By.XPath("//a[text()='Profile']"));

            // Click the Profile link.
            profileLink.Click();
            Wait();

            // Check that the page title is "Task Tracker - User Details - test user".
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - User Details - test user"));

            // Check that the user name is "testuser".
            IWebElement userNameSpan = driver.FindElement(By.XPath("//span[@id='UserName'][text()='testuser']"));

            // Check that the first name is "test".
            IWebElement firstNameSpan = driver.FindElement(By.XPath("//span[@id='FirstName'][text()='test']"));

            // Check that the last name is "user".
            IWebElement lastNameSpan = driver.FindElement(By.XPath("//span[@id='LastName'][text()='user']"));

            // Check that the email is "testuser@test.com".
            IWebElement emailSpan = driver.FindElement(By.XPath("//span[@id='Email'][text()='testuser@test.com']"));

            // Check that the time zone is "(GMT-07:00) Arizona".
            IWebElement timeZoneSpan = driver.FindElement(By.XPath("//span[@id='TimeZone'][text()='(UTC-07:00) Arizona']"));

            // Click the Log Off link.
            IWebElement logOffLink = driver.FindElement(By.XPath("//a[text()='Log Off']"));
            logOffLink.Click();
            Wait();
        }

        /**
         * \brief Any user can view any other user's detail view.
         * 
         * When logged in, any user can navigate to any other user's detail view.
         * 
         * Steps
         *  -# Navigate to http://localhost:8085.
         *  -# Click on the Log On link.
         *  -# Enter the following information:
         *      - User Name: testuser
         *      - Password: password
         *  -# Click the Log On button.
         *  -# Navigate to http://localhost:8085/User/Detail/testuser2
         *  -# The page title should be "Task Tracker - User Details - test user2".
         *  -# The User Name should be "testuser2".
         *  -# The First Name should be "test".
         *  -# The Last Name should be "user2".
         *  -# The Email should be "testuser2@test.com".
         *  -# The Time Zone should be "(GMT-07:00) Arizona".
         *  -# Click the Log Off link.
         */
        protected void AnyUser_CanNavigateToAnyOtherUserDetailView(IWebDriver driver)
        {
            // Navigate to the base url.
            driver.Navigate().GoToUrl("http://localhost:8085/");
            Wait();

            // Delete all cookies on the profile.
            driver.Manage().Cookies.DeleteAllCookies();
            Wait();

            // Click on the Log On link.
            IWebElement logOnLink = driver.FindElement(By.XPath("//a[text()='Log On']"));
            logOnLink.Click();
            Wait();

            // Enter the username and password.
            IWebElement userName = driver.FindElement(By.Id("UserName"));
            userName.SendKeys("testuser2");

            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("password");

            // Submit the form.
            IWebElement logOn = driver.FindElement(By.Id("LogOn"));
            logOn.Click();
            Wait();

            // Navigate to testuser2's detail view.
            driver.Navigate().GoToUrl("http://localhost:8085/User/Detail/testuser2");
            Wait();

            // Check that the page title is "Task Tracker - User Details - test user2".
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - User Details - test user2"));

            // Check that the user name is "testuser2".
            IWebElement userNameSpan = driver.FindElement(By.XPath("//span[@id='UserName'][text()='testuser2']"));

            // Check that the first name is "test".
            IWebElement firstNameSpan = driver.FindElement(By.XPath("//span[@id='FirstName'][text()='test']"));

            // Check that the last name is "user2".
            IWebElement lastNameSpan = driver.FindElement(By.XPath("//span[@id='LastName'][text()='user2']"));

            // Check that the email is "testuser2@test.com".
            IWebElement emailSpan = driver.FindElement(By.XPath("//span[@id='Email'][text()='testuser2@test.com']"));

            // Check that the time zone is "(GMT-07:00) Arizona".
            IWebElement timeZoneSpan = driver.FindElement(By.XPath("//span[@id='TimeZone'][text()='(UTC-07:00) Arizona']"));

            // Click the Log Off link.
            IWebElement logOffLink = driver.FindElement(By.XPath("//a[text()='Log Off']"));
            logOffLink.Click();
            Wait();
        }

        /**
         * \brief Unauthorized users cannot access any detail view.
         * 
         * When not logged in, the user is redirected back to the Log On page when attempting to access any detail view.
         * 
         * Steps
         *  -# Navigate to http://localhost:8085/User/Detail/testuser2
         *  -# The page title should be "Task Tracker - Log On".
         */
        protected void UnauthenticatedUser_IsRedirectedToLogOn(IWebDriver driver)
        {
            // Navigate to the base url.
            driver.Navigate().GoToUrl("http://localhost:8085/");
            Wait();

            // Delete all cookies on the profile.
            driver.Manage().Cookies.DeleteAllCookies();
            Wait();

            // Navigate to testuser2's detail view.
            driver.Navigate().GoToUrl("http://localhost:8085/User/Detail/testuser2");
            Wait();

            // Check that the page title is "Task Tracker - Log On".
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - Log On"));
        }
    }
}
