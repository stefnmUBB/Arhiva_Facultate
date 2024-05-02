using AI.Commons.Algorithms.Genetic;
using AI.Commons.Data;
using AI.Commons.IO;
using AI.Commons.UI.Controls;
using AI.Lab3.Data;
using AI.Lab3.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab3
{
    public partial class MainForm : Form
    {
        Graph<int> Graph = GraphReader.FromGML(Resources.in_karate).NormalizeNodes();
        //Graph<int> Graph = GraphReader.FromGMLFile(@"Input\easy_test3.gml").NormalizeNodes();
        CommunityFinderPopulation Population;

        public MainForm()
        {            
            InitializeComponent();
            ViewComboBox.SelectedIndex = 0;
            ViewComboBox.SelectedValueChanged += ViewComboBox_SelectedValueChanged;

            ChromosomeSelector.SelectedValueChanged += ChromosomeSelector_SelectedValueChanged;
            ChromosomeSelector.SelectedIndex = 0;

            AlphaBox.Value = (decimal)CommunityScoreFitnessChromosome.Alpha;
            AlphaBox.ValueChanged += AlphaBox_ValueChanged;


            Population = new CommunityFinderPopulation(Graph);            

            //Population.Populate(100, typeof(DensityFitnessChromosome));
            Population.Populate(100);

            MutationBar.Value = 30;
            MaxAgeBar.Value = 100;

            Population.StepEnd += Pop_StepEnd;
            Population.LocalBestFound += Pop_LocalBestFound;            

            GodsView.DataSource = Gods;

            InitMenu();
            
        }

        private void AlphaBox_ValueChanged(object sender, EventArgs e)
        {
            CommunityScoreFitnessChromosome.Alpha = (double)AlphaBox.Value;
        }

        Type ChromosomeType;

        private void ChromosomeSelector_SelectedValueChanged(object sender, EventArgs e)
        {            
            ChromosomeType = Assembly.GetExecutingAssembly()
                .GetType($"AI.Lab3.Data.{ChromosomeSelector.Text}FitnessChromosome");

            AlphaBox.Enabled = (ChromosomeSelector.Text == "CommunityScore");
        }

        void AddDefaultTest(string name)
        {
            var item = new ToolStripMenuItem();
            item.Click += DefaultTest_Click;
            item.Text = name;
            loadGraphToolStripMenuItem.DropDownItems.Add(item);
        }

        private void DefaultTest_Click(object sender, EventArgs e)
        {
            ResetSim();
            Graph = GraphReader.FromGML(typeof(Properties.Resources)
                    .GetProperty($"in_{(sender as ToolStripMenuItem).Text.ToLower()}"
                    , BindingFlags.NonPublic | BindingFlags.Static).GetValue(null) as byte[])
                .NormalizeNodes();
            Communities = null;
            if (ViewComboBox.SelectedItem as string == "Community")
            {
                ViewComboBox.SelectedIndex = 0;                
            }

            if(Graph.Nodes.Count<300)
            {
                GraphViewer.RenderGraph(Graph, Communities);
            }            
            ResetSim();
        }

        void InitMenu()
        {            
            AddDefaultTest("Karate");
            AddDefaultTest("Dolphins");
            AddDefaultTest("Football");
            AddDefaultTest("Krebs");            
            AddDefaultTest("community");            
            AddDefaultTest("easy_test");           
            AddDefaultTest("easy_test2");            
            AddDefaultTest("easy_test3");            
            AddDefaultTest("easy_test4");
            AddDefaultTest("net_in");           
        }

        private void ViewComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var sel = ViewComboBox.SelectedItem as string;

            if (sel == "Community" && Communities == null)
                sel = "Circular";

            if(sel=="Circular")
            {
                GraphViewer.GraphRenderer = new CircularGraphRenderer<int>();
            }
            else if(sel=="Force-Based")
            {
                GraphViewer.GraphRenderer = new ForceBasedGraphRenderer<int>();
            }
            else if(sel=="Community")
            {                
                GraphViewer.GraphRenderer = new CommunityGraphRenderer<int>(null, Communities);
            }
            if (Graph.Nodes.Count < 300)
            {
                GraphViewer.RenderGraph(Graph, Communities);
            }
        }

        private void Pop_LocalBestFound(object sender, IChromosome<Graph<int>, int[], double> target)
        {
            Invoke(new Action(() =>
            {
                Gods.Clear();
                Population.Gods.Select((g, i) => (g, i)).ToList()
                    .ForEach(x => Gods.Add(new GodRecord(x.i + 1, x.g.Chromosome)));
                //GodsView.DataSource = null;
                //GodsView.DataSource = Gods;
                //GodsView.DataBindings = new ControlBindingsCollection(Gods);
                GodsView.Refresh();
            }));           
        }

        class GodRecord
        {
            public CommunityFindingChromosome Chromosome;            
            public GodRecord(int id, CommunityFindingChromosome chromo)
            {
                Id = id;
                Chromosome = chromo;                
            }
            public int Id { get; }

            public double Fitness => Chromosome.Fitness;

        }
        
        BindingList<GodRecord> Gods = new BindingList<GodRecord>();

        int Generation = 0;

        List<List<int>> Communities = null;        

        private void Pop_StepEnd(object sender)
        {
            var p = sender as CommunityFinderPopulation;            

            Generation++;            
            if (p.Scope.Count > 0)
            {
                Communities = p.Scope.First().Chromosome.ToCommunities();

                BeginInvoke(new Action(() =>
                {
                    if (Generation % 10 == 0)
                    {
                        if (Graph.Nodes.Count < 300)
                        {
                            if (ViewComboBox.SelectedItem as string == "Community")
                            {
                                GraphViewer.GraphRenderer = new CommunityGraphRenderer<int>(null, Communities);
                            }
                            GraphViewer.RenderGraph(Graph, Communities);
                        }
                    }
                    GenerationLabel.Text = $"Gen {Generation}";
                    IndividualsCountLabel.Text = $"Individuals Count = {p.Scope.Count}";
                    
                }));
            }            
        }

        void Simulate()
        {
            while (Running && Population.Scope.Count + Population.Gods.Count > 0) 
            {
                try
                {
                    Population.RunStep();
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                    break;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ResetSim();
        }

        private void MutationBar_Scroll(object sender, EventArgs e)
        {
            Population.MutationProbability = MutationBar.Value * 0.01;
        }

        private void MaxAgeBar_Scroll(object sender, EventArgs e)
        {
            Population.MaxLifespan = MaxAgeBar.Value;
        }

        bool Running = false;
        bool Paused = false;

        void StartSim()
        {
            if (Running) return;
            Running = true;
            StartStopButton.Text = "Stop";
            ChromosomeSelector.Enabled = false;
            GodsView.Enabled = false;

            if (!Paused)
                Population.Populate(100, ChromosomeType);

            Task.Run(Simulate);                        
        }

        void StopSim()
        {
            Invoke(new Action(() =>
            {
                Running = false;
                StartStopButton.Text = "Start";
                Paused = true;
                GodsView.Enabled = true;
            }));
        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if(!Running)
            {
                StartSim();
            }
            else
            {
                StopSim();
            }
        }

        void ResetSim()
        {
            StopSim();

            Paused = false;

            ChromosomeSelector.Enabled = true;

            Population = new CommunityFinderPopulation(Graph);
            Population.StepEnd += Pop_StepEnd;
            Population.LocalBestFound += Pop_LocalBestFound;
            Gods.Clear();
            Generation = 0;

            GodsView.DataSource = null;
            GodsView.DataSource = Gods;

            MutationBar.Value = 30;
            MaxAgeBar.Value = 100;

            IndividualsCountLabel.Text = "Individuals Count = 0";            
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetSim();
        }

        private void GodsView_SelectionChanged(object sender, EventArgs e)
        {
            if(GodsView.SelectedCells.Count==1)
            {
                GodsView.CurrentRow.Selected = true;
            }
            if (!GodsView.Enabled)
                return;
            if(GodsView.SelectedRows.Count==1)
            {
                var data = GodsView.SelectedRows[0].DataBoundItem as GodRecord;
                Communities = data.Chromosome.ToCommunities();

                if (ViewComboBox.SelectedItem as string == "Community")
                {
                    GraphViewer.GraphRenderer = new CommunityGraphRenderer<int>(null, Communities);
                }

                if(Graph.Nodes.Count<300)
                    GraphViewer.RenderGraph(Graph, Communities);

                LabelsBox.Text = string.Join(" ", data.Chromosome.Representation);
                CommunitiesBox.Text = string.Join(Environment.NewLine
                    , Communities.Select(c => string.Join(" ", c.Select(x => x + 1))));
            }
        }

        private void loadGMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = Application.StartupPath;
            dialog.Filter = "GML files (*.gml)|*.gml";
            if(dialog.ShowDialog()==DialogResult.OK)
            {
                ResetSim();
                try
                {
                    Graph = GraphReader.FromGMLFile(dialog.FileName).NormalizeNodes();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error loading graph:\n" + ex.Message);
                    return;
                }
                Communities = null;
                if (ViewComboBox.SelectedItem as string == "Community")
                {
                    ViewComboBox.SelectedIndex = 0;
                }

                if (Graph.Nodes.Count < 300)
                    GraphViewer.RenderGraph(Graph, Communities);
                ResetSim();

            }
        }
    }
}
