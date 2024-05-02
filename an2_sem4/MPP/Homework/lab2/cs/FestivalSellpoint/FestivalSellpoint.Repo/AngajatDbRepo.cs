using FestivalSellpoint.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalSellpoint.Repo
{
    public class AngajatDbRepo : DatabaseRepoUtils<int, Angajat>, IAngajatRepo
    {
        public AngajatDbRepo(IDictionary<string, string> props) : base(props)
        {
        }

        public void Add(Angajat e)
        {
            int result = ExecuteNonQuery("insert into \"Angajat\" (\"username\", \"password\", \"email\") " +
                "values (@username, @password, @email)", new Dictionary<string, object>
                {
                    { "@username", e.Username },
                    { "@password", e.Password },
                    { "@email", e.Email }
                });
            if (result == 0)
                throw new RepositoryException("Angajat was not added");
        }

        public Angajat findByCredentials(string username, string password)
        {
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
            return Select("select * from \"Angajat\"");
        }

        public Angajat GetById(int id)
        {
            return SelectFirst("select * from \"Angajat\" where \"id\"=@id", new Dictionary<string, object>
            {
                { "@id", id },
            });
        }

        public void Remove(int id)
        {
            ExecuteNonQuery("delete from \"Angajat\" where \"id\"=@id", new Dictionary<string, object>
            {
                { "@id", id },
            });
        }

        public void Update(Angajat e)
        {
            throw new RepositoryException("Angajat update is not allowed");
        }

        protected override Angajat DecodeReader(IDataReader reader)
        {
            var x = reader["id"];
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
