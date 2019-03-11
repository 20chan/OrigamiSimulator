using System;
using System.Collections.Generic;
using System.Text;

namespace Origami.Math {
    public class LineSegment : Line {
        public Vertex P1 { get; }
        public Vertex P2 { get; }

        protected LineSegment(Vertex p1, Vertex p2) : base(grad(p1, p2), yint(p1, p2)) {
            P1 = p1;
            P2 = p2;
        }

        private static float grad(Vertex p1, Vertex p2)
            => (p2.Y - p1.Y) / (p2.X - p1.X);

        private static float yint(Vertex p1, Vertex p2)
            => p1.Y - grad(p1, p2) * p1.X;

        public static LineSegment FromTwoPoints(Vertex p1, Vertex p2)
            => new LineSegment(p1, p2);

        public float Length
            => P1.Distance(P2);
    }
}
