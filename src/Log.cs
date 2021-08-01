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

        public static void Info(object data)
        {
            logger.LogInfo(data);
        }
        
        public static void Warning(object data)
        {
            logger.LogWarning(data);
        }

        public static void Error(object data)
        {
            logger.LogError(data);
        }
        
        public static void Debug(object data)
        {
            logger.LogDebug(data);
        }

    }
}
