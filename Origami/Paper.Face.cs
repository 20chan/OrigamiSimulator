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
                // 파라미터로 이쪽으로 접는거야 하고 방향을 알려줘야 한다

                // 모서리들과 폴드된 부분은 짝수이다 (예외는 일단 제외하고 생각하자)
                // 첫번째 접점과 두번쨰 접점을 잇고 사이의 모든 vertices 는 markLine 에 반전시켜
                // 새로운 Face로 만들어 리턴하면 된다

                // todo: mark 만으로는 방향을 알 수 없다. 방향을 알아야 함
                // todo: newFace를 markLine 에 대해 대칭이동 시켜야한다

                var verts = new CyclicArray<Vector>(_vertices.ToArray());
                var intersects = new List<Vector>();
                var indices = new List<int>();

                for (var i = 0; i < verts.Length; i++) {
                    var prev = verts[i - 1];
                    var cur = verts[i];
                    var side = new LineSegment(prev, cur);
                    if (mark.TryGetIntersects(side, out var ints)) {
                        intersects.Add(ints[0]);
                        indices.Add(i);
                    }
                }
                if (intersects.Count < 2) {
                    newFace = null;
                    return false;
                }

                _vertices = new List<Vector>();
                _vertices.Add(intersects[1]);
                _vertices.Add(intersects[0]);

                for (var i = indices[0]; i != indices[1]; i = (i + 1) % verts.Length) {
                    _vertices.Add(verts[i]);
                }

                var newVerts = new List<Vector>();
                newVerts.Add(intersects[0]);
                newVerts.Add(intersects[1]);

                for (var i = indices[1]; i != indices[0]; i = (i + 1) % verts.Length) {
                    newVerts.Add(verts[i]);
                }

                newFace = new Face(newVerts);
                return true;
            }
        }
    }
}
