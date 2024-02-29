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
        private By searchListItemSupportWindows => By.CssSelector("#search_resultsRows > a > div.responsive_search_name_combined > div.col.search_name.ellipsis > div > span.platform_img.win");
        private By searchListItemSupportMac => By.CssSelector("#search_resultsRows > a > div.responsive_search_name_combined > div.col.search_name.ellipsis > div > span.platform_img.mac");
        private By searchListItemSupportLinux => By.CssSelector("#search_resultsRows > a > div.responsive_search_name_combined > div.col.search_name.ellipsis > div > span.platform_img.linux");

        private string gameTitle;
        private double gamePrice;
        private DateTime gameReleaseDate;
        private string gameSupportWindows;
        public List<GameResult> GetGameResultsData()
        {
            var results = new List<GameResult>();
            var titleItems = driver.FindElements(searchListItem);
            var priceItems = driver.FindElements(searchListItemPrice);
            var releaseDateItems = driver.FindElements(searchListItemReleaseDate);
            var windowsSupportedSigns = driver.FindElements(searchListItemSupportWindows); //how to check it for each game?
            var linuxSupportedSigns = driver.FindElements(searchListItemSupportLinux);
            var macSupportedSigns = driver.FindElements(searchListItemSupportMac);               

            for (int i = 0; i < titleItems.Count; i++)
            {               
                this.gameTitle = titleItems[i].Text;
                this.gamePrice = ConvertToDouble(priceItems[i].Text);
                this.gameReleaseDate = ConvertToDateTime(releaseDateItems[i].Text);
                
                
                logger.Debug($"Game Title {i + 1}: {this.gameTitle}, Price: {this.gamePrice},Release Date: {this.gameReleaseDate}");
                results.Add(this);
            }
            return results;
        }

        public List<GameResult> GetGameResultsDataFiltered(double minPrice, double maxPrice, string minDate, string maxDate)
        {
            var resultsFiltered = new List<GameResult>();
            var titleItems = driver.FindElements(searchListItem);
            var priceItems = driver.FindElements(searchListItemPrice);
            var releaseDateItems = driver.FindElements(searchListItemReleaseDate);
            var windowsSupportedSigns = driver.FindElements(searchListItemSupportWindows);
            var linuxSupportedSigns = driver.FindElements(searchListItemSupportLinux);
            var macSupportedSigns = driver.FindElements(searchListItemSupportMac);

            DateTime min_Date = ConvertToDateTime(minDate);
            DateTime max_Date = ConvertToDateTime(maxDate);

            for (int i = 0; i < titleItems.Count; i++)
            {
                this.gameTitle = titleItems[i].Text;
                this.gamePrice = ConvertToDouble(priceItems[i].Text);
                this.gameReleaseDate = ConvertToDateTime(releaseDateItems[i].Text);
                if ((minPrice<=this.gamePrice)&&(this.gamePrice<=maxPrice)&& (min_Date <= this.gameReleaseDate) && (this.gameReleaseDate <= max_Date))
                {
                        logger.Debug($"Game Title {i + 1}: {this.gameTitle}, Price: {this.gamePrice},Release Date: {this.gameReleaseDate}");
                        resultsFiltered.Add(this);                    
                }
                
            }
            return resultsFiltered;
        }

        public double ConvertToDouble(string price)
        {            
                string priceWithoutSymbols = price.Replace("₪", "").Replace("Бесплатно", "0.0").Replace("$", "");
                double finalPrice = Convert.ToDouble(priceWithoutSymbols, CultureInfo.InvariantCulture);                   
                return finalPrice;
        }

        public static DateTime ConvertToDateTime(string releaseDateItem)
        {
            string[] formats = { "d MMM. yyyy", "dd MMM. yyyy","d MMM yyyy","dd MMM yyyy","d MMMM yyyy","dd MMMM yyyy","d MMMM. yyyy","dd MMMM. yyyy",
                                 "d mmm. yyyy", "dd mmm. yyyy","d mmm yyyy","dd mmm yyyy","d mmmm yyyy","dd mmmm yyyy","d mmmm. yyyy","dd mmmm. yyyy"};
            DateTime releaseDate;
            CultureInfo russianCulture = new CultureInfo("ru-RU");
            foreach (string format in formats) 
            {
                if(DateTime.TryParseExact(releaseDateItem, format, russianCulture,DateTimeStyles.None,out releaseDate))
                {
                    return releaseDate.Date;
                }
            }
            throw new FormatException($"Invalid date format: {releaseDateItem}");
        }   
    }
}