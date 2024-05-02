using FestivalSellpoint.Domain;
using FestivalSellpoint.Repo.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FestivalSellpoint.Repo
{
    public class SpectacolDbRepo : ORMDb<int, Domain.Spectacol, Models.Spectacol>, ISpectacolRepo
    {
        private static readonly ILog log = LogManager.GetLogger("SpectacolDbRepo");
        public SpectacolDbRepo(IDictionary<string, string> props) : base(typeof(FestivalSellpointContext), "Spectacol")
        {
        }

        /*
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
        */

        public IEnumerable<Domain.Spectacol> GetBetweenDates(DateTime start, DateTime end)
        {
            log.Info($"{typeof(Domain.Spectacol)} : Getting between dates {start}, {end}");
            return GetAllProcessed(s => s.AsEnumerable().Where(a => start <= a.Data.DateTimeFromBytes() && a.Data.DateTimeFromBytes() <= end));          
        }             
    }
}
