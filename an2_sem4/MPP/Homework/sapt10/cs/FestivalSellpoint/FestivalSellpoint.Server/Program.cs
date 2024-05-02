using FestivalSellpoint.Network.Utils;
using FestivalSellpoint.Repo;
using FestivalSellpoint.Service;
using System;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using FestivalSellpoint.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace FestivalSellpoint.Server
{
    internal static class Program
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
