using Licenta.Commons.Math.Arithmetics;
using Licenta.Commons.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Commons
{
    public static class Initializer
    {
        public static void Run()
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(Reflection).TypeHandle);
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(OperativeConverter).TypeHandle);
        }
    }
}
