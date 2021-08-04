using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;

namespace grtg_mp
{
    //Simple wrapper for BepInEx's logging system so that we can use it everwhere. Set in Mod.cs
    static class Log
    {
        public static ManualLogSource logger;

        public static void SetupLogSourceIfLoggerNotSet()
        {
            if(logger == null)
            {
                logger = BepInEx.Logging.Logger.CreateLogSource("early-grtg-mp");
            }
        }

        public static void Info(object data)
        {
            SetupLogSourceIfLoggerNotSet();
            logger.LogInfo(data);
        }
        
        public static void Warning(object data)
        {
            SetupLogSourceIfLoggerNotSet();
            logger.LogWarning(data);
        }

        public static void Error(object data)
        {
            SetupLogSourceIfLoggerNotSet();
            logger.LogError(data);
        }
        
        public static void Debug(object data)
        {
            SetupLogSourceIfLoggerNotSet();
            logger.LogDebug(data);
        }

    }
}
