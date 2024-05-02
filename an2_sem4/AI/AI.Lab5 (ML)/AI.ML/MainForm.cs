using AI.ML.Data;
using AI.ML.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.ML
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var csv = new CsvData(@"Input\sport.csv");

            var values = csv.ToObjects<SportPrediction>().Select(d => d.ToPredictedData()).ToList();
            values.ForEach(Console.WriteLine);

            MessageBox.Show(values.MeanError(SportData.DefaultDifference).ToString());

            //csv.Columns.ToList().ForEach(Console.WriteLine);
            

            /*foreach (var row in csv) 
            {
                Console.WriteLine(row["Waist"]);
            }*/
        }
    }
}
