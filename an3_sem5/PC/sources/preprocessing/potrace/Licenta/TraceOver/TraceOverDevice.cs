using Licenta.Commons.Math.Arithmetics;
using Licenta.Commons.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.TraceOver
{
    using DoubleMatrix = Matrix<DoubleNumber>;

    public class TraceOverDevice
    {
        public DoubleMatrix Source { get; }

        public TraceOverDevice(DoubleMatrix source)
        {
            Source = source;
        }



    }
}
