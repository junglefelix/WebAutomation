using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInfra.Pages
{
    public class GameResult : BasePage
    {
        private By searchListItem => By.CssSelector("#search_resultsRows > a  > div.responsive_search_name_combined > div.col.search_name.ellipsis > span");

        private By searchListItemPrice => By.CssSelector("#search_resultsRows > a > div.responsive_search_name_combined > div.col.search_price_discount_combined.responsive_secondrow > div > div > div.discount_prices > div.discount_final_price");

        private By searchListItemReleaseDate => By.CssSelector("#search_resultsRows > a > div.responsive_search_name_combined > div.col.search_released.responsive_secondrow");

        private string gameTitle;
        private double gamePrice;
        private string gameReleaseDate;
        

        public List<string> GetSearchResultsTitles()
        {
            List<string> titles = new List<string>();
            var items = driver.FindElements(searchListItem);
            foreach (var item in items)
            {               
                titles.Add(item.Text);
            }            
            return titles;
        }

        public List<string> GetSearchPriceResults()
        {
            List<string> prices = new List<string>();
            var items = driver.FindElements(searchListItemPrice);
            foreach (var item in items)
            {
                prices.Add(item.Text);
            }          
            return prices;
        }

       

        public List<string> GetSearchReleaseDateResults()
        {
            List<string> releaseDates = new List<string>();
            var items = driver.FindElements(searchListItemReleaseDate);
            foreach (var item in items)
            {
                releaseDates.Add(item.Text);
            }
            return releaseDates;
        }


        public List<GameResult> GetGameResultsData(List<string> titles, List<string> prices,List<string> releaseDates)
        {
           
            var results = new List<GameResult>();

            for (int i = 0; i<titles.Count; i++) 
            {
                GameResult _game = new GameResult();
                _game.gameTitle = titles[i];             
                _game.gamePrice = PriceToDouble(prices[i]);
                _game.gameReleaseDate = releaseDates[i];
                results.Add(_game);             
            }
            return results;
        }

        private double PriceToDouble(string price)
        {
            string priceWithoutSymbols = price.Replace("₪", "").Replace("Бесплатно", "0.0").Replace("$", "");
            double finalPrice = Convert.ToDouble(priceWithoutSymbols, CultureInfo.InvariantCulture);
            return finalPrice;
        }
    }
}

