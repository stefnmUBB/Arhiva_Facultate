using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.Server
{
    internal static class Config
    {             
        public static string Host => ConfigurationManager.AppSettings.Get("Host");
        public static int Port => int.Parse(ConfigurationManager.AppSettings.Get("Port"));        
    }
}
