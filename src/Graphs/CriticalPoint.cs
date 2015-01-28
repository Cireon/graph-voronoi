namespace GraphVoronoi.Graphs
{
    sealed class CriticalPoint : IEdgeObject, IHighlightable
    {
        private readonly IHighlightable[] relatedObjects;
        public float T { get; private set; }

        public CriticalPoint(Edge e, float t, params IHighlightable[] relatedObjects)
        {
            this.relatedObjects = relatedObjects;
            this.T = t;
        }

        public void Highlight()
        {
            foreach (var obj in this.relatedObjects)
                obj.Highlight();
        }

        public void UnHighlight()
        {
            foreach (var obj in this.relatedObjects)
                obj.UnHighlight();
        }
    }
}