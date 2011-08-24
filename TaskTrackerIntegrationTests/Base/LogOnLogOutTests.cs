using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenQA.Selenium;

using NUnit.Framework;

namespace TaskTrackerIntegrationTests.Base
{
    abstract class LogOnLogOutTests
    {
        protected void HomeIndex_LogIn_LogOut(IWebDriver driver)
        {
            // Navigate to the base url.
            driver.Navigate().GoToUrl("http://localhost:8085/");

            // Click on the Log On link.
            IWebElement logOnLink = driver.FindElement(By.XPath("//a[text()='Log On']"));
            logOnLink.Click();

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

            // Check that the page title is that of the Home/Index page.
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - Home"));

            // Check that the Log On link does not exist, and the Log Out link and Welcome message do.
            Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.XPath("//a[text()='Log On']")));
            
            IWebElement logOutLink = driver.FindElement(By.XPath("//a[text()='Log Out']"));

            Assert.That(driver.PageSource.Contains("Welcome, testuser!"), Is.True);

            // Click the Log Out link.
            logOutLink.Click();

            // Check that the page title is that of the Home/Index page.
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - Home"));

            // Check that the Log On link exists, and the Log Out link and Welcome message do.
            logOnLink = driver.FindElement(By.XPath("//a[text()='Log In']"));

            Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.XPath("//a[text()='Log Off']")));

            Assert.That(driver.PageSource.Contains("Welcome, testuser!"), Is.False);
        }

        protected void HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication(IWebDriver driver)
        {
            // Navigate to the base url.
            driver.Navigate().GoToUrl("http://localhost:8085/");

            // Click on the Log On link.
            IWebElement logOnLink = driver.FindElement(By.XPath("//a[text()='Log On']"));
            logOnLink.Click();

            // Check that the page title is that of the User/Logon page.
            Assert.That(driver.Title, Is.EqualTo("Task Tracker - Log On"));

            // Press Log On without entering any information.
            IWebElement logOn = driver.FindElement(By.Id("LogOn"));
            logOn.Click();

            // Check that two error messages come up indicating that UserName and Password are required.
            Assert.That(driver.PageSource.Contains("User name is required."), Is.True);
            Assert.That(driver.PageSource.Contains("Password is required."), Is.True);

            // Enter an invalid user name and a valid password.
            IWebElement userName = driver.FindElement(By.Id("UserName"));
            userName.SendKeys("invaliduser");

            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("password");

            // Attempt to log on.
            logOn.Click();

            // Check that the invalid logon message came up.
            Assert.That(driver.PageSource.Contains("Invalid user name or password."), Is.True);

            // Enter a valid user name and an invalid password.
            userName.Clear();
            userName.SendKeys("testuser");

            password.Clear();
            password.SendKeys("invalidpassword");

            // Attempt to log on.
            logOn.Click();

            // Check that the invalid logon message came up.
            Assert.That(driver.PageSource.Contains("Invalid user name or password."), Is.True);
        }
    }
}
