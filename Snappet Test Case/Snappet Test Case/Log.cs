using NLog;

namespace Snappet_Test_Case
{
    public static class Log
    {
        private readonly static Logger log = LogManager.GetCurrentClassLogger();
        
        public static void LogInfo(string message)
        {
            log.Info(message);
        }

        public static void LogError(string error)
        {
            log.Error(error);
        }
    }
}
