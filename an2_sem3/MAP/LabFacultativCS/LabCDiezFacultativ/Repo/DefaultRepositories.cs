using LabCDiezFacultativ.Domain;
using System.IO;

namespace LabCDiezFacultativ.Repo
{
    internal static class DefaultRepositories
    {
        private static Repository<long, Echipa> _EchipaRepo = null;
        private static Repository<long, Jucator> _JucatorRepo = null;
        private static Repository<long, JucatorActiv> _JucatorActivRepo = null;
        private static Repository<long, Meci> _MeciRepo = null;

        public static Repository<long, Echipa> EchipaRepo
        {
            get => _EchipaRepo ?? (_EchipaRepo = CreateRepo<Echipa>());
        }

        public static Repository<long, Jucator> JucatorRepo
        {
            get => _JucatorRepo ?? (_JucatorRepo = CreateRepo<Jucator>());
        }

        public static Repository<long, JucatorActiv> JucatorActivRepo
        {
            get => _JucatorActivRepo ?? (_JucatorActivRepo = CreateRepo<JucatorActiv>());
        }

        public static Repository<long, Meci> MeciRepo
        {
            get => _MeciRepo ?? (_MeciRepo = CreateRepo<Meci>());
        }

        private static string StoragePath = "data";

        private static Repository<long, E> CreateRepo<E>() where E : Entity<long>
        {
            if(!Directory.Exists(StoragePath))
            {
                Directory.CreateDirectory(StoragePath);
            }
            string filename = typeof(E).Name.ToLower() + ".xml";
            filename = Path.Combine(StoragePath, filename);
            return new XMLRepository<long, E>(filename);
        }
    }
}
