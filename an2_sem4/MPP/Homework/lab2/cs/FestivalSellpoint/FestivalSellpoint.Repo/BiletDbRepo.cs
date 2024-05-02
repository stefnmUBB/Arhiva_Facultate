using FestivalSellpoint.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalSellpoint.Repo
{
    public class BiletDbRepo : DatabaseRepoUtils<int, Bilet>, IBiletRepo
    {
        ISpectacolRepo SpectacolRepo;

        public BiletDbRepo(IDictionary<string, string> props, ISpectacolRepo spectacolRepo) : base(props)
        {
            SpectacolRepo = spectacolRepo;
        }

        public void Add(Bilet e)
        {
            int result = ExecuteNonQuery("insert into \"Bilet\" (\"numeCumparator\", \"spectacol\", \"nrLocuri\") " +
                "values (@numeC, @spectacol, @nrLocuri)", new Dictionary<string, object>
                {
                    { "@numeC", e.NumeCumparator },
                    { "@spectacol", e.Spectacol.Id},
                    { "@nrLocuri", e.NrLocuri},
                });
            if (result == 0)
                throw new RepositoryException("Bilet was not added");
        }

        public IEnumerable<Bilet> GetAll()
        {
            return Select("select * from \"Bilet\"");
        }

        public Bilet GetById(int id)
        {
            return SelectFirst("select * from \"Bilet\" where \"id\"=@id", new Dictionary<string, object>
            {
                { "@id", id },
            });
        }

        public IEnumerable<Bilet> getBySpectacol(Spectacol spectacol)
        {
            return Select("select * from \"Bilet\" where \"spectacol\"=@spectacol", new Dictionary<string, object>
            {
                { "@spectacol", spectacol.Id },
            });
        }

        public void Remove(int id)
        {
            throw new NotImplementedException("Cannot remove Bilet");
        }

        public void Update(Bilet e)
        {
            throw new NotImplementedException("Cannot update Bilet");
        }

        protected override Bilet DecodeReader(IDataReader reader)
        {
            var numeC = reader["numeCumparator"] as string;
            var spectId = Convert.ToInt32(reader["spectacol"]);
            var nrLocuri = Convert.ToInt32(reader["nrLocuri"]);
            var id = Convert.ToInt32(reader["id"]);

            var b = new Bilet(numeC, nrLocuri, SpectacolRepo.GetById(spectId));
            b.Id = id;
            return b;
        }
    }
}
