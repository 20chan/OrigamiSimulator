using System;
using System.Collections.Generic;
using cirno;
using cirno.Geometry;

namespace Origami {
    public partial class Paper {
        public IReadOnlyList<Face> Layers => _layers;
        private List<Face> _layers;

        public Paper() {
            _layers = new List<Face>() {
                new Face(new List<Vector>() {
                new Vector(0, 0),
                new Vector(0, 1),
                new Vector(1, 1),
                new Vector(1, 0),
            })};
        }

        public void Fold(Vector from, Vector to) {
            var foldMark = Perpendicular(new Line(from, to));
            FoldFromMark(foldMark);
        }

        /// <summary>Always fold left side of line to right side</summary>
        public void FoldFromMark(Line mark) {
            for (int i = 0, counter = 0; i < _layers.Count; counter++) {
                var cur = _layers[i];
                if (cur.TryFold(mark, out var newFace)) {
                    _layers.Insert(++i, newFace);
                }

                i++;
            }
        }

        private static Line Perpendicular(Line line) {
            var v = line.P1 - line.P2;
            var x = v.X * (float)Math.Cos(Math.PI / 2) - v.Y * (float)Math.Sin(Math.PI / 2);
            var y = v.X * (float)Math.Sin(Math.PI / 2) + v.Y * (float)Math.Cos(Math.PI / 2);

            var p0 = new Vector((line.P1.X + line.P2.X) / 2, (line.P1.Y + line.P2.Y) / 2);
            var p1 = p0 + new Vector(x, y);
            return new Line(p0, p1);
        }
    }
}
