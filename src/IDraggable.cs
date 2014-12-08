using System.Drawing;

namespace GraphVoronoi
{
    interface IDraggable : IMouseInputReceiver
    {
        void OnMouseMove(PointF newPosition);
        void OnMouseRelease();
    }
}