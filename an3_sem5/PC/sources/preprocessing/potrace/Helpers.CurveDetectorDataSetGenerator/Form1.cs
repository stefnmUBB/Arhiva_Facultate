using Licenta.Commons.Math;
using Licenta.Commons.Math.Arithmetics;
using Licenta.Commons.OS;
using Licenta.Commons.Utils;
using Licenta.Imaging;
using Licenta.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Helpers.CurveDetectorDataSetGenerator
{

    public partial class Form1 : Form
    {
        Bitmap InputImage = new Bitmap(64, 64, PixelFormat.Format32bppPArgb);
        Graphics InputGraphics;

        Bitmap PredImage = new Bitmap(256, 256, PixelFormat.Format32bppPArgb);
        Graphics PredGraphics;

        void TestSingle()
        {
            var img = ImageRGB.FromFile("D:\\Users\\Stefan\\Datasets\\JL\\015.jpg");
            //var img = ImageRGB.FromFile("C:\\Users\\Stefan\\Desktop\\EMNIST-Image.png", 32, 32);
            //var img = ImageRGB.FromFile("D:\\Users\\Stefan\\fuck_it_licenta\\textcaps-master\\emnist_bal_200\\images\\original reconstruction.png");
            //var img = ImageRGB.FromFile("D:\\Users\\Stefan\\Datasets\\reteta.png");

            var r = img.ApplyPotrace();
            Display.Show(new ImageRGB(r.Bitmap));

        }

        public Form1()
        {
            InitializeComponent();
            typeof(Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(InputImg, true);            

            InputGraphics = Graphics.FromImage(InputImage);
            InputGraphics.Clear(Color.White);

            PredGraphics = Graphics.FromImage(PredImage);
            PredGraphics.Clear(Color.Transparent);

            //TestSingle();


            var inPath = @"D:\Users\Stefan\Datasets\IAM_Words\iam_words";
            var outPath = @"D:\Users\Stefan\fuck_it_licenta\proiect_licenta\app\Licenta\Helpers.CurveDetectorDataSetGenerator\bin\Debug\iam_w_bez";

            var bkp = @"D:\Users\Stefan\Datasets\IAM_Words\iam_words\words\a03\a03-059";
            
            foreach (var f in Directory.EnumerateFiles(inPath, "*.png", SearchOption.AllDirectories)) 
            {
                if (f.CompareTo(bkp) < 0) continue;
                Console.WriteLine(f);

                var outBmp = outPath + f.Substring(inPath.Length);
                var outDir = Path.GetDirectoryName(outBmp);
                var outBez = Path.Combine(outDir, Path.GetFileNameWithoutExtension(f) + ".txt");
                Directory.CreateDirectory(outDir);

                try
                {
                    var fimg = ImageRGB.FromFile(f);
                    var fr = fimg.ApplyPotrace();

                    fr.Bitmap.Save(outBmp);
                    fr.Bitmap.Dispose();

                    var paths = "";
                    foreach (var curve in fr.Curves)
                    {
                        paths += "#path\n";
                        foreach (var corner in curve)
                        {
                            paths += $"{corner.B0.X} {corner.B0.Y} {corner.A.X} {corner.A.Y} {corner.B1.X} {corner.B1.Y} {corner.IsCurved} {corner.Alpha}\n";
                        }
                    }
                    File.WriteAllText(outBez, paths);
                }
                catch(Exception e)
                {
                    File.WriteAllText($"err{DateTime.Now.Ticks}.txt", $"{e.Message}\r\n{e.StackTrace}\n\n{e}");
                }
            }
            
            
            //var img = ImageRGB.FromFile(@"C:\Users\Stefan\Desktop\Untitled.png");            

            //Display.Show(img, "015");
            //var r = img.PartialCannyEdgeDetection();
            //r.ToBitmap().Save("res.png");*/            

        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            
        }                

        private void InputImg_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(InputImage, 0, 0, 256, 256);
            e.Graphics.DrawImage(PredImage, 0, 0, 256, 256);
        }

        bool msDown = false;
        Point msPt;

        private void InputImg_MouseDown(object sender, MouseEventArgs e)
        {
            msPt = new Point(e.X / 4, e.Y / 4);
            msDown = true;
        }

        private void InputImg_MouseLeave(object sender, System.EventArgs e)
        {
            msDown = false;
        }

        private void InputImg_MouseMove(object sender, MouseEventArgs e)
        {
            if (!msDown) return;
            var pt = new Point(e.X / 4, e.Y / 4);

            InputGraphics.DrawLine(Pens.Black, msPt, pt);

            msPt = pt;
            InputGraphics.Flush(System.Drawing.Drawing2D.FlushIntention.Flush);
            InputImg.Invalidate();
        }

        private void InputImg_MouseUp(object sender, MouseEventArgs e)
        {
            msDown = false;
        }

        private void ProcessButton_Click(object sender, System.EventArgs e)
        {
            var img = new ImageRGB(InputImage);
            var dect = img.CannyEdgeDetectionMatrix();
            var pyInput = dect.Items.JoinToString(" ");

            Debug.WriteLine(pyInput);

            var (output, err) = ProcessUtil.Run("python", "main.py", pyInput);

            PredGraphics.Clear(Color.Transparent);


            if (err!=null)
            {
                var doubles = output.Split(new char[] { ' ', '\n' }, System.StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
                Debug.WriteLine(doubles.JoinToString(" "));

                double[] buffer = new double[13];

                for(int i=0;i<8;i++)
                {
                    for (int j = 0; j < 13; j++)
                        buffer[j] = 64 + doubles[13 * i + j] * 64 * 2;
                    buffer[0] = (buffer[0] - 32) / 2;
                    CurveEncoder.Decode(buffer, out var bez, out var th);

                    bez.Draw(PredGraphics, 10, th, Color.Green);
                }                
            }
            PredGraphics.Flush(System.Drawing.Drawing2D.FlushIntention.Flush);
            InputImg.Invalidate();
        }
    }
}
