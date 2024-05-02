using FestivalSellpoint.Domain;
using FestivalSellpoint.Repo.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FestivalSellpoint.Repo
{
    public class AngajatDbRepo : ORMDb<int, Domain.Angajat, Models.Angajat>, IAngajatRepo
    {
        private static readonly ILog log = LogManager.GetLogger("AngajatDbRepo");

        public AngajatDbRepo(IDictionary<string, string> props) : base(typeof(FestivalSellpointContext), "Angajat")
        {

        }

        public Domain.Angajat FindByCredentials(string username, string password)
        {
            log.Info($"{typeof(Domain.Angajat)} : Finding by credentials {username}, {password}");
            return GetAllProcessed(s => s.Where(a => a.Username == username && a.Password == password))
                .FirstOrDefault();
        }       
    }
}
