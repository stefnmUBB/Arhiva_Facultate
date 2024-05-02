using ISS.Biblioteca.Commons.Networking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISS.Biblioteca.ClientTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var server = new ServiceObjectProxy("127.0.0.1", 15000);
            server.ExceptionThrown += (e) => Console.WriteLine(e.InnerException.Message);

            var user = server.Login("5000000111111", "0000");

            foreach (var c in server.GetCartiDisponibile()) 
            {
                Console.WriteLine(c);
            }

            Console.ReadLine();            
        }
    }
}
