using System.Collections.Generic;
using Origami.Math;

namespace Origami {
    public partial class Paper {
        private List<Face> _layers;

        public Paper() {
            _layers = new List<Face>() {
                new Face(new LinkedList<Vertex>(new[] {
                new Vertex(0, 0),
                new Vertex(0, 1),
                new Vertex(1, 1),
                new Vertex(1, 0),
            }))};
        }


        public void Fold(Vertex from, Vertex to) {
            var foldMark = Geometry.GetPerpendicularLine(from, to);
            for (int i = 0, counter = 0; counter < _layers.Count; counter++) {
                var cur = _layers[i];
                if (cur.TryFold(foldMark, from, out var newFace)) {
                    _layers.Insert(++i, newFace);
                }

                i++;
            }
        }
    }
}
