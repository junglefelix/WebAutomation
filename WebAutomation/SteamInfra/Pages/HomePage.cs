
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInfra.Pages
{
   
    public class HomePage : BasePage
    {
        private const string HomePageSuffix = ""; // no suffix - it is main page.

        //Locators
        private By searchSelector => By.CssSelector("#store_nav_search_term");
        private By searchListItem => By.CssSelector("#search_resultsRows > a  > div.responsive_search_name_combined > div.col.search_name.ellipsis > span");
        


        public HomePage navigateTo()
        {
            driver.Navigate().GoToUrl(baseUrl + HomePageSuffix);
            
            //waitForPageToLoad();
            pause(1000);
            return this;
        }

        public void SearchForGame(string gameName)
        {
           IWebElement searchWindow =  locatorHelper.waitForElement(searchSelector);
            searchWindow.Click();
            searchWindow.SendKeys(gameName + Keys.Return);
        }

        public List<string> GetSearchResults()
        {
            List<string> titles = new List<string>();
            var items = driver.FindElements(searchListItem);
            foreach(var item in items)
            {
                titles.Add(item.Text);
            }
            return titles;
        }

    }
}
