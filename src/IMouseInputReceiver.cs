using System.Drawing;

namespace GraphVoronoi
{
    interface IMouseInputReceiver
    {
        bool OnMouseDown(PointF position);
    }
}