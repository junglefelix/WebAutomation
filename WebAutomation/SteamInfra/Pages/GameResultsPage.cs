using OpenQA.Selenium;
using SteamInfra.DataModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInfra.Pages
{
    public class GameResultsPage : BasePage
    {


        private By searchEntryParent => By.CssSelector("#search_resultsRows > a > div.responsive_search_name_combined");
        private By searchListItem => By.CssSelector("#search_resultsRows > a > div.responsive_search_name_combined > div.col.search_name.ellipsis > span");
        private By searchListItemPrice => By.CssSelector("#search_resultsRows > a > div.responsive_search_name_combined > div.col.search_price_discount_combined.responsive_secondrow > div > div > div.discount_prices > div.discount_final_price");
        private By searchListItemReleaseDate => By.CssSelector("#search_resultsRows > a > div.responsive_search_name_combined > div.col.search_released.responsive_secondrow");
        private By searchListItemSupportWindows => By.CssSelector("#search_resultsRows > a > div.responsive_search_name_combined > div.col.search_name.ellipsis > div > span.platform_img.win");
        private By searchListItemSupportMac => By.CssSelector("#search_resultsRows > a > div.responsive_search_name_combined > div.col.search_name.ellipsis > div > span.platform_img.mac");
        private By searchListItemSupportLinux => By.CssSelector("#search_resultsRows > a > div.responsive_search_name_combined > div.col.search_name.ellipsis > div > span.platform_img.linux");

        //private string gameTitle;
        //private double gamePrice;
        //private DateTime gameReleaseDate;
        //private string gameSupportWindows;
        public List<SearchEntry> GetGameResultsData()
        {
            var results = new List<SearchEntry>();

            var resultContainers = driver.FindElements(searchEntryParent);
            foreach(var container in resultContainers.Take(20))
            {
                SearchEntry entry = new SearchEntry();
                entry.GameName = container.FindElement(By.CssSelector("div.col.search_name.ellipsis > span")).Text;
                entry.PriceStr = container.FindElement(By.CssSelector("div.col.search_price_discount_combined.responsive_secondrow > div > div > div.discount_prices > div.discount_final_price")).Text;
                entry.ReleaseDateStr = container.FindElement(By.CssSelector("div.col.search_released.responsive_secondrow")).Text;

                entry.ProcessEntry();
                results.Add(entry);
            }



            return results;
        }

        public List<GameResultsPage> GetGameResultsDataFiltered(double minPrice, double maxPrice, string minDate, string maxDate)
        {
            var resultsFiltered = new List<GameResultsPage>();
            var titleItems = driver.FindElements(searchListItem);
            var priceItems = driver.FindElements(searchListItemPrice);
            var releaseDateItems = driver.FindElements(searchListItemReleaseDate);
            var windowsSupportedSigns = driver.FindElements(searchListItemSupportWindows);
            var linuxSupportedSigns = driver.FindElements(searchListItemSupportLinux);
            var macSupportedSigns = driver.FindElements(searchListItemSupportMac);

            //DateTime min_Date = ConvertToDateTime(minDate);
            //DateTime max_Date = ConvertToDateTime(maxDate);

            for (int i = 0; i < titleItems.Count; i++)
            {
                //this.gameTitle = titleItems[i].Text;
                //this.gamePrice = ConvertToDouble(priceItems[i].Text);
                //this.gameReleaseDate = ConvertToDateTime(releaseDateItems[i].Text);
                //if ((minPrice<=this.gamePrice)&&(this.gamePrice<=maxPrice)&& (min_Date <= this.gameReleaseDate) && (this.gameReleaseDate <= max_Date))
                //{
                //        logger.Debug($"Game Title {i + 1}: {this.gameTitle}, Price: {this.gamePrice},Release Date: {this.gameReleaseDate}");
                //        resultsFiltered.Add(this);                    
                //}
                
            }
            return resultsFiltered;
        }

       

   
    }
}