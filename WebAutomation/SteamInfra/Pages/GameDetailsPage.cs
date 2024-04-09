using OpenQA.Selenium;
using SteamInfra.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInfra.Pages
{
    public class GameDetailsPage : BasePage
    {
        string aboutDescription;
       // string reviewMark;
        SystemRequirements gameRequirements;
        private By description => By.Id("game_area_description");        

        private By SystemReqsMemory => By.XPath("//div[@class='game_area_sys_req_leftCol']/ul/ul/li[contains(., 'Memory')]");        
        private By SystemReqsStorage => By.XPath("//div[@class='game_area_sys_req_leftCol']/ul/ul/li[contains(., 'Storage')]");

        private By SystemReqsMemorySingleColumn => By.XPath("//div[@class='game_area_sys_req_full']/ul/ul/li[contains(., 'Memory')]");
        private By SystemReqsStorageSingleColumn => By.XPath("//div[@class='game_area_sys_req_full']/ul/ul/li[contains(., 'Storage')]");

  
        public string  GetDescriptionText()
        {
            this.aboutDescription = driver.FindElement(description).Text;
            return aboutDescription;
        }
        public SystemRequirements GetSystemRequirements()
        {
            //case if in game details page, there are 2 columns of system requirements: minimum and optimal. Verify column of minimal
            try
            {
                string memoryItemText = driver.FindElement(SystemReqsMemory).Text;
                string storageItemText = driver.FindElement(SystemReqsStorage).Text;
                SystemRequirements gameSystemReqs = new SystemRequirements(memoryItemText, storageItemText);
                return gameSystemReqs;
            }
            //case if in game details page, there is single column of system requirements. Verify it
            catch (NoSuchElementException ex)
            {
                string memoryItemText = driver.FindElement(SystemReqsMemorySingleColumn).Text;
                string storageItemText = driver.FindElement(SystemReqsStorageSingleColumn).Text;
                SystemRequirements gameSystemReqs = new SystemRequirements(memoryItemText, storageItemText);
                return gameSystemReqs;
            }
            
               
               
            
        }
    }
}
