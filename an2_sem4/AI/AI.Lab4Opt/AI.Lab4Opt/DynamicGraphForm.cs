using AI.Lab4Opt.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab4Opt
{
    public partial class DynamicGraphForm : Form
    {
        DynamicGraph Graph;
        Simulation Simulation;

        public DynamicGraphForm()
        {
            InitializeComponent();
            EdgesView.DataSource = CurrentEdges;

            SolutionsBinding = new BindingList<Solution>(Solutions);
            SolutionsView.DataSource = SolutionsBinding;
        }

        void StartSimulation()
        {
            if (!Running)
            {                
                if(Graph== null)
                {
                    MessageBox.Show("Please load a graph first");
                    return;
                }
                Graph.ResetStream();                                

                Simulation = new Simulation(Graph, (int)StartNodeBox.Value, (int)EndNodeBox.Value);
                Simulation.NextGraph();

                CurrentEdges.Clear();
                Graph.CurrentGraph.Edges.ForEach(e =>
                {
                    if (e.Source < e.Target)
                        CurrentEdges.Add(new EdgeView(e.Source, e.Target, e.Data));
                });
                Solutions.Clear();

                Running = true;
            }

            StartStopButton.Text = "Stop";
            Paused = false;

            Task.Run(Simulate);            
        }

        void StopSimulation()
        {
            Paused = true;
            StartStopButton.Text = "Start";

        }

        void ResetSimulation()
        {
            StopSimulation();
            Running = false;            
        }

        bool Running = false;
        bool Paused = true;

        void Simulate()
        {
            while (!Paused)
            {
                Simulation.RunStep();
                Invoke(new Action(UpdatePath));
                Thread.Sleep(10);
            }
        }

        void UpdatePath()
        {            
            List<int> path = new List<int>();
            int n = Simulation.StartNode;
            path.Add(n);
            double cost = 0;
            while (n != Simulation.FinishNode && path.Count < Graph.NodesCount)
            {
                var neighbor = Simulation.GetBestNeighbor(n, path);
                if (neighbor < 0)
                    break;
                cost += Simulation.Distances[n, neighbor];
                n = neighbor;
                path.Add(n);
            }            

            if (AllNodesCheckbox.Checked && path.Count < Graph.NodesCount) 
                return;

            var sol = new Solution(string.Join(" ", path), cost);

            if (Solutions.Any(x => x.Path == sol.Path))
                return;

            Solutions.Add(sol);
            Solutions.Sort();
            if (Solutions.Count > 20)
                Solutions.RemoveAt(Solutions.Count - 1);
            SolutionsBinding.ResetBindings();


        }

        private void GraphSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            StopSimulation();
            var name = GraphSelector.SelectedItem as string;            
            var graph_input = Properties.Resources.ResourceManager.GetObject(name) as byte[];
            Graph = DynamicGraph.FromBytes(graph_input);
            NodesCountLabel.Text = Graph.NodesCount.ToString();
            StartNodeBox.Value = 0;
            EndNodeBox.Value = Graph.NodesCount - 1;
        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {            
            if (Paused)
            {
                StartSimulation();
            }
            else
            {
                StopSimulation();
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetSimulation();
        }

        private void AlphaBox_ValueChanged(object sender, EventArgs e)
        {
            Simulation.Alpha = (double)AlphaBox.Value;
        }

        private void RhoBox_ValueChanged(object sender, EventArgs e)
        {
            Simulation.EvaporationRate = (double)RhoBox.Value;
        }

        private void BetaBox_ValueChanged(object sender, EventArgs e)
        {
            Simulation.Beta = (double)BetaBox.Value;            
        }

        class EdgeView
        {
            public int Source { get; set; }
            public int Target { get; set; }
            public double Cost { get; set; }

            public EdgeView(int source, int target, double cost)
            {
                Source = source;
                Target = target;
                Cost = cost;
            }
        }

        BindingList<EdgeView> CurrentEdges = new BindingList<EdgeView>();

        class Solution : IComparable
        {
            public string Path { get; set; }
            public double Cost { get; set; }

            public Solution(string path, double cost)
            {
                Path = path;
                Cost = cost;
            }

            public int CompareTo(object obj)
            {
                if (!(obj is Solution))
                    return -1;
                Solution s = (Solution)obj;
                return Math.Sign(Cost - s.Cost);
            }
        }

        List<Solution> Solutions = new List<Solution>();
        BindingList<Solution> SolutionsBinding;

        private void NextGraphStateButton_Click(object sender, EventArgs e)
        {
            if(!Graph.HasNextGraph)
            {
                MessageBox.Show("End of timestamps");
                return;
            }

            Simulation.NextGraph();

            CurrentEdges.Clear();
            Graph.CurrentGraph.Edges.ForEach(ed =>
            {
                if (ed.Source < ed.Target)
                    CurrentEdges.Add(new EdgeView(ed.Source, ed.Target, ed.Data));
            });
            Solutions.Clear();
        }
    }
}
