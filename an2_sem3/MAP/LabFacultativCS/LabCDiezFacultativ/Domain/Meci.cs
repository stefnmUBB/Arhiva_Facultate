using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCDiezFacultativ.Domain
{
    public class Meci : Entity<long>
    {
        public Meci()
        {
            IdEchipa[0] = -1;
            IdEchipa[1] = -1;
            Data = DateTime.Now;
        }

        public Meci(long idEchipa1, long idEchipa2, DateTime date)
        {
            IdEchipa[0] = idEchipa1;
            IdEchipa[1] = idEchipa2;
            Data = date;
        }


        public long[] IdEchipa { get; set; } = new long[2];
        public DateTime Data { get; set; }
    }
}
