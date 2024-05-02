using AI.Lab2.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab2.UI.Controls
{
    internal interface IGraphRenderer<T>
    {
        Bitmap Render(Graph<T> graph, List<List<int>> colors = null);
        string DisplayMember { get; set; }
    }
}
