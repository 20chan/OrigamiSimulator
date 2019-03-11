using System;
using M = System.Math;

namespace Origami.Math {
    public static class MathExtension {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T> {
            if (val.CompareTo(min) < 0) {
                return min;
            }
            else if (val.CompareTo(max) > 0) {
                return max;
            }
            else {
                return val;
            }
        }

        public static double Distance(this Vertex v1, Vertex v2) {
            return M.Sqrt(M.Pow(v1.X - v2.X, 2) + M.Pow(v1.Y - v2.Y, 2));
        }
    }
}
