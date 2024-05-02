using System.Configuration;
using System.Data.SQLite;

namespace ISS.Biblioteca.Commons.Utils
{
    internal static class Config
    {
        public static string GetConnectionStringByName(string name)
            => ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
        public static string ConnectionString => GetConnectionStringByName("Database");

        public static SQLiteConnection Connection = new SQLiteConnection(ConnectionString);
    }
}
