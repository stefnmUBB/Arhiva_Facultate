using LabCDiezFacultativ.Domain;
using LabCDiezFacultativ.Repo;
using LabCDiezFacultativ.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LabCDiezFacultativ.Service
{
    public class NBAService
    {
        public Service<long, Echipa> EchipaServ { get; }
        public Service<long, Jucator> JucatorServ { get; }
        public Service<long, JucatorActiv> JucatorActivServ { get; }
        public Service<long, Meci> MeciServ { get; }

        public NBAService(Service<long, Echipa> echipaServ, Service<long, Jucator> jucatorServ,
            Service<long, JucatorActiv> jucatorActivServ, Service<long, Meci> meciServ)
        {
            EchipaServ = echipaServ;
            JucatorServ = jucatorServ;
            JucatorActivServ = jucatorActivServ;
            MeciServ = meciServ;
        }

        public IEnumerable<Echipa> Echipe { get => EchipaServ.GetAll(); }
        public IEnumerable<Jucator> Jucatori { get => JucatorServ.GetAll(); }
        public IEnumerable<JucatorActiv> JucatoriActivi { get => JucatorActivServ.GetAll(); }
        public IEnumerable<Meci> Meciuri { get => MeciServ.GetAll(); }

        public IEnumerable<Jucator> GetJucatori(Echipa echipa)
        {
            return JucatorServ.Filter(j => j.IdEchipa == echipa.Id);
        }

        public IEnumerable<JucatorActiv> GetJucatoriActivi(Echipa Echipa, Meci meci)
        {
            return JucatorActivServ.Filter(ja => ja.IdMeci == meci.Id)
                .Where(ja =>
                {
                    Jucator j = JucatorServ.GetById(ja.IdJucator);
                    return j.IdEchipa == Echipa.Id;
                });
        }

        public IEnumerable<Meci> GetMeciuri(DateTime start, DateTime end)
        {
            return MeciServ.Filter(m => m.Data.IsBetween(start, end));
        }

        public Meci GetMeci(Echipa e1, Echipa e2)
        {
            return MeciServ.Filter(m => m.IdEchipa[0] == e1.Id && m.IdEchipa[1] == e2.Id).FirstOrDefault();
        }

        public Dictionary<Echipa, int> GetScore(Meci meci)
        {
            Dictionary<Echipa, int> result = new Dictionary<Echipa, int>();

            for (int i = 0; i < 2; i++)
            {
                Echipa echipa = EchipaServ.GetById(meci.IdEchipa[i]);
                result[echipa] = GetJucatoriActivi(echipa, meci)
                    .Sum(ja => ja.NrPuncteInscrise);
            }

            return result;
        }

        private static NBAService _DefaultInstance = null;

        public static NBAService DefaultInstance
        {
            get => _DefaultInstance ?? (_DefaultInstance = CreateDefaultInstance());
        }

        private static NBAService CreateDefaultInstance()
        {
            Service<Echipa> echipaServ = new Service<Echipa>(DefaultRepositories.EchipaRepo);
            Service<Jucator> jucatorServ = new Service<Jucator>(DefaultRepositories.JucatorRepo);
            Service<JucatorActiv> jucatorActivServ = new Service<JucatorActiv>(DefaultRepositories.JucatorActivRepo);
            Service<Meci> meciServ = new Service<Meci>(DefaultRepositories.MeciRepo);

            echipaServ.IdGenerator = new LongIdGenerator();
            jucatorServ.IdGenerator = new LongIdGenerator();
            jucatorActivServ.IdGenerator = new LongIdGenerator();
            meciServ.IdGenerator = new LongIdGenerator();

            return new NBAService(echipaServ, jucatorServ, jucatorActivServ, meciServ);
        }
    }
}
