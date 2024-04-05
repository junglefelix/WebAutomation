
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace SteamInfra.Pages
{

    public class HomePage : BasePage
    {
        private const string HomePageSuffix = ""; // no suffix - it is main page.

        //Locators
        private By searchSelector => By.CssSelector("#store_nav_search_term");
        private By languageDropDownSelector => By.CssSelector("#language_pulldown");




        public HomePage navigateTo()
        {
            driver.Navigate().GoToUrl(baseUrl + HomePageSuffix);
        //    driver.FindElement(languageSelector).Click();
         //   driver.FindElement(By.LinkText("English (английский)")).Click();


            //waitForPageToLoad();
            pause(1000);
            return this;
        }

        public void SearchForGame(string gameName)
        {
            try
            {
                IWebElement searchWindow = locatorHelper.waitForElement(searchSelector);
                searchWindow.Click();
                searchWindow.SendKeys(gameName + Keys.Return);
            }
            catch (StaleElementReferenceException) 
            {
                IWebElement searchWindow = locatorHelper.waitForElement(searchSelector);
                searchWindow.Click();
                searchWindow.SendKeys(gameName + Keys.Return);
            }
        }


        public void ChangeLanguage(Language language)
        {
            locatorHelper.waitForElement(languageDropDownSelector).Click();
            var languageMenus =  driver.FindElements(By.CssSelector("#language_dropdown > div > a"));

            IWebElement selectedLangMenu = null;
            switch (language)
            {
                case Language.English:
                    selectedLangMenu = languageMenus.SingleOrDefault(m => m.Text.ToLower().Contains("english"));
                    break;
                case Language.Russian:
                    selectedLangMenu = languageMenus.SingleOrDefault(m => m.Text.ToLower().Contains("russian"));
                    break;
                default:
                    break;
            }
            if(selectedLangMenu == null)
            {
                logger.Debug($"Expected language: {language} not present -> this means it is already selected");
                pause(500);
                Actions actions = new Actions(driver);
                actions.SendKeys(Keys.Escape).Perform();
            }
            else
            {
                selectedLangMenu.Click();
            }
          
        }
      

    }

    public enum Language
    {
        English,
        Russian,
    }
}
