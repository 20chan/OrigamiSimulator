﻿using Origami.Math;

namespace Origami {
    public readonly struct Vertex {
        public readonly float X, Y;

        public Vertex(float x, float y) {
            X = x.Clamp(0, 1);
            Y = y.Clamp(0, 1);
        }
    }
}
