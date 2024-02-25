﻿using NLog;
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

<<<<<<< HEAD
            var titles = Pages.gameResultsOnHomePage.GetSearchResultsTitles();
            var prices = Pages.gameResultsOnHomePage.GetSearchPriceResults();
            var releaseDates = Pages.gameResultsOnHomePage.GetSearchReleaseDateResults();

           var results = Pages.gameResultsOnHomePage.GetGameResultsData(titles,prices,releaseDates);

    

        }

       
=======
            var titles = homePage.GetSearchResults();
            logger.Debug($"titles object is: {Environment.NewLine}{DataHelper.PrintObject(titles)}");

        }


        [Test]
        public void ConvertToDouble()
        {
            string str = "54.00";
            double num = Convert.ToDouble(str);
        }

>>>>>>> 82278e58c2c8c503ff26a218a998bc683852bb1b

    }
}