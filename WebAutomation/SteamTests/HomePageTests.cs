using NLog;
using NUnit.Framework;
using SteamInfra.DataModels;
using SteamInfra.Helpers;
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

            logger.Debug($"Will search for game: {gameName}");
            homePage.navigateTo();
            homePage.SearchForGame(gameName);                      

           //var results = game.GetGameResultsData();

            var resultsFiltered = searchResPage.GetGameResultsDataFiltered(10, 20, "1 Jan. 2021", "1 Jan. 2024");
            

        } 
               

        [Test]
        public void SearchForGame_AllResults()
        {         

            string gameName = "Starcraft";

            logger.Debug($"Will search for game: {gameName}");
            homePage.navigateTo();
            homePage.SearchForGame(gameName);                      

          

            List<SearchEntry> games  = searchResPage.GetGameResultsData();
            

        }

        [Test]
        public void ChangeLanguage()
        {

           
            homePage.navigateTo();
            homePage.ChangeLanguage(Language.Russian);
            homePage.ChangeLanguage(Language.English);




           


        }

    }
}