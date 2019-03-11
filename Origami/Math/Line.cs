    using System;
using System.Collections.Generic;
using System.Text;

namespace Origami.Math {
    public class Line {
        public float Grad { get; }
        public float Yint { get; }

        protected Line(float grad, float yint) {
            Grad = grad;
            Yint = yint;
        }

        public static Line FromTwoPoints(Vertex p1, Vertex p2) {
            var grad = (p2.Y - p1.Y) / (p2.X - p1.X);
            var yint = p1.Y - grad * p1.X;

            return new Line(grad, yint);
        }

        public static Line FromPointGrad(Vertex p, float grad) {
            var yint = p.Y - grad * p.X;

            return new Line(grad, yint);
        }
    }
}
