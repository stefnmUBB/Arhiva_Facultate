using AI.Commons.Data;
using System.Collections.Generic;
using System.Drawing;

namespace AI.Commons.UI.Controls
{
    public interface IGraphRenderer<T>
    {
        Bitmap Render(Graph<T> graph, List<List<int>> colors = null);
        string DisplayMember { get; set; }
    }
}
