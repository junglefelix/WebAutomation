using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInfra.Pages
{
    public class Pages : BasePage
    {
        private static HomePage _homePage;
        public static HomePage homePage => _homePage ?? (_homePage = new HomePage());

        private static GameResultsPage _gameResultsOnHomePage;
        public static GameResultsPage gameResultsOnHomePage => _gameResultsOnHomePage ?? (_gameResultsOnHomePage = new GameResultsPage());

        private static GameDetailsPage _gameDetailsPage;
        public static GameDetailsPage gameDetailsPage => _gameDetailsPage ?? (_gameDetailsPage = new GameDetailsPage());
    }
}
