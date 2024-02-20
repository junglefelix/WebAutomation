using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SteamInfra.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInfra.Pages
{
    public abstract class BasePage
    {
        protected static ILogger logger = LogManager.GetCurrentClassLogger();
        protected const int SECOND = 1000;
        protected const int MINUTE = 60 * 1000;
        protected const int HOUR = 60 * MINUTE;
        protected static IWebDriver driver;
        protected static string baseUrl;
        protected static  LocatorHelper locatorHelper;


        public static void initDriver(string _baseUrl)
        {
            baseUrl = _baseUrl;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--metrics-recording-only");
            options.AddArgument("--force-fieldtrials=SiteIsolationExtensions/Control");
            options.AddArgument("--disable-web-resources");
            options.AddArgument("--no-sandbox");
            options.AddArgument("no-sandbox");
            //options.AddUserProfilePreference("download.default_directory", defaultDownloadPath);
            //options.AddUserProfilePreference("download.prompt_for_download", false);
            //options.AddUserProfilePreference("download.directory_upgrade", true);

            // options for logging
            //options.SetLoggingPreference(LogType.Browser, OpenQA.Selenium.LogLevel.All);
            //options.SetLoggingPreference(LogType.Client, OpenQA.Selenium.LogLevel.All);
            //options.SetLoggingPreference(LogType.Driver, OpenQA.Selenium.LogLevel.All);
            //options.SetLoggingPreference(LogType.Server, OpenQA.Selenium.LogLevel.All);
            driver = new ChromeDriver(options);
            locatorHelper = new LocatorHelper(driver);
        }
        public static void closeDriver()
        {

            driver.Close();
            driver.Dispose();
        }


        public static void pause(int waitMiliSec = 1000)
        {
            Thread.Sleep(waitMiliSec);
        }
    }
}
