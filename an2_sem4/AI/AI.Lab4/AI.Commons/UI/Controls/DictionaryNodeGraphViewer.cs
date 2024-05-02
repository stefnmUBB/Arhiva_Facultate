using System.Collections.Generic;

namespace AI.Commons.UI.Controls
{
    public class DictionaryNodeGraphViewer : GraphViewer<Dictionary<string, string>>
    {
        public DictionaryNodeGraphViewer() : base()
        {
            GraphRenderer = new CircularGraphRenderer<Dictionary<string, string>>("id");
        }        
    }
}
