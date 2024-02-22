using NLog;
using NUnit.Framework;
using SteamInfra.Pages;
using SteamTests.Helpers;
using System.Globalization;

namespace SteamTests
{
    [TestFixture]
    public class HomePageTests: TestBase
    {
       


        [SetUp]
        public void Setup()
        {
            logger.Debug($"#####################   Setup Started  ######################");

            logger.Debug($"#####################   Setup Finished   ######################");
        }

        [TearDown]
        public void TearDown()
        {
            logger.Debug($"#####################   TearDown Started  ######################");

            logger.Debug($"#####################   TearDown Finished   ######################");

        }

       

        [Test]
        public void SearchForGame()
        {
            string gameName = "Starcraft";
            logger.Debug($"Will search for starcraft");
            homePage.navigateTo();
            homePage.SearchForGame(gameName);

            var titles = Pages.gameResultsOnHomePage.GetSearchResultsTitles();
            var prices = Pages.gameResultsOnHomePage.GetSearchPriceResults();
            var releaseDates = Pages.gameResultsOnHomePage.GetSearchReleaseDateResults();

           var results = Pages.gameResultsOnHomePage.GetGameResultsData(titles,prices,releaseDates);

    

        }

       

    }
}