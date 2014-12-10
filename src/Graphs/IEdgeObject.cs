using System.Collections.Generic;
using System.Drawing;

namespace GraphVoronoi.Graphs
{
    interface IEdgeObject
    {
        Color Color { get; }
        float T { get; }
        double Distance { get; }
    }

    sealed class VertexEdgeObject : IEdgeObject
    {
        private readonly Vertex vertex;
        private readonly float t;

        public Color Color
        {
            get { return this.vertex.Color; }
        }

        public float T
        {
            get { return this.t; }
        }

        public double Distance
        {
            get { return this.vertex.Owner.Distance; }
        }

        public VertexEdgeObject(Vertex v, float t)
        {
            this.vertex = v;
            this.t = t;
        }
    }

    sealed class EdgeObjectComparer : IComparer<IEdgeObject>
    {
        private static EdgeObjectComparer instance;
        public static EdgeObjectComparer Instance
        {
            get { return EdgeObjectComparer.instance ?? (EdgeObjectComparer.instance = new EdgeObjectComparer()); }
        }

        private EdgeObjectComparer() { }

        public int Compare(IEdgeObject x, IEdgeObject y)
        {
            return x.T.CompareTo(y.T);
        }
    }
}