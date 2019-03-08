using System.Collections.Generic;
using Origami.Math;

namespace Origami {
    public partial class Paper {
        /// <summary>
        /// 종이의 현재 테두리 폴리곤의 버텍스 포인트
        /// </summary>
        private LinkedList<Vertex> _edges;
        private List<Face> _layers;

        public Paper() {
            _edges = new LinkedList<Vertex>(new[] {
                new Vertex(0, 0),
                new Vertex(0, 1),
                new Vertex(1, 1),
                new Vertex(1, 0),
            });
            _layers = new List<Face>() {

            };
        }


        public void Fold(Vertex from, Vertex to) {
            var foldMark = Geometry.GetPerpendicularLine(from, to);


        }
    }
}
