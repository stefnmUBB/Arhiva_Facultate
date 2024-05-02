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

namespace AI.Lab10.Controls.Misc
{
    public partial class ConfusionMatrix : UserControl
    {
        public ConfusionMatrix()
        {
            InitializeComponent();
        }

        public List<string> Labels { get; private set; }

        public void SetLabels(params string[] labels)
        {
            Labels = labels.ToList();
           
            var cnt = Labels.Count;

            Grid.Columns.Clear();
            for (int i = 0; i < cnt; i++)
                Grid.Columns.Add(Labels[i], Labels[i]);

            Grid.Rows.Clear();

            var h = Grid.Height - Grid.ColumnHeadersHeight;
            for (int i = 0; i < cnt; i++)
            {
                Grid.Rows.Add(new DataGridViewRow()
                {
                    HeaderCell = new DataGridViewRowHeaderCell { Value = Labels[i] },
                    Height = h / cnt
                });
            }
        }

        private void Grid_Resize(object sender, EventArgs e)
        {
            if (Grid.Rows.Count == 0) return;
            var h = Grid.Height - Grid.ColumnHeadersHeight;
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                Grid.Rows[i].Height = h / Grid.Rows.Count;
            }
        }

        private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == -1 && e.RowIndex >= 0) 
            {
                e.PaintBackground(e.CellBounds, true);
                e.Graphics.TranslateTransform(e.CellBounds.Left, e.CellBounds.Bottom);
                e.Graphics.RotateTransform(270);
                e.Graphics.DrawString(e.FormattedValue.ToString(), e.CellStyle.Font, Brushes.White, 5, 5);
                e.Graphics.ResetTransform();
                e.Handled = true;
            }
        }

        private void SetCell(int real, int predicted, int count)
        {
            Grid.Rows[real].Cells[predicted].Value = count;
        }

        int[,] Matrix = new int[1, 1];

        public void SetData(int[] real, int[] predicted)
        {
            int cnt = real.Max() + 1;

            Matrix = new int[cnt, cnt];

            for (int i = 0; i < real.Length; i++)
            {
                Matrix[real[i], predicted[i]]++;
            }

            for (int i = 0; i < cnt; i++)
            {
                for (int j = 0; j < cnt; j++)
                    SetCell(i, j, Matrix[i, j]);
            }

            ComputeStats();
        }

        public double Accuracy { get; private set; }
        public double[] Precision { get; private set; }
        public double[] Recall { get; private set; }

        public void ComputeStats()
        {
            int accuracy = 0;                      

            var confusionMatrix = Matrix;
            int len = confusionMatrix.GetLength(0);
            
            var tfpn = GetTFPN(confusionMatrix);

            var labels = new string[] { "TP", "TN", "FP", "FN" };
                  
            Precision = new double[len];
            Recall = new double[len];
            for (int i = 0; i < len; i++) 
            {
                Precision[i] = 1.0 * tfpn[TP, i] / (tfpn[TP, i] + tfpn[FP, i]);
                Recall[i] = 1.0 * tfpn[TP, i] / (tfpn[TP, i] + tfpn[FN, i]);                
            }
            
            for (int i = 0; i < len; i++)
            {
                accuracy += tfpn[TP, i];             
            }

            int s = 0;
            for (int i = 0; i < len; i++) for (int j = 0; j < len; j++) s += confusionMatrix[i, j];
            Accuracy = s == 0 ? 1 : 1.0 * accuracy / s;
        }

        public string GetStatsInfo()
        {
            return $"Accuracy = {Accuracy}\n" +
                $"Precision = {Precision.Select((p, i) => $"{Labels[i]}:{p:F4}").JoinToString(", ")}\n" +
                $"Recall = {Recall.Select((p, i) => $"{Labels[i]}:{p:F4}").JoinToString(", ")}\n";
        }

        const int TP = 0;
        const int TN = 1;
        const int FP = 2;
        const int FN = 3;

        public static int[,] GetTFPN(int[,] confMatrix)
        {
            var len = confMatrix.GetLength(0);
            int[,] result = new int[4, len];

            int sum = 0;

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (i == j) result[TP, i] += confMatrix[i, i];
                    else
                    {
                        result[FN, i] += confMatrix[i, j];
                        result[FP, j] += confMatrix[i, j];
                    }
                    sum += confMatrix[i, j];
                }
            }

            for (int i = 0; i < len; i++)
                result[TN, i] = sum - result[TP, i] - result[FP, i] - result[FN, i];

            return result;
        }
    }
}
