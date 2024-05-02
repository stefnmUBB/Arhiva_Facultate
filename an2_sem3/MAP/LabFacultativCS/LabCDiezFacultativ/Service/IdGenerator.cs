using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabCDiezFacultativ.Service
{
    public abstract class IdGenerator<ID>
    {
        public abstract ID Generate();
    }

    public class LongIdGenerator : IdGenerator<long>
    {
        public override long Generate()
        {            
            return (long)((DateTime.Now - new DateTime(2023, 1, 1, 0, 0, 0)).Ticks);
        }
    }
}
