using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab11
{
    public static partial class DataSets
    {
        public static IEnumerable<Mat> FacesProvider(string srcFolder)
        {            
            var cc = new CascadeClassifier("./haarcascade_frontalface_default.xml");

            Console.WriteLine("Here?");
            foreach (var f in Directory.EnumerateFiles(srcFolder, "*.png", SearchOption.TopDirectoryOnly))
            {
                var m = new Mat();
                Cv2.CvtColor(new Mat(f), m, ColorConversionCodes.BGR2GRAY);
                var faces = cc.DetectMultiScale(m, 1.3, 3);
                foreach (var face in faces)
                    yield return new Mat(m, face);
            }
            yield break;                      
        }        
    }
}
