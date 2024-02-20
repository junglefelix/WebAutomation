using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInfra.Helpers
{
    public class LocatorHelper
    {
        private IWebDriver driver;
        public LocatorHelper(IWebDriver _driver)
        {
            driver = _driver;
        }
        internal IWebElement waitForElement(By locator, int waitSec = 30)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSec));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
            return driver.FindElement(locator);
        }
    }
}
