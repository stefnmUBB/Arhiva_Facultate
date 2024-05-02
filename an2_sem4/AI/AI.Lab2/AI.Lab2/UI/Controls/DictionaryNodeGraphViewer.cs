using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab2.UI.Controls
{
    internal class DictionaryNodeGraphViewer : GraphViewer<Dictionary<string, string>>
    {
        public DictionaryNodeGraphViewer() : base()
        {
            GraphRenderer = new CircularGraphRenderer<Dictionary<string, string>>("id");
        }        
    }
}
