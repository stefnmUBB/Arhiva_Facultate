using AI.Lab2.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab2.UI.Controls
{
    internal abstract class AbstractGraphRenderer<T> : IGraphRenderer<T>
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

        public AbstractGraphRenderer(string displayMember = null)
        {
            DisplayMember = displayMember;
        }
    }
}
