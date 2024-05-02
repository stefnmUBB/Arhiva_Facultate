using FestivalSellpoint.Repo;
using FestivalSellpoint.Service;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FestivalSellpoint.Server
{
    internal class Program
    {
      
        static void Main(string[] args)
        {
            var angajatRepo = new AngajatDbRepo(Config.DatabaseProperties);
            var spectacolRepo = new SpectacolDbRepo(Config.DatabaseProperties);
            var biletRepo = new BiletDbRepo(Config.DatabaseProperties, spectacolRepo);

            var angajatServ = new AngajatService(angajatRepo);
            var biletServ = new BiletService(biletRepo);
            var spectacolServ = new SpectacolService(spectacolRepo);

            var appService = new AppService(angajatServ, biletServ, spectacolServ);           

            var server = new Server(Config.Host, Config.Port, new ServiceImpl(appService));
            server.Start();

            Console.WriteLine("Server started ...");            
            Console.ReadLine();
            Console.WriteLine("Server stopped ...");
            Environment.Exit(0);
        }
    }
}
