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
    public partial class MainForm : Form
    {
        DynamicGraph Graph;
        Simulation Simulation;

        public MainForm()
        {
            InitializeComponent();
        }

        void StartSimulation()
        {
            if(!Running)
            {
                Graph = new DynamicGraph(GraphEditor.GetGraph().CastEdgeData(c => (double)c));
                Simulation = new Simulation(Graph, 0, Graph.NodesCount - 1);
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
            GraphEditor.Viewer.HighlightedPath.Clear();
            GraphEditor.Viewer.Invalidate();
        }

        bool Running = false;
        bool Paused = true;

        void Simulate()
        {            
            while(!Paused)
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
            while (n != Simulation.FinishNode && path.Count < Graph.NodesCount) 
            {
                var neighbor = Simulation.GetBestNeighbor(n, path);
                if (neighbor < 0)
                    break;
                n = neighbor;
                path.Add(n);
            }

            if (AllNodesCheckbox.Checked && path.Count < Graph.NodesCount) 
                return;
            GraphEditor.HighlightPath(path.ToArray());            
        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if(Paused)
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

        private void DynamicGraphsButton_Click(object sender, EventArgs e)
        {
            new DynamicGraphForm().ShowDialog();
        }        
    }
}
