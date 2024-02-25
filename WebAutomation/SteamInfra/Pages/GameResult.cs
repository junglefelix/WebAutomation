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
        private ReleaseDate gameReleaseDate;   
        
            

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
                ReleaseDate _releaseDate = new ReleaseDate();
                _game.gameTitle = titles[i];             
                _game.gamePrice = PriceToDouble(prices[i]);
                _game.gameReleaseDate = _releaseDate.RevealDateDetails(releaseDates[i]);
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

        private class ReleaseDate
        {
            public int day;
            public int year;
            public int month;
            private static Dictionary<string, int> months;
            
           
            


            public ReleaseDate()
            {              
                this.day = 0;
                this.month = 0;
                this.year = 0;
                if (months == null) 
                {
                    months = new Dictionary<string, int>();
                    months.Add("Jan", 1); months.Add("янв", 1); months.Add("Feb", 2); months.Add("фев", 2); months.Add("Mar", 3); months.Add("мар", 3); months.Add("Apr", 4); months.Add("апр", 4); months.Add("мая", 5); months.Add("May", 5);
                    months.Add("Jun", 6); months.Add("июн", 6); months.Add("Jul", 7); months.Add("июл", 7); months.Add("Aug", 8); months.Add("авг", 8); months.Add("Sep", 9); months.Add("сен", 9); months.Add("Oct", 10); months.Add("окт", 10);
                    months.Add("Nov", 11); months.Add("ноя", 11); months.Add("Dec", 12); months.Add("дек", 12);

                }
            }        

          public  ReleaseDate RevealDateDetails(string releaseDateResult)  //1 Oct. 2021; 17 Nov. 2022
            {                
                year = Convert.ToInt32(releaseDateResult.Substring(releaseDateResult.Length - 4), CultureInfo.InvariantCulture);
                month = months[releaseDateResult.Substring(releaseDateResult.Length - 9,3)];
                if (releaseDateResult.Length == 11)
                {
                    day = Convert.ToInt32(releaseDateResult[0]-'0', CultureInfo.InvariantCulture);
                }
                else
                {
                    day = Convert.ToInt32(releaseDateResult.Substring(0,2), CultureInfo.InvariantCulture);
                }
                return this;
            }
        }
    }
}

