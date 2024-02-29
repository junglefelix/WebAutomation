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

        public DateTime ReleaseData { get; set; }
        public string ReleaseDateStr { get; set; }
        public GamePatform Platform { get; set; }

        public void ProcessEntry()
        {
            Price = ConvertToDouble(this.PriceStr);
            ReleaseData = ConvertToDateTime(ReleaseDateStr);
        }



        private double ConvertToDouble(string price)
        {
            string priceWithoutSymbols = price.Replace("₪", "").Replace("Бесплатно", "0.0").Replace("$", "");
            double finalPrice = Convert.ToDouble(priceWithoutSymbols, CultureInfo.InvariantCulture);
            return finalPrice;
        }


        private static DateTime ConvertToDateTime(string releaseDateItem)
        {
            string[] formats = { "d MMM. yyyy", "dd MMM. yyyy","d MMM yyyy","dd MMM yyyy","d MMMM yyyy","dd MMMM yyyy","d MMMM. yyyy","dd MMMM. yyyy",
                                 "d mmm. yyyy", "dd mmm. yyyy","d mmm yyyy","dd mmm yyyy","d mmmm yyyy","dd mmmm yyyy","d mmmm. yyyy","dd mmmm. yyyy"};
            DateTime releaseDate;
            CultureInfo russianCulture = new CultureInfo("en-EN");
            foreach (string format in formats)
            {
                if (DateTime.TryParseExact(releaseDateItem, format, russianCulture, DateTimeStyles.None, out releaseDate))
                {
                    return releaseDate.Date;
                }
            }
            throw new FormatException($"Invalid date format: {releaseDateItem}");
        }


    }

    public enum GamePatform
    {
        Win,
        Mac,
        Both
    }


}
