using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenQA.Selenium;

using NUnit.Framework;

namespace TaskTrackerIntegrationTests.Base
{
    /**
     * \brief Integration tests for the authentication functionality.
     * \author Katharine Gillis
     * \date 2011-09-10
     * 
     * Defines the integration tests for logging in and logging out.
     */
    abstract class LogOnLogOutTests : BasicTestFixture
    {
        /**
         * \brief Valid log in and log out test.
         * 
         * Tests that a valid user can log in and log out.
         * 
         * Steps:
         *  -# Navigate to http://localhost:8085.
         *  -# Click the Log On link.
         *  -# The page title should be "Task Tracker - Log On".
         *  -# Enter the following information:
         *      - User Name: testuser
         *      - Password: password
         *  -# Click the Log On button.
         *  -# The page title should be "Task Tracker - Home".
         *  -# The Log On link should be replaced with a welcome message and the Log Off link.
         *  -# Click the Log Off link.
         *  -# The page title should be "Task Tracker - Home".
         *  -# The welcome message and Log Off link should be replaced with the Log On link.
         *  
         * \param driver The browser driver to use.
         */
        protected void HomeIndex_LogIn_LogOut(IWebDriver driver)
        {
            // Navigate to the base url.
            driver.Navigate().GoToUrl("http://localhost:8085/");
            Wait();

            // Click on the Log On link.
            IWebElement logOnLink = driver.FindElement(By.XPath("//a[text()='Log On']"));
            logOnLink.Click();
            Wait();

            // Check that the page title is that of the User/Logon page.
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - Log On"));

            // Enter the username and password.
            IWebElement userName = driver.FindElement(By.Id("UserName"));
            userName.SendKeys("testuser");

            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("password");

            // Submit the form.
            IWebElement logOn = driver.FindElement(By.Id("LogOn"));
            logOn.Click();
            Wait();

            // Check that the page title is that of the Home/Index page.
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - Home"));

            // Check that the Log On link does not exist, and the Log Out link and Welcome message do.
            Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.XPath("//a[text()='Log On']")));
            
            IWebElement logOutLink = driver.FindElement(By.XPath("//a[text()='Log Off']"));

            IWebElement welcomeMessage = driver.FindElement(By.XPath("//span[@welcome=''][text()='Welcome, test user!']"));

            // Click the Log Out link.
            logOutLink.Click();
            Wait();

            // Check that the page title is that of the Home/Index page.
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - Home"));

            // Check that the Log On link exists, and the Log Out link and Welcome message do.
            logOnLink = driver.FindElement(By.XPath("//a[text()='Log On']"));

            Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.XPath("//a[text()='Log Off']")));

            Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.XPath("//span[@welcome=''][text()='Welcome, test user!']")));
        }

        /**
         * \brief Invalid log in test.
         * 
         * Tests that an invalid user or password can't get to the secure content.
         * 
         * Steps:
         *  -# Navigate to http://localhost:8085.
         *  -# Click on the Log On link.
         *  -# The page title should be "Task Tracker - Log On".
         *  -# Click the Log On button, leaving the User Name and Password fields blank.
         *  -# Error messages "User Name is required." and "Password is required." should appear.
         *  -# Enter the following information:
         *      - User Name: invaliduser
         *      - Password: password
         *  -# Click the Log On button.
         *  -# Error message "Invalid username or password." should appear, while the required error messages should disappear.
         *  -# Enter the following information:
         *      - User Name: testuser
         *      - Password: invalidpassword
         *  -# Click the Log On button.
         *  -# Error message "Invalid username or password." should appear.
         *  
         * \param driver The browser driver to use.
         */
        protected void HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication(IWebDriver driver)
        {
            // Navigate to the base url.
            driver.Navigate().GoToUrl("http://localhost:8085/");
            Wait();

            // Click on the Log On link.
            IWebElement logOnLink = driver.FindElement(By.XPath("//a[text()='Log On']"));
            logOnLink.Click();
            Wait();

            // Check that the page title is that of the User/Logon page.
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - Log On"));

            // Press Log On without entering any information.
            IWebElement logOn = driver.FindElement(By.Id("LogOn"));
            logOn.Click();
            Wait();

            // Check that two error messages come up indicating that UserName and Password are required.
            IWebElement userNameErrorMessage = driver.FindElement(By.XPath("//span[@class='field-validation-error'][@data-valmsg-for='UserName']"));
            IWebElement passwordErrorMessage = driver.FindElement(By.XPath("//span[@class='field-validation-error'][@data-valmsg-for='Password']"));

            // Enter an invalid user name and a valid password.
            IWebElement userName = driver.FindElement(By.Id("UserName"));
            userName.SendKeys("invaliduser");

            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("password");

            // Attempt to log on.
            logOn = driver.FindElement(By.Id("LogOn"));
            logOn.Click();
            Wait();

            // Check that the two required error messages disappeared.
            userNameErrorMessage = driver.FindElement(By.XPath("//span[@class='field-validation-valid'][@data-valmsg-for='UserName']"));
            passwordErrorMessage = driver.FindElement(By.XPath("//span[@class='field-validation-valid'][@data-valmsg-for='Password']"));

            // Check that the invalid logon message came up.
            IWebElement invalidLoginAlert = driver.FindElement(By.XPath("//span[@alert=''][text()='Invalid user name or password.']"));

            // Enter a valid user name and an invalid password.
            userName = driver.FindElement(By.Id("UserName"));
            userName.Clear();
            userName.SendKeys("testuser");

            password = driver.FindElement(By.Id("Password"));
            password.Clear();
            password.SendKeys("invalidpassword");

            // Attempt to log on.
            logOn = driver.FindElement(By.Id("LogOn"));
            logOn.Click();
            Wait();

            // Check that the invalid logon message came up.
            invalidLoginAlert = driver.FindElement(By.XPath("//span[@alert=''][text()='Invalid user name or password.']"));
        }
    }
}
