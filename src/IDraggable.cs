using System.Drawing;

namespace GraphVoronoi
{
    interface IDraggable
    {
        void OnMouseMove(PointF newPosition);
        void OnMouseRelease();
    }
}