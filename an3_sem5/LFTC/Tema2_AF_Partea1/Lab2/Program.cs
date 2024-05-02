using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

/****************

EBNF descriere automat finit:

automat_finit = { declaratie "\n" {"\n"} } ["#done"].
declaratie = tranzitie | decl_init | decl_fin.

tranzitie  = stare atom stare.

decl_init  = "#s" stare.
decl_fin   = "#f" stare { stare }.

*******************/

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var f = File.OpenRead(args[0]);
            var af = new AF("Q0 0 Q1\nQ0 1 Q1\n#s Q0\n#f Q1");

            Console.WriteLine("Help:");
            Console.WriteLine("  exit");
            Console.WriteLine("  afload <file|stdin> [filename]");
            Console.WriteLine("  afview");

            while(true)
            {               
                Console.Write(">> ");
                var command = Console.ReadLine().Split('"')
                     .Select((element, index) => index % 2 == 0
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                           : new string[] { element })
                     .SelectMany(element => element).ToArray();                
                if (command.Length == 0) continue;

                try
                {
                    var action = command[0].ToLowerInvariant();
                    if (action == "exit")
                    {
                        Environment.Exit(0);
                        continue;
                    }

                    if (action == "afload")
                    {
                        var option = command.Length > 1 ? command[1].ToLowerInvariant() : "stdin";

                        if (option == "file") 
                        {
                            var filename = command.Length > 2 ? command[2] : "";
                            af = new AF(File.OpenRead(filename), closeAfterReading: true);
                        }                   
                        else if(File.Exists(option))
                        {
                            af = new AF(File.OpenRead(option), closeAfterReading: true);
                        }
                        else
                        {
                            Console.WriteLine("Type in the AF data. Use #done when ready");
                            af = new AF(Console.In);
                        }
                        continue;
                    }
                    if(action=="afview")
                    {
                        Console.WriteLine(af);
                        continue;
                    }
                    if(action=="afmatch")
                    {
                        var expr = command.Length > 1 ? command[1] : "";
                        var result = af.IsMatch(expr);
                        Console.WriteLine(result);
                    }
                    if (action == "aflongestmatch") 
                    {
                        var expr = command.Length > 1 ? command[1] : "";
                        var result = "";

                        Console.WriteLine(af.LongestMatch(expr));
                        /*
                        for(int i=0;i<=expr.Length;i++)
                        {
                            var substr = expr.Substring(0, i);
                            if(af.IsMatch(substr))
                            {
                                result = substr;
                            }                            
                        }                        
                        Console.WriteLine(result);*/
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("An error occured");
                    Console.WriteLine(e.Message);
                }                
            }            
        }
    }
}
