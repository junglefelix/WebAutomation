
using OpenQA.Selenium;
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

    public class MallPage : BasePage
    {

        //Locators
        private By searchSelector => By.ClassName("search-input");
        private By product_Price => By.CssSelector("body > div.dialog-wrapper > div.dialog.animated.zoomIn.product.enable-close > div.dialog-body.default-text-align > div.product-details > div.product-details > div.bottom.new-promotion > div.weight-and-price > div.sp-product-price > span");


        public void navigateTo(string productName)
        {
            driver.Navigate().GoToUrl(mallUrl);          
            driver.FindElement(searchSelector).SendKeys(productName+Keys.Enter);
            var productResults = driver.FindElements(By.Id("product_name"));
            foreach (var product_result in productResults)
            {
                    if (product_result.Text == productName)
                    {
                        product_result.Click();                        
                        break;
                    }
            }
        }
            
        
        public string checkPrice()
        {
            string productPrice = driver.FindElement(product_Price).Text;
            return productPrice; 
        }

    }
}
