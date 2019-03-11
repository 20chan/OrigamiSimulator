using Origami.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Origami {
    public partial class Paper {
        private class Face {
            public LinkedList<Vertex> Vertices;

            public Face(LinkedList<Vertex> vertices) {
                Vertices = vertices;
            }

            public bool TryFold(Line mark, Vertex origVertex, out Face newFace) {
                Vertex prev = default;
                bool isFirst = true;

                foreach (var v in Vertices) {
                    if (isFirst) {
                        isFirst = false;
                        prev = v;
                        continue;
                    }

                    var curLine = LineSegment.FromTwoPoints(prev, v);
                    if (Geometry.TryGetIntersects(mark, curLine, out var intersect)) {
                        
                    }

                    prev = v;
                }
            }
        }
    }
}
