using System.Drawing;

namespace GraphVoronoi
{
    sealed class Player
    {
        private readonly Color color;
        
        public Color Color { get { return this.color; } }

        public Player(Color color)
        {
            this.color = color;
        }
    }
}