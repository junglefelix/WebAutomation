using Microsoft.VisualBasic;
using OpenQA.Selenium.DevTools.V120.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SteamInfra.DataModels
{
    public class SystemRequirements
    {
        public double memory;
        public double storage;

        public SystemRequirements(string memory, string storage)
        {
            this.memory = GetMemory(memory);
            this.storage = GetStorage(storage);
        }

        public SystemRequirements(double memory, double storage)
        {
            this.memory = memory;
            this.storage = storage;
        }

        private double GetStorage(string storage)
        {
            string pattern = @"Storage: (\d+) GB available space";

            Match match = Regex.Match(storage, pattern);

            string pattern2 = @"Storage: (\d+) MB available space";

            Match match2 = Regex.Match(storage, pattern2);


            // If a match is found, extract and return the version number as a double
            // my specifications are in GB so if System requirements on page are specified in MB, need to divide by 1024 to get proper comparison
            if (match.Success)
            {
                string storageStr = match.Groups[1].Value;
                if (double.TryParse(storageStr, out double storageSize))
                {
                    return storageSize;
                }
            }
            else if (match2.Success)
            {
                string storageStr = match2.Groups[1].Value;
                if (double.TryParse(storageStr, out double storageSize))
                {
                    return storageSize / 1024;
                }
            }
            return 0;
        }

        private double GetMemory(string memory)
        {
            string pattern = @"Memory: (\d+) GB RAM";

            Match match = Regex.Match(memory, pattern);

            string pattern2 = @"Memory: (\d+) MB RAM";

            Match match2 = Regex.Match(memory, pattern2);

            // If a match is found, extract and return the version number as a double
            // my specifications are in GB so if System requirements on page are specified in MB, need to divide by 1024 to get proper comparison
            if (match.Success)
            {
                string memoryStr = match.Groups[1].Value;
                if (double.TryParse(memoryStr, out double memorySize))
                {
                    return memorySize;
                }
            }
            else if (match2.Success)
            {
                string memoryStr = match2.Groups[1].Value;
                if (double.TryParse(memoryStr, out double memorySize))
                {
                    return memorySize / 1024;
                }
            }
            return 0;
        }
    }
}
