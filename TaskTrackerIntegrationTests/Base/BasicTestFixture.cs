using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace TaskTrackerIntegrationTests.Base
{
    /**
     * \brief Basic test fixture that all test fixtures inherit.
     * \author Katharine Gillis
     * \date 2011-09-10
     * 
     * Defines the basic, common methods used by all test fixtures in this assembly.
     */
    abstract class BasicTestFixture
    {
        /**
         * \brief The browser type for the test fixture.
         * 
         * The browser type for the test fixture.
         */
        protected BROWSER_TYPE browserType;

        /**
         * \brief The wait time (in milliseconds) for the Wait function.
         * 
         * The wait time (in milliseconds) for the Wait function.
         */
        protected int waitTime;

        /**
         * \brief Default constructor.
         * 
         * Sets the wait time based on the machine the assembly is running on.
         */
        public BasicTestFixture()
        {
            if (File.Exists(@"C:\Program Files\Mozilla Firefox 3.6\firefox.exe"))
            {
                this.waitTime = 2000;
            }
            else
            {
                this.waitTime = 500;
            }
        }

        /**
         * \brief Creates the web driver.
         * 
         * Used to create the browser driver for the tests, based on the browser type.
         */
        protected IWebDriver CreateDriver()
        {
            FirefoxBinary binary;
            FirefoxProfile profile;
            switch (this.browserType)
            {
                case BROWSER_TYPE.Chrome:
                    return new ChromeDriver();
                case BROWSER_TYPE.FireFox3_6:
                    if (File.Exists(@"C:\Program Files\Mozilla Firefox 3.6\firefox.exe"))
                    {
                        binary = new FirefoxBinary(@"C:\Program Files\Mozilla Firefox 3.6\firefox.exe");
                    }
                    else
                    {
                        binary = new FirefoxBinary(@"G:\Program Files (x86) SSD\Mozilla Firefox 3.6\firefox.exe");
                    }
                    profile = new FirefoxProfile();
                    return new FirefoxDriver(binary, profile);
                case BROWSER_TYPE.FireFox4:
                    if (File.Exists(@"C:\Program Files\Mozilla Firefox 4.0\firefox.exe"))
                    {
                        binary = new FirefoxBinary(@"C:\Program Files\Mozilla Firefox 4.0\firefox.exe");
                    }
                    else
                    {
                        binary = new FirefoxBinary(@"G:\Program Files (x86) SSD\Mozilla Firefox 4.0\firefox.exe");
                    }
                    profile = new FirefoxProfile();
                    return new FirefoxDriver(binary, profile);
                case BROWSER_TYPE.FireFox5:
                    if (File.Exists(@"C:\Program Files\Mozilla Firefox 5.0\firefox.exe"))
                    {
                        binary = new FirefoxBinary(@"C:\Program Files\Mozilla Firefox 5.0\firefox.exe");
                    }
                    else
                    {
                        binary = new FirefoxBinary(@"G:\Program Files (x86) SSD\Mozilla Firefox 5.0\firefox.exe");
                    }
                    profile = new FirefoxProfile();
                    return new FirefoxDriver(binary, profile);
                case BROWSER_TYPE.FireFoxCurrent:
                    return new FirefoxDriver();
                case BROWSER_TYPE.InternetExplorer:
                    return new InternetExplorerDriver();
                default:
                    return null;
            }
        }

        /**
         * \brief Pauses the test for a preset amount of time.
         * 
         * Used to force a test to wait, primarily for browser pages to finish loading.
         * The wait time is determined when the test fixture is constructed.
         */
        protected void Wait()
        {
            Thread.Sleep(this.waitTime);
        }

        /**
         * \brief Ensure the user is logged out for Internet Explorer.
         * 
         * Ensure the user is logged out for Internet Explorer.
         */
        protected void EnsureLoggedOut(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://localhost:8085/");
            Wait();

            try
            {
                IWebElement logOutLink = driver.FindElement(By.XPath("//a[text()='Log Off']"));
                logOutLink.Click();
                Wait();
            }
            catch (NoSuchElementException e) { }
        }
    }

    /**
     * \brief Browser enum.
     * 
     * A list of browsers available to test with.
     */
    public enum BROWSER_TYPE { Chrome, FireFox3_6, FireFox4, FireFox5, FireFoxCurrent, InternetExplorer };
}
