﻿using OpenQA.Selenium;
using SteamInfra.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        
      
        public List<SearchEntry> GetGameResultsData()
        {
            var results = ProcessingEntries();      
            return results;
        }
        public List<SearchEntry> GetGameResultsDataFiltered(double minPrice, double maxPrice, DateTime minDate, DateTime maxDate)
        {
            var results = ProcessingEntries();         
            return results.Where(r_entry => r_entry.Price >= minPrice && r_entry.Price <= maxPrice && r_entry.ReleaseDate >= minDate && r_entry.ReleaseDate <= maxDate).ToList();
        }        
        public List<SearchEntry> ProcessingEntries()
        {
            var results = new List<SearchEntry>();
            var resultContainers = driver.FindElements(searchEntryParent);
            foreach (var container in resultContainers)
            {
                SearchEntry entry = new SearchEntry();
                entry.GameName = container.FindElement(By.CssSelector("div.col.search_name.ellipsis > span")).Text;
                
                try
                {
                    entry.PriceStr = container.FindElement(By.CssSelector("div.col.search_price_discount_combined.responsive_secondrow > div > div > div.discount_prices > div.discount_final_price")).Text;
                }
                catch (NoSuchElementException noPriceElement)
                {
                    entry.PriceStr = "0.0";
                }
                try
                {
                    entry.ReleaseDateStr = container.FindElement(By.CssSelector("div.col.search_released.responsive_secondrow")).Text;
                }
                catch (NoSuchElementException ReleaseDateEmpty)
                {
                    entry.ReleaseDateStr = "";
                }
                entry.ProcessEntry();
                if (entry.ReleaseDate <= DateTime.Now)
                {
                    results.Add(entry);
                }

            }
            return results;
        }

  

        public void NavigateToEntryDetails(SearchEntry entry)
        {
            var resultContainers = driver.FindElements(searchEntryParent);
            string targetTitle = entry.GameName;
            foreach (var container in resultContainers) 
            {
                IWebElement targetElement = container.FindElement(By.CssSelector("div.col.search_name.ellipsis > span"));                
                string title = targetElement.Text;
                if (title == targetTitle)
                {
                    targetElement.Click();
                    return;
                }
            }
             
          
               
            }
                 
        }

    }
