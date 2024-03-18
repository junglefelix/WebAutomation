using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInfra.DataModels
{
    public class SearchEntry
    {
        public string GameName { get; set; }
        public double Price { get; set; }
        public string PriceStr { get; set; }

        public DateTime ReleaseDate { get; set; }
        public string ReleaseDateStr { get; set; }
        public GamePlatform Platform { get; set; }

        public void ProcessEntry()
        {
            Price = ConvertToDouble(this.PriceStr);
            ReleaseDate = ConvertToDateTime(ReleaseDateStr);
        }



        private double ConvertToDouble(string price)
        {
            
            string priceWithoutSymbols = price.Replace("₪", "").Replace("Бесплатно", "0.0").Replace("$", "").Replace("Free", "0.0");
            double finalPrice = Convert.ToDouble(priceWithoutSymbols, CultureInfo.InvariantCulture);
            return finalPrice;
        }


        private static DateTime ConvertToDateTime(string releaseDateItem)
        {
            string[] formats = { "d MMM. yyyy", "dd MMM. yyyy","d MMM yyyy","dd MMM yyyy","d MMMM yyyy","dd MMMM yyyy","d MMMM. yyyy","dd MMMM. yyyy",
                                 "d mmm. yyyy", "dd mmm. yyyy","d mmm yyyy","dd mmm yyyy","d mmmm yyyy","dd mmmm yyyy","d mmmm. yyyy","dd mmmm. yyyy",
                                 "d MMM, yyyy", "dd MMM, yyyy","d MMMM, yyyy","dd MMMM, yyyy", "d mmm, yyyy", "dd mmm, yyyy","d mmmm, yyyy","dd mmmm, yyyy"};
            DateTime releaseDate;
           // CultureInfo russianCulture = new CultureInfo("ru-RU");
            foreach (string format in formats)
            {
                if (DateTime.TryParseExact(releaseDateItem, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate))
                {
                    return releaseDate.Date;
                }
                //Handling cases of empty date entry or entry like "Q2 2024"/"2024" or entry like "Coming soon" 
                else if ((string.IsNullOrEmpty(releaseDateItem)) || (releaseDateItem.StartsWith("Q")) || (releaseDateItem.StartsWith("202")) || releaseDateItem.Contains("soon")|| releaseDateItem.Contains("announced"))
                {
                    return new DateTime(2030, 1, 1);
                }
            }
            
            
            throw new FormatException($"Invalid date format: {releaseDateItem}");
        }


    }

    public enum GamePlatform
    {
        Win,
        Mac,
        Both
    }


}
