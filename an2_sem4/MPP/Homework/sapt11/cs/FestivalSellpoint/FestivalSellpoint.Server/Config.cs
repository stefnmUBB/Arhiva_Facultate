using System.Collections.Generic;
using System.Configuration;

namespace FestivalSellpoint.Server
{
    internal static class Config
    {
        public static string GetConnectionStringByName(string name)
            => ConfigurationManager.ConnectionStrings[name]?.ConnectionString;

        public static string ConnectionType
            => ConfigurationManager.AppSettings.Get("ConnectionType");

        public static string Host => ConfigurationManager.AppSettings.Get("Host");
        public static int Port => int.Parse(ConfigurationManager.AppSettings.Get("Port"));

        public static readonly IDictionary<string, string> DatabaseProperties = new Dictionary<string, string>
            {
                { "ConnectionString",  GetConnectionStringByName("FestivalDb") },
                { "ConnectionType", ConnectionType}
            };
    }
}
