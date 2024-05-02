using FestivalSellpoint.Domain;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;

namespace FestivalSellpoint.Repo
{
    public class SpectacolDbRepo : DatabaseRepoUtils<int, Spectacol>, ISpectacolRepo
    {
        private static readonly ILog log = LogManager.GetLogger("SpectacolDbRepo");
        public SpectacolDbRepo(IDictionary<string, string> props) : base(props)
        {
        }

        public void Add(Spectacol s)
        {
            log.Info($"Adding spectacol {s}");

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
            {
                log.Error("Spectacol was not added");
                throw new RepositoryException("Spectacol was not added");
            }
            log.Info($"Added spectacol {s}");
        }

        public IEnumerable<Spectacol> GetAll() => Select("select * from \"Spectacol\"");        
        public IEnumerable<Spectacol> GetBetweenDates(DateTime start, DateTime end)
        {
            string startStr = start.ToString("yyyy-MM-dd");
            string endStr = start.ToString("yyyy-MM-dd");
            log.Info($"Getting spectacol between {start} and {end}");
            return Select("select * from Spectacol where strftime('%Y-%m-%d', \"data\") between @start and @end", new Dictionary<string, object>
            {
                { "@start", startStr },
                { "@end", endStr },
            });
        }

        public Spectacol GetById(int id)
        {
            log.Info($"Getting spectacol by {id}");
            return SelectFirst("select * from \"Spectacol\" where \"id\"=@id", new Dictionary<string, object>
            {
                { "@id", id },
            });
        }

        public void Remove(int id)
        {
            log.Info($"Deleting spectacol by {id}");
            var r = ExecuteNonQuery("delete from \"Spectacol\" where \"id\"=@id", new Dictionary<string, object>
            {
                { "@id", id },
            });
            if(r>0)
            {
                log.Info($"Deleted spectacol by {id}");
            }
        }

        public void Update(Spectacol s)
        {
            log.Info($"Updating spectacol {s}");
            int result = ExecuteNonQuery("update \"Spectacol\" set " +
                "\"artist\" = @artist, " +
                "\"data\" = @data, " +
                "\"locatie\" = @locatie, " +
                "\"nrLocuriDisponibile\" = @nrDisp, " +
                "\"nrLocuriVandute\" = @nrVand " +
                "where \"id\" = @id", new Dictionary<string, object>
                {
                    { "@artist", s.Artist },
                    { "@data", s.Data},
                    { "@locatie", s.Locatie },
                    { "@nrDisp", s.NrLocuriDisponibile },
                    { "@nrVand", s.NrLocuriVandute },
                    { "@id", s.Id }
                });
            if (result == 0)
                throw new RepositoryException("Spectacol was not updated");
            log.Info($"Updated spectacol {s}");
        }

        protected override Spectacol DecodeReader(IDataReader reader)
        {
            log.Info($"Decoding spectacol");
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
