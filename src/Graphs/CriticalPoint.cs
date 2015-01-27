namespace GraphVoronoi.Graphs
{
    sealed class CriticalPoint : IEdgeObject
    {
        public float T { get; private set; }

        public CriticalPoint(Edge e, float t)
        {
            this.T = t;
        }
    }
}