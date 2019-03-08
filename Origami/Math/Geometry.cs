using System;
using System.Collections.Generic;
using System.Text;

namespace Origami.Math {
    public static class Geometry {
        public static Line GetPerpendicularLine(Vertex p1, Vertex p2) {
            var mid = new Vertex((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            var grad = -1f / Line.FromTwoPoint(p1, p2).Grad;

            return Line.FromPointGrad(mid, grad);
        }
    }
}
