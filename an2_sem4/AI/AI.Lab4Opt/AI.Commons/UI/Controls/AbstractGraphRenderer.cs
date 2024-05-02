using AI.Commons.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace AI.Commons.UI.Controls
{
    public abstract class AbstractGraphRenderer<T> : IGraphRenderer<T>
    {
        public string DisplayMember { get; set; }

        public abstract Bitmap Render(Graph<T> graph, List<List<int>> colors = null);

        public string GetNodeLabel(T node)
        {
            if (DisplayMember == null) 
                return node.ToString();            

            var prop = typeof(T).GetProperty(DisplayMember);
            if (prop != null)
            {                
                return prop.GetValue(node).ToString();
            }            

            prop = typeof(T).GetProperty("Item");            

            if (prop!=null)
            {                
                return prop.GetValue(node, new object[] { DisplayMember }).ToString();
            }            


            return node.ToString();
        }

        private Color[] NodeColors;
        private int[] NodeColorIds;

        public Color GetNodeColor(int id)
        {
            return NodeColors[NodeColorIds[id]];
        }

        protected void LoadNodeColors(List<List<int>> colors, int nodesCount)
        {
            NodeColorIds = new int[nodesCount];         

            if (colors != null)
            {
                for (int c = 0; c < colors.Count; c++)
                {
                    colors[c].ForEach(i => NodeColorIds[i] = c);
                }
                NodeColors = new Color[colors.Count];

                for (int i = 0; i < NodeColors.Length; i++)
                {
                    NodeColors[i] = (Color)typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.Public)
                        .ToList()
                        .Where(p => p.PropertyType == typeof(Color))
                        .ElementAt(1 + i)
                        .GetValue(null);
                }
            }
            else
            {
                NodeColors = new Color[1];
                NodeColors[0] = Color.White;
            }
        }

        public AbstractGraphRenderer(string displayMember = null)
        {
            DisplayMember = displayMember;
        }
    }
}
