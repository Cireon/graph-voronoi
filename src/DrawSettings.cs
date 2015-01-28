namespace GraphVoronoi
{
    enum DrawMode
    {
        Colour,
        WinArea
    }

    sealed class DrawSettings
    {
        public DrawMode Mode;
        public bool DrawCriticalPoints;
    }
}