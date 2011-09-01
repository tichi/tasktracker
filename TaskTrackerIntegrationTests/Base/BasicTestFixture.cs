using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace TaskTrackerIntegrationTests.Base
{
    abstract class BasicTestFixture
    {
        protected BROWSER_TYPE browserType;

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
                        binary = new FirefoxBinary(@"D:\Program Files (x86)\Mozilla Firefox 3.6\firefox.exe");
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
                        binary = new FirefoxBinary(@"D:\Program Files (x86)\Mozilla Firefox 4.0\firefox.exe");
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
                        binary = new FirefoxBinary(@"D:\Program Files (x86)\Mozilla Firefox 5.0\firefox.exe");
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
    }

    public enum BROWSER_TYPE { Chrome, FireFox3_6, FireFox4, FireFox5, FireFoxCurrent, InternetExplorer };
}
