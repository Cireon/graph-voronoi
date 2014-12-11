using System.Drawing;

namespace GraphVoronoi.Graphs
{
    sealed class VertexOwner
    {
        private readonly Marker marker;
        private readonly double distance;

        public Player Player { get { return this.marker.Player; } }
        public Color Color { get { return this.marker.Color; } }
        public double Distance { get { return this.distance; } }

        public VertexOwner(Marker marker, double distance)
        {
            this.marker = marker;
            this.distance = distance;
        }
    }
}