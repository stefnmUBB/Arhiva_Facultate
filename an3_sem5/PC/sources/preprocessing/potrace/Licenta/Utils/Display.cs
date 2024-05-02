using Licenta.Imaging;
using Licenta.Utils.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Licenta.Utils
{
    public static class Display
    {
        public static void Show(ImageRGB image, string title = "ImageRGB")
        {
            var form = new ImageRGBDisplayForm
            {
                Image = image,
                Text = title
            };                       

            form.ShowDialog();
        }


    }
}
