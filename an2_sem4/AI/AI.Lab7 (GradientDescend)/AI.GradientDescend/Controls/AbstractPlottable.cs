namespace AI.GradientDescend.Controls
{
    internal abstract class AbstractPlottable : IPlottable
    {
        public void Clear(IPlotter plotter)
        {
            plotter.Clear(this);
        }

        public abstract void Plot(IPlotter plotter);        
    }
}
