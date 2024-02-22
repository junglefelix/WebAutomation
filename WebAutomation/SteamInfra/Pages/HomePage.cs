
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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



        public HomePage navigateTo()
        {
            driver.Navigate().GoToUrl(baseUrl + HomePageSuffix);

            //waitForPageToLoad();
            pause(1000);
            return this;
        }

        public void SearchForGame(string gameName)
        {
            IWebElement searchWindow = locatorHelper.waitForElement(searchSelector);
            searchWindow.Click();
            searchWindow.SendKeys(gameName + Keys.Return);
        }

      

    }
}
