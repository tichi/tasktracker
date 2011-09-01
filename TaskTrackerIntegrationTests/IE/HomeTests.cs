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
    [TestFixture]
    [Category("IE")]
    class HomeTests : TaskTrackerIntegrationTests.Base.HomeTests
    {
        IWebDriver driver;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.browserType = Base.BROWSER_TYPE.InternetExplorer;
        }

        [TearDown]
        public void TearDown()
        {
            this.driver.Quit();
        }

        [Test]
        public void DefaultURL_RoutesToHomeIndex()
        {
            this.driver = this.CreateDriver();

            base.DefaultURL_RoutesToHomeIndex(this.driver);
        }
    }
}
