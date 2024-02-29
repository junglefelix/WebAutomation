using NLog;
using NUnit.Framework;
using SteamInfra.Pages;
using SteamTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTests
{
    public class TestBase
    {
        protected static string baseUrl = "https://store.steampowered.com/";
        protected static string logPath = @"C:\Temp\SteamAutomation.log";
        protected static ILogger logger;
        protected HomePage homePage => Pages.homePage;
        protected GameResult game => Pages.gameResultsOnHomePage;


        [OneTimeSetUp]
        public void OneTimeSetup()
        {

            logger = LogHelper.Create(logPath);
            logger = LogManager.GetCurrentClassLogger();
            logger.Debug($"#####################   OneTimeSetup Started  ######################");
            BasePage.initDriver(baseUrl);
            logger.Debug($"#####################   OneTimeSetup Finished   ######################");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            logger.Debug($"#####################   OneTimeTearDown Started  ######################");
            BasePage.closeDriver();
            logger.Debug($"#####################   OneTimeTearDown Finished   ######################");
        }

    }
}
