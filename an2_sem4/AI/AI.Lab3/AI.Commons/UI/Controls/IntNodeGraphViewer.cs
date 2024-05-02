namespace AI.Commons.UI.Controls
{
    public class IntNodeGraphViewer : GraphViewer<int>
    {
        public IntNodeGraphViewer() : base()
        {
            GraphRenderer = new CircularGraphRenderer<int>();
        }
    }
}
