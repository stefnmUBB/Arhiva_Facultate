using FestivalSellpoint.Repo;
using FestivalSellpoint.Service;
using FestivalSellpoint.UI;
using FestivalSellpoint.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestUIApplication
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var angajatRepo = new AngajatDbRepo(Config.DatabaseProperties);
            var spectacolRepo = new SpectacolDbRepo(Config.DatabaseProperties);
            var biletRepo = new BiletDbRepo(Config.DatabaseProperties, spectacolRepo);

            var angajatServ = new AngajatService(angajatRepo);
            var biletServ = new BiletService(biletRepo);
            var spectacolServ = new SpectacolService(spectacolRepo);

            var appService = new AppService(angajatServ, biletServ, spectacolServ);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SpectacoleViewForm(appService));
        }
    }
}
