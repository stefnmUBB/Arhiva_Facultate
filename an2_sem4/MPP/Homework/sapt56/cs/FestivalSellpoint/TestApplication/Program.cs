
using System.Collections.Generic;
using System;
using FestivalSellpoint.Repo;
using FestivalSellpoint.Domain;

namespace TestApplication
{
    internal class Program
    {

        static void AddSpectacole(SpectacolDbRepo repo)
        {
            var artists = new string[]
                {"Artist1", "Artist2", "Artist3", "Artist4"};

            var locatii = new string[]
                {"Buzau", "Cluj", "Sibiu", "Timisoara"};

            var rand = new Random();
            for (int i = 1; i < 100; i++) 
            {                
                var day = rand.Next(1, 30);
                var spectacol = new Spectacol(
                    artists[rand.Next(artists.Length)],
                    new DateTime(2023, 3, day, 0, 0, 0),
                    locatii[rand.Next(locatii.Length)],
                    rand.Next(30, 100),
                    0
                    );

                repo.Add(spectacol);
            }
        }
        static void Main(string[] args)
        {
            var angajatRepo = new AngajatDbRepo(Config.DatabaseProperties);
            var spectacolRepo = new SpectacolDbRepo(Config.DatabaseProperties);
            var biletRepo = new BiletDbRepo(Config.DatabaseProperties, spectacolRepo);

            AddSpectacole(spectacolRepo);
            return;

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
