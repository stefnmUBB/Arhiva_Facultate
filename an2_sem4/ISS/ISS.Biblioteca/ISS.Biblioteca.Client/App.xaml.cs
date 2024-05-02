using ISS.Biblioteca.Commons.Networking.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ISS.Biblioteca.Commons
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceObjectProxy Server = new ServiceObjectProxy("127.0.0.1", 15000);
    }
}
