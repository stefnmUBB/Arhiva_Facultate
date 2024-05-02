using System.Configuration;

namespace FestivalSellpoint.Client
{
    internal static class Config
    {                
        public static string Host => ConfigurationManager.AppSettings.Get("Host");
        public static int Port => int.Parse(ConfigurationManager.AppSettings.Get("Port"));
        
    }
}
