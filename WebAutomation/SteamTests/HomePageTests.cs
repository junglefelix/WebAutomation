using NLog;
using NUnit.Framework;
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

            var resultsFiltered = game.GetGameResultsDataFiltered(10, 20, "1 янв. 2021", "1 янв. 2024");
            

        } 

    }
}