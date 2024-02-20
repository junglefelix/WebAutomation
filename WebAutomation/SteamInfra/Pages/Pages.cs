using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInfra.Pages
{
    public class Pages : BasePage
    {
        private static HomePage _homePage;
        public static HomePage homePage => _homePage ?? (_homePage = new HomePage());

    }
}
