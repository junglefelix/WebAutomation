using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTests.Helpers
{
    public class LogHelper
    {
        public static ILogger Create(string logFileName)
        {
            if (LogManager.Configuration == null)
            {
                ConfigLog(logFileName);
            }
            return LogManager.GetCurrentClassLogger();
        }


        private static void ConfigLog(string fileName)
        {
            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget target =
                new FileTarget
                {
                    FileName = fileName,
                    EnableArchiveFileCompression = true,
                    EnableFileDelete = true,
                    DeleteOldFileOnStartup = true,
                    Layout = "${date} | ${level} | ${logger:shortName=true} | ${message}"
                };
            config.AddTarget("logfile", target);
            LoggingRule rule = new LoggingRule("*", LogLevel.Debug, target);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;
        }
    }
}
