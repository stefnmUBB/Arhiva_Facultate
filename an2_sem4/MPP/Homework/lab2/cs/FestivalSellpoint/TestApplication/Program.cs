
using System.Collections.Generic;
using System;
using FestivalSellpoint.Repo;
using FestivalSellpoint.Domain;

namespace TestApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var angajatRepo = new AngajatDbRepo(Config.DatabaseProperties);
            var spectacolRepo = new SpectacolDbRepo(Config.DatabaseProperties);
            var biletRepo = new BiletDbRepo(Config.DatabaseProperties, spectacolRepo);

            biletRepo.Add(new Bilet("Marin", 3, spectacolRepo.GetById(1)));

            Console.WriteLine("\n\nShowing Angajati");
            Show(angajatRepo);
            Console.WriteLine("\n\nShowing Spectacole");
            Show(spectacolRepo);
            Console.WriteLine("\n\nShowing Bilete");
            Show(biletRepo);


            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        static void Show<ID, E>(IRepo<ID, E> repo) where E:Entity<ID>
        {            
            foreach(var item in repo.GetAll())
            {
                Console.WriteLine(item);
            }
        }


    }
}
