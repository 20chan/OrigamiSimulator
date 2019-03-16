using Origami.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Origami {
    public partial class Paper {
        private class Face {
            public List<Vertex> Vertices;

            public Face(List<Vertex> vertices) {
                Vertices = vertices;
            }

            public bool TryFold(Line mark, Vertex origVertex, out Face newFace) {
                // TODO: face 전체가 접히는 경우는 어떡하지?
                // 파라미터로 이쪽으로 접는거야 하고 방향을 알려줘야 한다

                // 모서리들과 폴드된 부분은 짝수이다 (예외는 일단 제외하고 생각하자)
                // 첫번째 접점과 두번쨰 접점을 잇고 사이의 모든 vertices 는 markLine 에 반전시켜
                // 새로운 Face로 만들어 리턴하면 된다
                var newVertices = new List<Vertex> {
                    Vertices[0]
                };

                var prev = Vertices[0];
                var lastIntersectIdx = -1;
                var lastIntersection = default(Vertex);
                var isIntersected = false;

                for (int i = 1, counter = 1; counter < Vertices.Count; counter++) {
                    var v = Vertices[i];

                    var curLine = LineSegment.FromTwoPoints(prev, v);

                    if (Geometry.TryGetIntersects(mark, curLine, out var intersect)) {
                        if (!isIntersected) {
                            lastIntersectIdx = i;
                            lastIntersection = intersect;
                            isIntersected = true;
                            newVertices.Add(intersect);
                        }
                        else {
                            isIntersected = false;
                            newVertices.Add(intersect);
                            newVertices.Add(v);
                        }
                    }
                    else {
                        newVertices.Add(v);
                    }
                    prev = v;
                    i++;
                }

                Vertices = newVertices;
                // 일단 newFace는 패스 ㅎ
                newFace = null;
                return false;
            }
        }
    }
}
