using System.Drawing;

namespace Helpers.CurveDetectorDataSetGenerator.Curves
{
    internal interface IPointDefinedCurve
    {
        PointF StartPoint { get; set; }
        PointF EndPoint { get; set; }
    }
}
