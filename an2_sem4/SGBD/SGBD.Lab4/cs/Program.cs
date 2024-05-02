using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SGBD.Lab4.Deadlock
{
    internal class Program
    {
        static SqlConnection GetConnection() => new SqlConnection(@"Data Source=DESKTOP-9HMTO67\SQLEXPRESS;Initial Catalog=DepozitColectie;Integrated Security=True");
        static SqlDataAdapter DataAdapter = new SqlDataAdapter();

        static void Execute(string procName)
        {
            using (var conn = GetConnection())
            using (var command = new SqlCommand(procName, conn) { CommandType = CommandType.StoredProcedure }) 
            {
                conn.Open();
                var reader = command.ExecuteReader();
                if(reader.Read())
                {
                    Console.WriteLine($"[{procName}] {reader[0]}");
                }
                else
                {
                    Console.WriteLine($"[{procName}] Error - No Output");
                }                
            }
        }


        static void Main(string[] args)
        {
            var rand = new Random();
            while (true)
            {
                Console.WriteLine("Running...");
                if (rand.NextDouble() < 0.5)
                {
                    var t1 = Task.Run(() => Execute("DeadLock1"));
                    var t2 = Task.Run(() => Execute("DeadLock2"));
                    t1.Wait();
                    t2.Wait();
                }             
                else
                {
                    var t1 = Task.Run(() => Execute("DeadLock2"));
                    var t2 = Task.Run(() => Execute("DeadLock1"));
                    t1.Wait();
                    t2.Wait();
                }
                Console.WriteLine("OK");
            }
            Console.ReadLine();
        }
    }
}
