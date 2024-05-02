using FestivalSellpoint.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace FestivalSellpoint.Repo
{
    public class SpectacolDbRepo : DatabaseRepoUtils<int, Spectacol>, ISpectacolRepo
    {
        public SpectacolDbRepo(IDictionary<string, string> props) : base(props)
        {
        }

        public void Add(Spectacol s)
        {
            int result = ExecuteNonQuery("insert into \"Spectacol\" (\"artist\", \"data\", \"locatie\"," +
                "\"nrLocuriDisponibile\", \"nrLocuriVandute\") " +
                "values (@artist, @data, @locatie, @nrDisp, @nrVand)", new Dictionary<string, object>
                {
                    { "@artist", s.Artist },
                    { "@data", s.Data},
                    { "@locatie", s.Locatie },
                    { "@nrDisp", s.NrLocuriDisponibile },
                    { "@nrVand", s.NrLocuriVandute },                    
                });
            if (result == 0)
                throw new RepositoryException("Sepctacol was not added");
        }

        public IEnumerable<Spectacol> GetAll()
        {
            return Select("select * from \"Spectacol\"");
        }

        public IEnumerable<Spectacol> getBetweenDates(DateTime start, DateTime end)
        {
            return Select("select * from \"Spectacol\" where \"date\" between @start and @end", new Dictionary<string, object>
            {
                { "@start", start },
                { "@end", end },
            });
        }

        public Spectacol GetById(int id)
        {
            return SelectFirst("select * from \"Spectacol\" where \"id\"=@id", new Dictionary<string, object>
            {
                { "@id", id },
            });
        }

        public void Remove(int id)
        {
            ExecuteNonQuery("delete from \"Spectacol\" where \"id\"=@id", new Dictionary<string, object>
            {
                { "@id", id },
            });
        }

        public void Update(Spectacol s)
        {
            int result = ExecuteNonQuery("update \"Spectacol\" set " +
                "\"artist\" = @artist, " +
                "\"data\" = @data, " +
                "\"locatie\" = @locatie, " +
                "\"nrLocuriDisponibile\" = @nrDisp, " +
                "\"nrLocuriVandute\" = @nrVand) " +
                "values (@artist, @data, @locatie, @nrDisp, @nrVand) where \"id\" = @id", new Dictionary<string, object>
                {
                    { "@artist", s.Artist },
                    { "@data", s.Data},
                    { "@locatie", s.Locatie },
                    { "@nrDisp", s.NrLocuriDisponibile },
                    { "@nrVand", s.NrLocuriVandute },
                    { "@id", s.Id }
                });
            if (result == 0)
                throw new RepositoryException("Angajat was not updated");
        }

        protected override Spectacol DecodeReader(IDataReader reader)
        {
            var artist = reader["artist"] as string;
            var data = (DateTime)reader["data"];
            var locatie = reader["locatie"] as string;
            var nrDisp = Convert.ToInt32(reader["nrLocuriDisponibile"]);
            var nrVand = Convert.ToInt32(reader["nrLocuriVandute"]);
            var id = Convert.ToInt32(reader["id"]);
            var s = new Spectacol(artist, data, locatie, nrDisp, nrVand);
            s.Id = id;
            return s;
        }
    }
}
