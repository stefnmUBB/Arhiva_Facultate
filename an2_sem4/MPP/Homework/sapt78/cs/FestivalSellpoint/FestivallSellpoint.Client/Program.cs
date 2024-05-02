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
            
            var server = new ServiceObjectProxy("127.0.0.1", 15000);
            server.ExceptionThrown += (e) => MessageBox.Show(e.InnerException.Message, "Server Error");

            Application.Run(new SpectacoleViewForm(server));
        }
    }
}
