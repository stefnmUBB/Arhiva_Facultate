using FestivalSellpoint.Domain;
using FestivalSellpoint.Repo.Models;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;

namespace FestivalSellpoint.Repo
{
    public class BiletDbRepo : ORMDb<int, Domain.Bilet, Models.Bilet>, IBiletRepo
    {
        private static readonly ILog log = LogManager.GetLogger("BiletDbRepo");

        ISpectacolRepo SpectacolRepo;

        public BiletDbRepo(IDictionary<string, string> props, ISpectacolRepo spectacolRepo) : base(typeof(FestivalSellpointContext), "Bilet")
        {
            SpectacolRepo = spectacolRepo;            
        }

        /*
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
            log.Info($"Getting Bilet with id = {id}");
            return SelectFirst("select * from \"Bilet\" where \"id\"=@id", new Dictionary<string, object>
            {
                { "@id", id },
            });
        }
          public override void Remove(int id)
        {
            log.Error("Cannot remove Bilet");
            throw new NotImplementedException("Cannot remove Bilet");
        }

        public override void Update(Bilet e)
        {
            log.Error("Cannot update Bilet");
            throw new NotImplementedException("Cannot update Bilet");
        }      
         
         */

        public override void Add(Domain.Bilet b)
        {
            using (var db = GetContext())
            {
                var bilet = ModelAdapter.ToModelEntity(b);
                db.Attach(bilet.SpectacolNavigation);
                GetDbSet(db).Add(bilet);
                db.SaveChanges();
            }
        }

        public IEnumerable<Domain.Bilet> GetBySpectacol(Domain.Spectacol spectacol)
        {
            log.Info($"Getting Bilet by {spectacol}");            
            return GetAllProcessed(s => s.Where(b => b.Spectacol == spectacol.Id));            
        }
             
    }
}
