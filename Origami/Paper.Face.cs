using System.Collections.Generic;
using cirno;
using cirno.Geometry;

namespace Origami {
    public partial class Paper {
        /// <summary>
        /// 종이의 왼쪽 아래가 원점
        /// </summary>
        public class Face {
            public IReadOnlyList<Vector> Vertices => _vertices;
            private List<Vector> _vertices;

            public Face(List<Vector> vertices) {
                _vertices = vertices;
            }

            public bool TryFold(Line mark, out Face newFace) {
                // TODO: face 전체가 접히는 경우는 어떡하지?

                var verts = new CyclicArray<Vector>(_vertices.ToArray());
                var intersects = new List<Vector>();
                var indices = new List<int>();
                Vector? prevIntersect = null;

                for (var i = 0; i < verts.Length; i++) {
                    var prev = verts[i - 1];
                    var cur = verts[i];
                    var side = new LineSegment(prev, cur);
                    if (mark.TryGetIntersects(side, out var ints)) {
                        if (prevIntersect.HasValue && prevIntersect.Value.Equals(ints[0])) {
                            indices[indices.Count - 1] = i;
                            continue;
                        }
                        prevIntersect = ints[0];
                        intersects.Add(ints[0]);
                        indices.Add(i);
                    }
                }
                if (intersects.Count < 2) {
                    newFace = null;
                    return false;
                }

                var v0 = new List<Vector> {
                    intersects[1],
                    intersects[0]
                };

                for (var i = indices[0]; i != indices[1]; i = (i + 1) % verts.Length) {
                    v0.Add(verts[i]);
                }

                var v1 = new List<Vector> {
                    intersects[0],
                    intersects[1]
                };

                for (var i = indices[1]; i != indices[0]; i = (i + 1) % verts.Length) {
                    v1.Add(verts[i]);
                }

                // v0 가 새로운 face의 verticies 인지
                // 이를 통해 무조건 v0를 현재 face의 verticies로 만든다
                if (mark.GetSideOf(v0[2]) < 0) {
                    var temp = v0;
                    v0 = v1;
                    v1 = temp;
                }

                for (int i = 2; i < v1.Count; i++) {
                    v1[i] = mark.GetPerpendicular(v1[i]);
                }

                _vertices = v0;
                newFace = new Face(v1);
                return true;
            }
        }
    }
}
