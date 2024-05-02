using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.Repo;
using ISS.Biblioteca.Commons.Service;
using ISS.Biblioteca.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.Commons.Server
{
    internal class Program
    {
        private static void PopulateDb()
        {            
            var adminRepo = new AdministratorITRepo();
            var carteRepo = new CarteRepo();
            var exemplarRepo = new ExemplarCarteRepo();

            adminRepo.Add(new AdministratorIT
            {
                Nume = "root",
                Adresa = "Str. Demiurgului Nr. 1",
                Cnp = "5000000111111",
                Telefon = "0700111222",
                TokenLogare = "0000"
            });

            var carti = new List<Carte>();

            carti.Add(new Carte
            {
                Titlu = "Baltagul",
                Autor = "Mihail Sadoveanu",
                Isbn = "9789735911997"
            });

            carti.Add(new Carte
            {
                Titlu = "Idiotul",
                Autor = "Fyodor Dostoievski",
                Isbn = "9780226159621"
            });

            carti.Add(new Carte
            {
                Titlu = "Русский язык в картинках",
                Autor = "И. В. Баранников, Л. А. Варковицкая",
                Isbn = "9785170907434"
            });
            carti.Add(new Carte
            {
                Titlu = "Biblia",
                Autor = "",
                Isbn = "9780007103072"
            });
            carti.Add(new Carte
            {
                Titlu = "Cititorul din pestera",
                Autor = "Rui Zink",
                Isbn = "9786067798746"
            });
            carti.Add(new Carte
            {
                Titlu = "Regula paralelogramului",
                Autor = "Alexandra Stanescu",
                Isbn = "9789731838090 "
            });

            var rnd = new Random();
            foreach(var carte in carti)
            {
                carteRepo.Add(carte);
                var cnt = 15 + rnd.Next() % 30;
                for(int i=0;i<cnt;i++)
                {
                    exemplarRepo.Add(new ExemplarCarte
                    {
                        Carte = carte,
                        Status = StatusCarte.Disponibil
                    });
                }
            }
        }

        static void Main(string[] args)
        {
            /*var repo = new AdministratorITRepo();
            repo.Add(new AdministratorIT
            {
                Nume = "root",
                Adresa = "Str. Demiurgului Nr. 1",
                Cnp = "5000000111111",
                Telefon = "0700111222",
                TokenLogare = "0000"
            });

            var x = repo.FindByCredentials("5000000111111", "0000");
            Console.WriteLine(x.Nume);*/

            var service = new BaseService(
                new AbonatRepo(),
                new AdministratorITRepo(),
                new BibliotecarRepo(),
                new CarteRepo(),
                new ExemplarCarteRepo(),
                new ImprumutRepo(),
                new ReturRepo()
                );

            //PopulateDb();

            var server = new BibliotecaServer(Config.Host, Config.Port, new ServiceImpl(service));
            server.Start();

            Console.WriteLine("Server started ...");
            Console.ReadLine();
            Console.WriteLine("Server stopped ...");
            Console.ReadLine();
            Environment.Exit(0);            
        }
    }
}
