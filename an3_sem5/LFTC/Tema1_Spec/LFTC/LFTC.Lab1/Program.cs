using LFTC.Lexic;
using System;
using System.IO;
using System.Linq;

namespace LFTC.Lab1
{
    internal class Program
    {
        private static void Process(string code)
        {
            Console.WriteLine("_________________________________________________");            
            var atomExtractor = new AtomExtractor();
            var tokens = atomExtractor.SplitToTokens(code);
            Console.WriteLine("\nTokens : ");
            tokens.ForEach(Console.WriteLine);

            var symTable = new SymbolsTable(tokens);

            Console.WriteLine();
            Console.WriteLine(symTable.TS.Select(_ => (_.Id, _.Text)).AsTable("Id", "Valoare"));
            Console.WriteLine();
            Console.WriteLine(symTable.FIP.AsTable("Cod Atom", "Poz. in TS"));

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Reserved words and symbols");
            Console.WriteLine(SymbolsTable.ReservedAtomsList.Select((_, i) => (_, i)).AsTable("Id", "Text"));

            args.Select(File.ReadAllText).ToList().ForEach(Process);
            Console.WriteLine("\nDone.");
            Console.ReadLine();
        }
    }
}
