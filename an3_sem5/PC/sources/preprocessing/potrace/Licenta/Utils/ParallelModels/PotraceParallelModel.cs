using Licenta.Commons.Math.Arithmetics;
using Licenta.Commons.Math;
using Licenta.Commons.Parallelization;
using Licenta.Imaging;
using System.Collections.Generic;
using Licenta.Commons.Utils;
using System;
using System.Linq;
using System.Security.AccessControl;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.InteropServices;

namespace Licenta.Utils.ParallelModels
{   
    using DoubleMatrix = Matrix<DoubleNumber>;    

    internal class PotraceParallelModel : ParallelGraphModel
    {        
        int ThurdSize = 30;
        public PotraceParallelModel()
        {
            var inputImage = CreateInput<ImageRGB>();
            var options = CreateInput<PotraceOptions>();
            var monochromeFilter = CreateNestedModelNode(new MonochromeParallelModel(0), inputImage, resultIndex: 0);
            var result = CreateOutput<DoubleMatrix, PotraceOptions>(Potrace.Process, monochromeFilter, options);
        }
        public PotraceOutput Run(ImageRGB image, TaskManager tm = null) => Run(tm, new object[] { image, new PotraceOptions() })[0] as PotraceOutput;
        public PotraceOutput RunSync(ImageRGB image) => RunSync(new object[] { image, new PotraceOptions() })[0] as PotraceOutput;                
    }
}
