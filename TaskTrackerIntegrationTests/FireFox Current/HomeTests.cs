using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace TaskTrackerIntegrationTests.FireFoxCurrent
{
    [TestFixture]
    [Category("FireFox")]
    class HomeTests : TaskTrackerIntegrationTests.Base.HomeTests
    {
        IWebDriver driver;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.browserType = Base.BROWSER_TYPE.FireFoxCurrent;
        }

        [TearDown]
        public void TearDown()
        {
            this.driver.Quit();
        }

        [Test]
        public void DefaultURL_RoutesToHomeIndex()
        {
            this.driver = new FirefoxDriver();

            base.DefaultURL_RoutesToHomeIndex(this.driver);
        }
    }
}
