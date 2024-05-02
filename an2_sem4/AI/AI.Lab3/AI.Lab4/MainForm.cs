using AI.Commons.Data;
using AI.Commons.UI.Controls;
using AI.Commons.Utils;
using AI.Lab4.Data;
using AI.Lab4.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab4
{
    public partial class MainForm : Form
    {
        ShortestPathParams ProblemParams;
        ShortestPathPopulation Population;

        ShortestPathParams GetParamsFromFile(string path)
        {
            var lines = File.ReadAllLines(path);
            var graph = CitiesParser.FromCitiesLines(lines);
            int start = int.Parse(lines[lines.Length - 2]) - 1;
            int finish = int.Parse(lines[lines.Length - 1]) - 1;

            numericBoxValid = false;
            StartNodeBox.Value = start + 1;
            FinishNodeBox.Value = finish + 1;
            numericBoxValid = true;

            return new ShortestPathParams(graph, start, finish);            
        }

        void BuildCostMatrixView()
        {
            CostMatrixView.Rows.Clear();
            CostMatrixView.Columns.Clear();            
            int height = ProblemParams.EdgeCost.GetLength(0);
            int width = ProblemParams.EdgeCost.GetLength(1);

            CostMatrixView.ColumnCount = width;

            for (int r = 0; r < height; r++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(CostMatrixView);

                for (int c = 0; c < width; c++)
                {
                    row.Cells[c].Value = ProblemParams.EdgeCost[r, c];
                }
                row.HeaderCell.Value = (r + 1).ToString();

                CostMatrixView.Rows.Add(row);
            }

            for (int c = 0; c < width; c++)
                CostMatrixView.Columns[c].HeaderCell.Value = (c + 1).ToString();
        }

        public MainForm()
        {            
            InitializeComponent();            
            ProblemParams = GetParamsFromFile(@"easy_01_tsp.txt");
            ShortestPathRadio.Checked = true;
            BuildCostMatrixView();

            Population = new ShortestPathPopulation(ProblemParams);
            Population.LocalBestFound += Population_LocalBestFound;
            Population.StepEnd += Population_StepEnd;

            GodsView.DataSource = null;
            GodsView.DataSource = Gods;

            MutationBar.Value = 30;
            MaxAgeBar.Value = 100;

            Population.SelectionFactor = 4;
            GraphDesignerCombo.SelectedIndex = 1;
        }

        int Generation = 0;
        private void Population_StepEnd(object sender)
        {
            BestFitnessCapture.Add(-Population.Scope.First().Chromosome.Fitness);

            if (Commons.Utils.Random.Decision(0.3) && Population.Gods.Count > 0) 
            {
                var god = Commons.Utils.Random.ValueFromContainer(Population.Gods);
                var chromo = new ShortestPathChromosome(ProblemParams);
                chromo.SetRepresentation(god.Chromosome.Representation.ToArray());
                chromo.Mutate();
                Population.AddIndividual(chromo);                
            }            

            if (ProblemParams.Goal==ShortestPathGoal.Cycle)
            {

            }

            Invoke(new Action(() =>
            {
                IndividualsCountLabel.Text = $"Individuals Count = {Population.Count}";
                GenerationLabel.Text = $"Generation : {Generation++}";
                BestFitnessLabel.Text = $"Current Best : {-Population.Scope.FirstOrDefault()?.Chromosome?.Fitness}";
            }));
        }

        private void Population_LocalBestFound(object sender, Commons.Algorithms.Genetic.IChromosome<ShortestPathParams, int[], long> target)
        {            
            Invoke(new Action(() =>
            {
                Gods.Clear();
                Population.Gods.Select((g, i) => (g, i)).ToList()
                    .ForEach(x => Gods.Add(new GodRecord(x.i + 1, x.g.Chromosome)));
                GodsView.Refresh();

                if (Generation % 10 != 0) return;
                               
                if (Gods.Count>0 && GraphDesignerCombo.SelectedIndex==1)
                {
                    GraphEditor.HighlightPath(Gods[0].Chromosome.Representation,
                        ProblemParams.Goal == ShortestPathGoal.Cycle);
                }
            }));
        }

        bool Loaded = false;
        private void MainForm_Load(object sender, EventArgs e)
        {
            Loaded = true;
        }


        void StartSimulation()
        {            
            if (!Paused)
            {                
               

                Population.Scope.Clear();
                Population.Gods.Clear();
                Gods.Clear();                                             

                if (GraphDesignerCombo.SelectedIndex == 1) 
                {
                    var graph = GraphEditor.GetGraph();
                    ProblemParams = new ShortestPathParams(graph, 0, graph.Nodes.Count - 1);                    
                }

                Population.SelectionFactor = ProblemParams.Graph.Nodes.Count < 25 ? 4 : 2;

                ProblemParams.StartNode = Math.Min(ProblemParams.Graph.Nodes.Count, (int)StartNodeBox.Value) - 1;
                ProblemParams.FinishNode = Math.Min(ProblemParams.Graph.Nodes.Count, (int)FinishNodeBox.Value) - 1;
                ProblemParams.Goal = ShortestPathRadio.Checked ? ShortestPathGoal.Normal
                    : AllNodesRadio.Checked ? ShortestPathGoal.AllNodes : ShortestPathGoal.Cycle;

                ProblemParams.MissingNodePenalty = (int)MissingNodePenaltyBox.Value;
                ProblemParams.DupeNodePenalty = (int)DupeNodePenaltyBox.Value;

                Population.ProblemParam = ProblemParams;

                if (ProblemParams.StartNode == ProblemParams.FinishNode) 
                {
                    MessageBox.Show("Soruce and destination must be different");
                    return;
                }

                if (ProblemParams.Graph.Nodes.Count < 2)
                {
                    MessageBox.Show("Input graph should have at least 2 nodes");
                    return;
                }

                Population.Populate();
            }           

            /*Debug.WriteLine(ProblemParams.Graph.Nodes.Count);
            for (int i = 0; i < ProblemParams.Graph.Nodes.Count; i++)
            {
                for (int j = 0; j < ProblemParams.Graph.Nodes.Count; j++)
                    Debug.Write(ProblemParams.EdgeCost[i, j].ToString() + " ");
                Debug.Write("\n");
            } */           

            Running = true;
            StartStopButton.Text = "Stop";

            StartNodeBox.Enabled = false;
            FinishNodeBox.Enabled = false;
            SolutionTypePanel.Enabled = false;
            GraphEditor.Enabled = false;
            
            Paused = false;

            Task.Run(Simulate);
        }

        bool Running = false;
        bool Paused = false;

        void Simulate()
        {
            while (Running && Population.Scope.Count + Population.Gods.Count > 0)
            {
                try
                {
                    Population.RunStep();
                    Thread.Sleep(10);

                    if (Generation % 500 == 0) 
                    {
                        Console.WriteLine($"--------------------{Generation}-------------------------");
                        Population.Scope.ToList().ForEach(Console.WriteLine);
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    MessageBox.Show("Something wrong happened:\n" + ex.Message);
                    Running = false;
                    Invoke(new Action(() => { throw ex; }));
                    break;                    
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                    throw ex;
                }
            }
        }

        class GodRecord
        {
            public ShortestPathChromosome Chromosome;
            public GodRecord(int id, ShortestPathChromosome chromo)
            {
                Id = id;
                Chromosome = chromo;
            }
            public int Id { get; }

            public string Path => string.Join(" ", Chromosome.Representation.Select(x => x + 1));

            public double Fitness => -Chromosome.Fitness;

        }

        BindingList<GodRecord> Gods = new BindingList<GodRecord>();

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if (!Running)
            {
                StartSimulation();
            }
            else
            {
                StopSimulation();
            }
        }


        void StopSimulation()
        {
            Invoke(new Action(() =>
            {
                Running = false;
                StartStopButton.Text = "Start";
                Paused = true;
                GodsView.Enabled = true;
            }));
        }

        void ResetSimulation()
        {
            StopSimulation();
            Paused = false;

            Population.Scope.Clear();
            Population.Gods.Clear();
            Generation = 0;

            Gods.Clear();
            GodsView.DataSource = null;
            GodsView.DataSource = Gods;

            IndividualsCountLabel.Text = "Individuals Count = 0";

            StartNodeBox.Enabled = true;
            FinishNodeBox.Enabled = true;
            SolutionTypePanel.Enabled = true;
            GraphEditor.Enabled = true;
        }

        private void MutationBar_Scroll(object sender, EventArgs e)
        {
            Population.MutationProbability = MutationBar.Value * 0.01;
        }

        private void MaxAgeBar_Scroll(object sender, EventArgs e)
        {
            Population.MaxLifespan = MaxAgeBar.Value;
        }        

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetSimulation();
        }

        private void GraphDesignerCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GraphDesignerCombo.SelectedIndex;

            if (index == 0)
            {
                if (CityFileLoader.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if(Loaded)
                            ResetSimulation();
                        ProblemParams = GetParamsFromFile(CityFileLoader.FileName);
                        Population.ProblemParam = ProblemParams;
                        CostMatrixView.Show();
                        GraphEditor.Hide();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"Could not load graph.\n{ex.Message}");
                    }
                }

            }
            else if (index == 1) 
            {
                GraphEditor.Viewer.HighlightedPath.Clear();
                GraphEditor.Viewer.Segments.Clear();
                if (Loaded)
                    ResetSimulation();
                CostMatrixView.Hide();
                GraphEditor.Show();                
            }
        }

        bool numericBoxValid = true;

        private void StartNodeBox_ValueChanged(object sender, EventArgs e)
        {
            if (!numericBoxValid) return;
            ProblemParams.StartNode = (int)StartNodeBox.Value - 1;
        }

        private void FinishNodeBox_ValueChanged(object sender, EventArgs e)
        {
            if (!numericBoxValid) return;
            ProblemParams.FinishNode = (int)FinishNodeBox.Value - 1;
        }

        private void ShortestPathRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (ShortestPathRadio.Checked)
                ProblemParams.Goal = ShortestPathGoal.Normal;
        }

        private void AllNodesRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (AllNodesRadio.Checked)
                ProblemParams.Goal = ShortestPathGoal.AllNodes;
        }

        private void CycleRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (CycleRadio.Checked)
                ProblemParams.Goal = ShortestPathGoal.Cycle;
        }

        private void GodsView_SelectionChanged(object sender, EventArgs e)
        {
            if(GodsView.SelectedCells.Count==1)
            {
                GodsView.Rows[GodsView.SelectedCells[0].RowIndex].Selected = true;
                return;
            }

            if(GodsView.SelectedRows.Count==1)
            {
                var repr = (GodsView.SelectedRows[0].DataBoundItem as GodRecord).Chromosome.Representation;
                GraphEditor.HighlightPath(repr, ProblemParams.Goal == ShortestPathGoal.Cycle);
            }

        }

        private void MissingNodePenaltyBox_ValueChanged(object sender, EventArgs e)
        {
            ProblemParams.MissingNodePenalty = (int)MissingNodePenaltyBox.Value;
        }

        private void DupeNodePenaltyBox_ValueChanged(object sender, EventArgs e)
        {
            ProblemParams.DupeNodePenalty = (int)DupeNodePenaltyBox.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loader = new CoordsLoaderForm();
            if(loader.ShowDialog()==DialogResult.OK)
            {
                GraphEditor.Viewer.HighlightedPath.Clear();
                GraphEditor.Viewer.Segments.Clear();
                if (Loaded)
                    ResetSimulation();
                GraphEditor.Viewer.Points.AddRange(loader.Points);
                CostMatrixView.Hide();
                GraphEditor.Show();
            }
        }

        List<long> BestFitnessCapture = new List<long>();

        private void button2_Click(object sender, EventArgs e)
        {
            new PlotForm(BestFitnessCapture).ShowDialog();
        }
    }
}
