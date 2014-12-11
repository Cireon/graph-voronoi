using System.Collections.Generic;
using System.Linq;

namespace GraphVoronoi
{
    sealed class PlayerScores
    {
        private readonly Dictionary<Player, double> scores;
        private double total;

        public PlayerScores(IEnumerable<Player> players)
        {
            this.scores = players.ToDictionary(p => p, p => 0d);
        }

        public void AddScore(Player p, double amount)
        {
            this.scores[p] += amount;
            this.total += amount;
        }

        public Dictionary<Player, double> GetScoresNormalised()
        {
            return total <= 0 ? null : this.scores.ToDictionary(pair => pair.Key, pair => pair.Value / total);
        }
    }
}