using LabCDiezFacultativ.Domain;
using LabCDiezFacultativ.Properties;
using LabCDiezFacultativ.Repo;
using LabCDiezFacultativ.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace LabCDiezFacultativ.Service
{
    public static class Populator
    {
        static List<string> FirstNames = Resources.fnames.Split('\n').ToList();
        static List<string> LastNames = Resources.lnames.Split('\n').ToList();

        static Random rand = new Random();

        static T Choose<T>(List<T> list)
        {
            if (list.Count == 0) return default;
            return list[rand.Next() % list.Count];
        }

        private static string RandomStudentName()
        {
            return $"{Choose(LastNames)} {Choose(FirstNames)}";

        }

        public static void Populate()
        {
            File.Delete("data/echipa.xml");
            File.Delete("data/jucator.xml");
            File.Delete("data/jucatoractiv.xml");
            File.Delete("data/meci.xml");

            Service<Echipa> echipaServ = new Service<Echipa>(DefaultRepositories.EchipaRepo);            
            Service<Jucator> jucatorServ = new Service<Jucator>(DefaultRepositories.JucatorRepo);            
            Service<JucatorActiv> jucatorActivServ = new Service<JucatorActiv>(DefaultRepositories.JucatorActivRepo);            
            Service<Meci> meciServ = new Service<Meci>(DefaultRepositories.MeciRepo);

            echipaServ.IdGenerator = new LongIdGenerator();
            jucatorServ.IdGenerator = new LongIdGenerator();
            jucatorActivServ.IdGenerator = new LongIdGenerator();
            meciServ.IdGenerator = new LongIdGenerator();

            List<Echipa> echipe = new List<Echipa>();
            Dictionary<Echipa, string> scoala = new Dictionary<Echipa, string>();

            Resources.echipe.Split('\n').ToList().ForEach(line => 
            {
                var cols = line.Split(';');
                string s = cols[0];
                string e = cols[1];
                scoala[new Echipa(e)] = s;
            });

            echipe = scoala.Keys.ToList();

            foreach(var e in echipe)
            {
                e.Id = echipaServ.Add(e).Id;

                for(int i=0;i<15;i++)
                {
                    Jucator j = new Jucator(RandomStudentName(), scoala[e], e.Id);
                    jucatorServ.Add(j);
                }
            }

            DateTime startDate = new DateTime(2023, 1, 1);
            DateTime endDate = new DateTime(2023, 1, 15);
            var rnd = new Random();

            List<JucatorActiv> chooseJucatoriActivi(Echipa e, Meci m, int count)
            {                
                return jucatorServ.Filter(j => j.IdEchipa == e.Id).OrderBy(j => rnd.Next()).Take(count)
                    .Select(j =>new JucatorActiv(j.Id, m.Id, rnd.Next() % 5, 
                        rnd.Next() % 4 == 3 ? TipJucatorActiv.Rezerva : TipJucatorActiv.Participant))
                    .ToList();
            }

            int x = 0;
            foreach(var e1 in echipe)
            {
                foreach(var e2 in echipe)
                {
                    if (e1.Id == e2.Id) continue;
                    Meci meci = new Meci(e1.Id, e2.Id, Date.RandomBetween(startDate, endDate));
                    meciServ.Add(meci);
                    Debug.WriteLine($"Saved! {x++}");
                    foreach (var ja in chooseJucatoriActivi(e1, meci, 8 + rnd.Next() % 8))
                    {
                        jucatorActivServ.Add(ja);
                    }

                    foreach (var ja in chooseJucatoriActivi(e2, meci, 8 + rnd.Next() % 8))
                    {
                        jucatorActivServ.Add(ja);
                    }
                }
            }

            //echipe.ForEach(e => Debug.WriteLine(e.Nume));
            //scoala.ToList().ForEach((kv) => Debug.WriteLine(kv.Key.Nume + " : " + kv.Value));


        }
    }
}
