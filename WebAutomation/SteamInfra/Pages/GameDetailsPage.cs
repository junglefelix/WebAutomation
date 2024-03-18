using OpenQA.Selenium;
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
        string reviewMark;
        SystemRequirements gameRequirements;
        private By description => By.Id("game_area_description");
        private By SystemRequirements => By.CssSelector("#tabletGrid > div.page_content_ctn > div:nth-child(6) > div.leftcol.game_description_column > div:nth-child(11) > div.game_page_autocollapse.sys_req > div > div > div.game_area_sys_req_leftCol > ul > ul");

        public string  GetDescriptionText()
        {
            this.aboutDescription = driver.FindElement(description).Text;
                
           return aboutDescription;
        }
    }
}
