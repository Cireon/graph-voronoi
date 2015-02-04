using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphVoronoi.Graphs
{
    sealed class Edge
    {
        private const float collisionThickness = 40f;

        private readonly Vertex from, to;

        public Vertex From { get { return this.from; } }
        public Vertex To { get { return this.to; } }

        private readonly SortedSet<IColouredEdgeObject> objectSet = new SortedSet<IColouredEdgeObject>(EdgeObjectComparer.Instance);
        private readonly SortedSet<CriticalPoint> criticalPoints = new SortedSet<CriticalPoint>(EdgeObjectComparer.Instance); 

        public IList<IColouredEdgeObject> ObjectSet { get { return this.objectSet.ToList(); } } 

        public double Length
        {
            get
            {
                var p1 = this.from.Position;
                var p2 = this.to.Position;
                return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
            }
        }

        public Edge(Vertex from, Vertex to)
        {
            this.from = from;
            this.to = to;

            this.objectSet.Add(new VertexEdgeObject(from, 0));
            this.objectSet.Add(new VertexEdgeObject(to, 1));

            this.from.AddAdjacency(to, this);
            this.to.AddAdjacency(from, this);
        }

        public void Draw(GraphicsHelper graphics, PlayerScores scores)
        {
            graphics.DrawEdge(this.from.Position, this.to.Position);

            if (this.from.Owner == null && this.criticalPoints.Count == 0)
                return;

            var diffX = this.To.Position.X - this.From.Position.X;
            var diffY = this.To.Position.Y - this.From.Position.Y;

            if (this.from.Owner == null)
            {
                this.drawCriticalPoints(graphics, diffX, diffY);
                return;
            }

            var w = this.Length;

            switch (graphics.Settings.Mode)
            {
                case DrawMode.Colour:
                    var objects = this.objectSet.ToList();

                    for (int i = 0; i < objects.Count; i++)
                    {
                        var prev = i - 1 >= 0 ? objects[i - 1] : null;
                        var curr = objects[i];
                        var next = i < objects.Count - 1 ? objects[i + 1] : null;

                        var t1 = curr.T;
                        var t2 = curr.T;

                        if (prev != null)
                        {
                            t1 = .5f * (prev.T + curr.T) + .5f * (float) ((curr.Distance - prev.Distance) / w);
                        }
                        if (next != null)
                        {
                            t2 = .5f * (curr.T + next.T) + .5f * (float) ((next.Distance - curr.Distance) / w);
                        }

                        graphics.DrawEdgeSegment(
                            new PointF(this.from.Position.X + t1 * diffX, this.from.Position.Y + t1 * diffY),
                            new PointF(this.from.Position.X + t2 * diffX, this.from.Position.Y + t2 * diffY), curr.Color);
                        scores.AddScore(curr.Player, (t2 - t1) * w);
                    }

                    break;

                case DrawMode.WinArea:
                    var points = this.criticalPoints.ToList();
                    var n = this.From.DominatingAreas.Length - 1;

                    for (int i = 0; i <= points.Count; i++)
                    {
                        var scoresFrom = i == 0 ? this.From.DominatingAreas : points[i - 1].DominatingAreasFromAbove;
                        var scoresTo = i == points.Count ? this.To.DominatingAreas : points[i].DominatingAreasFromBelow;

                        var t1 = i == 0 ? 0 : points[i - 1].T;
                        var t2 = i == points.Count ? 1 : points[i].T;

                        var winFrom = scoresFrom.All(d => d <= scoresFrom[n]);
                        var winTo = scoresTo.All(d => d <= scoresTo[n]);

                        if (winFrom && winTo)
                        {
                            graphics.DrawEdgeSegment(
                                new PointF(this.from.Position.X + t1 * diffX, this.from.Position.Y + t1 * diffY),
                                new PointF(this.from.Position.X + t2 * diffX, this.from.Position.Y + t2 * diffY),
                                Color.LimeGreen);
                        }
                        else
                        {
                            float w1 = t1;
                            float w2 = t2;

                            if (winFrom)
                            {
                                var y = scoresTo[n];

                                for (int j = 0; j < n; j++)
                                {
                                    if (scoresTo[j] <= y)
                                        continue;

                                    var t = (scoresFrom[n] - scoresFrom[j]) /
                                        (scoresTo[j] - scoresFrom[j] - scoresTo[n] + scoresFrom[n]);
                                    w2 = Math.Min(w2, t1 + (float) t * (t2 - t1));
                                }
                            }
                            else if (winTo)
                            {
                                var y = scoresFrom[n];

                                for (int j = 0; j < n; j++)
                                {
                                    if (scoresFrom[j] <= y)
                                        continue;

                                    var t = (scoresFrom[n] - scoresFrom[j]) /
                                        (scoresTo[j] - scoresFrom[j] - scoresTo[n] + scoresFrom[n]);
                                    w1 = Math.Max(w1, t1 + (float) t * (t2 - t1));
                                }
                            }
                            else
                            {
                                var y1 = scoresFrom[n];
                                var y2 = scoresTo[n];

                                for (int j = 0; j < n; j++)
                                {
                                    if (scoresFrom[j] <= y1 && scoresTo[j] <= y2)
                                        continue;
                                    if (scoresFrom[j] > y1 && scoresTo[j] > y2)
                                    {
                                        w1 = w2 = t2;
                                        break;
                                    }

                                    var t = (scoresFrom[n] - scoresFrom[j]) /
                                        (scoresTo[j] - scoresFrom[j] - scoresTo[n] + scoresFrom[n]);

                                    if (scoresFrom[j] <= y1)
                                        w2 = Math.Min(w2, (float) t * (t2 - t1));
                                    else
                                        w1 = Math.Max(w1, (float) t * (t2 - t1));
                                }
                            }

                            if (t1 < w1)
                            {
                                graphics.DrawEdgeSegment(
                                    new PointF(this.from.Position.X + t1 * diffX, this.from.Position.Y + t1 * diffY),
                                    new PointF(this.from.Position.X + w1 * diffX, this.from.Position.Y + w1 * diffY),
                                    Color.OrangeRed);
                            }
                            if (w1 < w2)
                            {
                                graphics.DrawEdgeSegment(
                                    new PointF(this.from.Position.X + w1 * diffX, this.from.Position.Y + w1 * diffY),
                                    new PointF(this.from.Position.X + w2 * diffX, this.from.Position.Y + w2 * diffY),
                                    Color.LimeGreen);
                            }
                            if (w2 < t2)
                            {
                                graphics.DrawEdgeSegment(
                                    new PointF(this.from.Position.X + w2 * diffX, this.from.Position.Y + w2 * diffY),
                                    new PointF(this.from.Position.X + t2 * diffX, this.from.Position.Y + t2 * diffY),
                                    Color.OrangeRed);
                            }
                        }
                    }

                    break;
            }

            this.drawCriticalPoints(graphics, diffX, diffY);
        }

        private void drawCriticalPoints(GraphicsHelper graphics, float diffX, float diffY)
        {
            if (!graphics.Settings.DrawCriticalPoints)
                return;

            foreach (var p in this.criticalPoints)
                graphics.DrawCriticalPoint(new PointF(this.from.Position.X + p.T * diffX, this.from.Position.Y + p.T * diffY));
        }

        public float? OnMouseDown(PointF position)
        {
            return this.ProjectPoint(position, true);
        }

        public CriticalPoint GetCriticalPointAt(PointF position)
        {
            var diffX = this.To.Position.X - this.From.Position.X;
            var diffY = this.To.Position.Y - this.From.Position.Y;

            return this.criticalPoints.FirstOrDefault(p =>
            {
                var pPosX = this.From.Position.X + p.T * diffX;
                var pPosY = this.From.Position.Y + p.T * diffY;

                return (position.X - pPosX) * (position.X - pPosX) + (position.Y - pPosY) * (position.Y - pPosY) <=
                    .5f * collisionThickness;
            });
        }

        public float? ProjectPoint(PointF position, bool failOnNoCollision = false)
        {
            var x1 = this.from.Position.X;
            var y1 = this.from.Position.Y;
            var x2 = this.to.Position.X;
            var y2 = this.to.Position.Y;

            var width = this.Length;
            const double halfHeight = .5 * Edge.collisionThickness;

            var edgeAngle = Math.Atan2(y2 - y1, x2 - x1);

            var diffX = position.X - x1;
            var diffY = position.Y - y1;

            var pA = Math.Atan2(diffY, diffX);
            var pR = Math.Sqrt(diffX * diffX + diffY * diffY);

            var pX = x1 + pR * Math.Cos(pA - edgeAngle);
            var pY = y1 + pR * Math.Sin(pA - edgeAngle);

            if (!failOnNoCollision || (pX >= x1 && pX <= x1 + width && pY >= y1 - .5 * halfHeight && pY <= y1 + .5 * halfHeight))
                return (float)((pX - x1) / width);

            return null;
        }

        public void AddMarker(Marker m)
        {
            this.objectSet.Add(m);
        }

        public void RemoveMarker(Marker m)
        {
            this.objectSet.Remove(m);
        }

        public void AddCriticalPoint(CriticalPoint p)
        {
            this.criticalPoints.Add(p);
        }

        public void ClearCriticalPoints()
        {
            this.criticalPoints.Clear();
        }
    }
}