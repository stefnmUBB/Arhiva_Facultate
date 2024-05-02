using FestivalSellpoint.Domain;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;

namespace FestivalSellpoint.Repo
{
    public class AngajatDbRepo : DatabaseRepoUtils<int, Angajat>, IAngajatRepo
    {
        private static readonly ILog log = LogManager.GetLogger("AngajatDbRepo");
        public AngajatDbRepo(IDictionary<string, string> props) : base(props)
        {
        }

        public void Add(Angajat e)
        {
            log.Info($"Adding Angajat: {e}");
            int result = ExecuteNonQuery("insert into \"Angajat\" (\"username\", \"password\", \"email\") " +
                "values (@username, @password, @email)", new Dictionary<string, object>
                {
                    { "@username", e.Username },
                    { "@password", e.Password },
                    { "@email", e.Email }
                });
            if (result == 0)
            {
                log.Error($"Angajat was not added: {e}");
                throw new RepositoryException("Angajat was not added");
            }
            log.Info($"Added successful");
        }

        public Angajat findByCredentials(string username, string password)
        {
            log.Info($"Finding by credentials: {username}, {password}");
            return SelectFirst("select * from \"Angajat\" where " +
                "\"username\"=@username and \"password\"=@password",
                new Dictionary<string, object>
                {
                    { "@username", username },
                    { "@password", password },                    
                });
        }

        public IEnumerable<Angajat> GetAll()
        {
            log.Info($"Selecting all Angajati");
            return Select("select * from \"Angajat\"");
        }

        public Angajat GetById(int id)
        {
            log.Info($"Finding Angajat with id {id}");
            return SelectFirst("select * from \"Angajat\" where \"id\"=@id", new Dictionary<string, object>
            {
                { "@id", id },
            });
        }

        public void Remove(int id)
        {
            log.Info($"Deleting Angajat with id {id}");
            var r=ExecuteNonQuery("delete from \"Angajat\" where \"id\"=@id", new Dictionary<string, object>
            {
                { "@id", id },
            });
            if (r > 0)
                log.Info($"Deleted Angajat with id {id}");
        }

        public void Update(Angajat e)
        {
            throw new RepositoryException("Angajat update is not allowed");
        }

        protected override Angajat DecodeReader(IDataReader reader)
        {
            log.Info($"Deconding Angajat");
            var id = Convert.ToInt32(reader["id"]);
            var username = reader["username"] as string;
            var password = reader["password"] as string;
            var email = reader["email"] as string;            
            var angajat = new Angajat(username, password, email);
            angajat.Id = id;
            return angajat;
        }
    }
}
