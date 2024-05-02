using FestivalSellpoint.Client;
using FestivalSellpoint.Network.Client;
using FestivalSellpoint.UI.Forms;
using System;
using System.Windows.Forms;

namespace FestivallSellpoint.Client
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var server = new ServiceObjectProxy(Config.Host, Config.Port);
            server.ExceptionThrown += (e) => throw e;// MessageBox.Show((e.InnerException ?? e).Message, "Server Error");

            Application.Run(new SpectacoleViewForm(server));
        }
    }
}
