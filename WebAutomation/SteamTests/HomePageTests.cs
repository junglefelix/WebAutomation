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
        [TestCase("Starcraft", 10, 20, "01/01/2021", "01/01/2024",5) ]
        //Filter search results by game name, between which prices to search, between which dates to search. Assert number of results is above min_count value
        public void SearchForGame_Filtered_Results(string searchGame,double minimum_price, double maximum_price,DateTime minimum_date, DateTime maximum_date, int min_count)
        {              
            logger.Debug($"Will search for game: {searchGame}");
            homePage.navigateTo();
            homePage.ChangeLanguage(Language.English);
            homePage.SearchForGame(searchGame);  
            var resultsFiltered = searchResPage.GetGameResultsDataFiltered(minimum_price, maximum_price, minimum_date, maximum_date);
            Assert.That(resultsFiltered.Count > min_count);        
        }
        
        [Test]
        [TestCase(50, "Starcraft")]
        //Verify if the most expensive and most recent results in search list for game are not above your budget
        public void Budget_verification(double your_budget, string searchGame)
        {
            logger.Debug($"Will search for game: {searchGame}");
            homePage.navigateTo();
            homePage.ChangeLanguage(Language.English);
            homePage.SearchForGame(searchGame);
            List<SearchEntry> games = searchResPage.GetGameResultsData();
            //var maxPriceEntry = searchResPage.getEntryWithMaxPriceOnResultsPage(games);
            var maxPriceEntry = games.OrderByDescending(g => g.Price).First();

            //var recentDateEntry = searchResPage.getEntryWithRecentDateOnResultsPage(games);
            var recentDateEntry = games.OrderByDescending(g => g.ReleaseDate).First();

            Assert.That(maxPriceEntry.Price <= your_budget);
            Assert.That(recentDateEntry.Price <= your_budget);
        }
        
        
        [Test]
        [TestCase("Starcraft", "shoot")]
        //Verify if the most expensive results in search list for game include mentioned word in description section
        public void Verify_Description(string searchGame, string targetSearch)
        {
            logger.Debug($"Will search for game: {searchGame}");
            homePage.navigateTo();
            homePage.ChangeLanguage(Language.English);
            homePage.SearchForGame(searchGame);
            List<SearchEntry> games = searchResPage.GetGameResultsData();
            //var maxPriceEntry = searchResPage.getEntryWithMaxPriceOnResultsPage(games);
            var maxPriceEntry = games.OrderByDescending(g => g.Price).First();
            searchResPage.NavigateToEntryDetails(maxPriceEntry);
            string description = gameDetailsPage.GetDescriptionText();            
            Assert.That(description.Contains(targetSearch));
        }
        

        [Test]
        [TestCase(32, 32, "Starcraft")]
        //Verify if the most expensive / most recent results in search list for game meet sys.requirements (memory,hdd in GB) mentioned above     
        public void VerifySystemRequirementsExpensive(double my_memory, double my_storage, string searchGame)
        {
            logger.Debug($"Will search for game: {searchGame}");
            homePage.navigateTo();
            homePage.ChangeLanguage(Language.English);
            homePage.SearchForGame(searchGame);
            List<SearchEntry> games = searchResPage.GetGameResultsData();
            var maxPriceEntry = games.OrderByDescending(g => g.Price).First(); 
            searchResPage.NavigateToEntryDetails(maxPriceEntry);           
            SystemRequirements gameReqs = gameDetailsPage.GetSystemRequirements();            
            Assert.That((my_memory >= gameReqs.memory) && (my_storage >= gameReqs.storage));
        }

        [Test]
        [TestCase(32, 32, "Starcraft")]
        //Verify if the most expensive / most recent results in search list for game meet sys.requirements (memory,hdd in GB) mentioned above     
        public void VerifySystemRequirementsLatest(double my_memory, double my_storage, string searchGame)
        {
            logger.Debug($"Will search for game: {searchGame}");
            homePage.navigateTo();
            homePage.ChangeLanguage(Language.English);
            homePage.SearchForGame(searchGame);
            List<SearchEntry> games = searchResPage.GetGameResultsData();           
            var recentDateEntry = games.OrderByDescending(g => g.ReleaseDate).First();
            searchResPage.NavigateToEntryDetails(recentDateEntry);            
            SystemRequirements gameReqs = gameDetailsPage.GetSystemRequirements();          
            Assert.That((my_memory >= gameReqs.memory) && (my_storage >= gameReqs.storage));
        }
        /*
       [Test]
       public void SearchForGame_AllResults()
       {         

           string gameName = "Starcraft";

           logger.Debug($"Will search for game: {gameName}");
           homePage.navigateTo();
           homePage.ChangeLanguage(Language.English);
           homePage.SearchForGame(gameName);   
           List<SearchEntry> games  = searchResPage.GetGameResultsData();
       }

        */


    }
}