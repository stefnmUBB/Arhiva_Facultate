using AI.Lab10.Tools;
using AI.Lab10.Tools.Clustering;
using AI.Lab10.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab10.Controls.Tabs
{
    public partial class IrisTab : UserControl
    {
        public IrisTab()
        {
            InitializeComponent();
        }

        private void Run()
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                return;

            var data = DataSets.Flowers;

            var tt = data.SplitTrainTest();


            var kmeans = new KMeans(3);
            kmeans.Train(tt.Train.Select(_ => _.Features).ToArray());

            int[,] classes = new int[3, 3];
            int adjustClass(int cat, int index)
            {
                classes[cat, data[index].Type]++;
                return cat;
            }

            var x = tt.Train.Select(_ => (_.Features[0], _.Features[1], adjustClass(kmeans.GetClusterSingle(_.Features), _.Index))).ToArray();
            var p = tt.Test.ToArray().Select(_ => (_.Features[0], _.Features[1], kmeans.GetClusterSingle(_.Features))).ToArray();

            // remap clusters to their originated groups
            // it may happen for the clusters to generate correctly, but have wrong labels
            // therefore swap their ids when needed
            int[] mx = new int[3];
            for (int i = 0; i < 3; i++)
            {                
                for (int j = 0; j < 3; j++)
                {
                    if (classes[i, j] > classes[mx[j], j])
                        mx[j] = i;
                    Console.Write($"{classes[i, j]} ");
                }
                Console.WriteLine();
            }

            while (mx.Distinct().Count() < 3) 
            {
                var vals = mx.Distinct().ToArray();
                var missingValue = 0;
                for (missingValue = 0; missingValue < 3; missingValue++)
                    if (!vals.Contains(missingValue))
                        break;
                for(int i=0;i<3;i++)
                    for(int j=i+1;j<3;j++)
                        if (mx[i] == mx[j])
                        {
                            mx[i] = missingValue;
                            break;
                        }
            }

            var t = tt.Test.ToArray().Select(_ => (_.Features[0], _.Features[1], mx[_.Type])).ToArray();

            Invoke(new Action(() => 
            {
                legendTTPlot1.AddLegend(mx[0], "setosa");
                legendTTPlot1.AddLegend(mx[1], "versicolor");
                legendTTPlot1.AddLegend(mx[2], "virginica");
                legendTTPlot1.AddLegend(3, "centroids");

                legendTTPlot1.SetTrainData(x);
                legendTTPlot1.SetTestData(t);
                legendTTPlot1.SetPredictedData(p);
                legendTTPlot1.Zoom = 200;
                legendTTPlot1.Plot();
            }));            
        }

        private void DoRun()
        {
            RetryButton.Enabled = false;
            Task.Run(() =>
            {
                Run();
                Invoke(new Action(() => RetryButton.Enabled = true));
            });
        }

        private void IrisTab_Load(object sender, EventArgs e)
        {
            DoRun();
        }

        private void RetryButton_Click(object sender, EventArgs e)
        {
            DoRun();
        }
    }
}
