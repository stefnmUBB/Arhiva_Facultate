using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCDiezFacultativ.Utils
{
    internal static class Date
    {
        public static bool IsBetween(this DateTime date, DateTime start, DateTime end)
        {
            return start <= date && date <= end;
        }

        static Random rand = new Random();

        public static DateTime RandomBetween(DateTime start, DateTime end)
        {
            long ticks = (end - start).Ticks;
            if (ticks <= 0) return start;

            long r = ((long)rand.Next() << 32) | (long)rand.Next();

            return new DateTime(start.Ticks + r % ticks);
        }
    }
}
