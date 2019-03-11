using System;
using System.Collections.Generic;
using System.Text;

namespace Origami.Math {
    public static class Geometry {
        public static Line GetPerpendicularLine(Vertex p1, Vertex p2) {
            var mid = new Vertex((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            var grad = -1f / Line.FromTwoPoints(p1, p2).Grad;

            return Line.FromPointGrad(mid, grad);
        }

        public static bool TryGetIntersects(Line line, LineSegment seg, out Vertex intersect) {
            if (line.Grad == seg.Grad && line.Yint == seg.Yint) {
                throw new NotFiniteNumberException("일치");
            }

            intersect = default;
            if (line.Grad == seg.Grad) {
                return false;
            }

            var intX = (seg.Yint - line.Yint) / (line.Grad - seg.Grad);
            intersect = new Vertex(intX, intX * line.Grad + line.Yint);

            var len = seg.Length;
            return intersect.Distance(seg.P1) < len
                && intersect.Distance(seg.P2) < len;
        }
    }
}
