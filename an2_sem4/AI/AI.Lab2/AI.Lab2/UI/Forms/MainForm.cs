using AI.Lab2.Algorithms;
using AI.Lab2.Data;
using AI.Lab2.IO;
using AI.Lab2.UI.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static AI.Lab2.Algorithms.NewmanFinder;

namespace AI.Lab2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();           

            GraphViewCombo.SelectedIndex = 2;

            TemplateInputCombobox.Items.AddRange(Directory.EnumerateFiles("Input").ToArray());

            FourTestsButton.Checked = true;

            TabControl.Appearance = TabAppearance.FlatButtons;
            TabControl.ItemSize = new Size(0, 1);
            TabControl.SizeMode = TabSizeMode.Fixed;

            GmlFileSelector.InitialDirectory = Application.StartupPath;

        }

        Graph<Dictionary<string, string>> InputGraph = null;
        List<List<int>> GraphCommunities = null;
        List<CommunityGrouping> Groupings = new List<CommunityGrouping>();       

        private void MainForm_Load(object sender, EventArgs e)
        {            
        }

        Control ChosenInput = null;

        void ChangeInput(Control newInput)
        {
            if(ChosenInput!=null)
            {
                ChosenInput.Hide();
            }
            ChosenInput = newInput;
            ChosenInput.Show();
        }

        private void FourTestsButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FourTestsButton.Checked)
            {
                ChangeInput(FourTestsCombobox);
                FourTestsCombobox.SelectedItem = null;
                FourTestsCombobox.SelectedIndex = 0;
            }
        }

        private void TemplateInputButton_CheckedChanged(object sender, EventArgs e)
        {
            if (TemplateInputButton.Checked)
            {
                ChangeInput(TemplateInputCombobox);
                TemplateInputCombobox.SelectedItem = null;
                TemplateInputCombobox.SelectedIndex = 0;
            }
        }

        private void FourTestsCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FourTestsCombobox.SelectedItem == null)
            {
                InputGraph = null;
                return;
            }
            string name = (FourTestsCombobox.SelectedItem as string).ToLower();

            try
            {
                InputGraph = GraphReader.FromGML(typeof(Properties.Resources)
                    .GetProperty($"in_{name}", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null) as byte[]);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Could not load graph:\n{ex.Message}");
            }
        }

        private void TemplateInputCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TemplateInputCombobox.SelectedItem == null)
            {
                InputGraph = null;
                return;
            }

            try
            {
                InputGraph = GraphReader.FromGMLFile(TemplateInputCombobox.SelectedItem as string);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load graph:\n{ex.Message}");
            }            
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            if(RandomGraphBuilder == ChosenInput)
            {
                InputGraph = RandomGraphBuilder.Generate();
            }

            if(InputGraph==null)
            {
                MessageBox.Show("Input graph not chosen.");
                return;
            }

            ExecuteGraph();
            TabControl.SelectedIndex = 1;
        }

        private void ExecuteGraph()
        {
            var finder = new NewmanFinder();
            GraphCommunities = finder.Find(InputGraph);

            Histogram.SetValues(finder.QStamps);
            Groupings = finder.Groupings;

            RefreshGraph();            


            //GraphCommunities.ForEach(l => Console.WriteLine(string.Join(", ", l.Select(x => InputGraph.Nodes[x]["id"]))));
        }

        private void GraphViewCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InputGraph != null)
                RefreshGraph();
        }

        void RefreshGraph()
        {
            ControlsBody.Enabled = false;
            var method = GraphViewCombo.SelectedItem as string;
            if(method=="Community")
            {
                GraphViewer.GraphRenderer = new CommunityGraphRenderer<Dictionary<string, string>>("id", GraphCommunities);
            }
            else if(method=="Circular")
            {
                GraphViewer.GraphRenderer = new CircularGraphRenderer<Dictionary<string, string>>("id");
            }
            else if(method=="Force-Based")
            {
                GraphViewer.GraphRenderer = new ForceBasedGraphRenderer<Dictionary<string, string>>("id");
            }

            CommunitiesBox.Text = $"Found {GraphCommunities.Count} communities";
            
            GraphViewer.RenderGraph(InputGraph, GraphCommunities);
            GraphViewer.ResetZoom();

            CommunitiesOutput.Text = "";
            GraphCommunities.ForEach(c => CommunitiesOutput
                .AppendText(string.Join(" ", c.Select(x => InputGraph.Nodes[x]["id"])) + Environment.NewLine));

            CommunitiesOutputCondensed.Text = string.Join(" ",
            GraphCommunities.Select((com, i) => com.Select(n => (i, n)).ToList())
                .SelectMany(x => x).OrderBy(x => x.n).Select(x => x.i).ToArray());

            ControlsBody.Enabled = true;
        }

        private void FindSubComsButton_Click(object sender, EventArgs e)
        {
            GraphCommunities = GraphCommunities
                .Select(s =>
                {                    
                    var subg = new Graph<int>();
                    s.ForEach(i => subg.Nodes.Add(i));

                    foreach (var edge in InputGraph.Edges)
                    {
                        if (s.Contains(edge.Source) && s.Contains(edge.Target))
                        {
                            subg.Edges.Add((s.IndexOf(edge.Source), s.IndexOf(edge.Target)));
                            subg.Edges.Add((s.IndexOf(edge.Target), s.IndexOf(edge.Source)));
                        }
                    }                    

                    return subg;
                })
                .Select(subg =>
                     new NewmanFinder().Find(subg)
                        .Select(l => l.Select(i => subg.Nodes[i]).ToList()).ToList())
                .SelectMany(x => x)
                .ToList();
            

            RefreshGraph();
        }

        private void Histogram_MouseDown(object sender, MouseEventArgs e)
        {            
            GraphCommunities = Groupings[Histogram.SelectedX].Communities;
            RefreshGraph();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ExecuteGraph();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            TabControl.SelectedIndex = 0;
        }

        private void ExternalFileButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ExternalFileButton.Checked)
            {
                ChangeInput(GmlFileSelector);
                GmlFileSelector.Path = GmlFileSelector.Path;                
            }
        }

        private void GmlFileSelector_PathChanged(object sender, EventArgs args)
        {
            if(GmlFileSelector.Path=="" || GmlFileSelector.Path==null)
            {
                InputGraph = null;
                return;
            }
            try
            {
                InputGraph = GraphReader.FromGMLFile(GmlFileSelector.Path);
            }
            catch(Exception e)
            {
                MessageBox.Show($"Could not load graph:\n{e.Message}");
                InputGraph = null;
            }
        }

        private void RandomButton_CheckedChanged(object sender, EventArgs e)
        {
            if (RandomButton.Checked)
            {
                ChangeInput(RandomGraphBuilder);                
            }
        }
    }
}
