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
            /*
            string gameName = "Starcraft";
            double minimum_price = 10;
            double maximum_price = 20;
            DateTime minimum_date = new DateTime(2021,1,1);
            DateTime maximum_date = new DateTime(2024, 1, 1);

            logger.Debug($"Will search for game: {gameName}");
            homePage.navigateTo();
            homePage.SearchForGame(gameName);                      

           var results = searchResPage.GetGameResultsData();

            var resultsFiltered = searchResPage.GetGameResultsDataFiltered(minimum_price, maximum_price, minimum_date, maximum_date);
            
            */
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

    }
}