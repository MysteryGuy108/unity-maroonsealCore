using System;
using UnityEngine;

namespace MaroonSeal.Maths.Shapes {
    public interface IPolygon {
        public int VertexCount { get; }
        public Vector3[] GetVertices();
        public Line[] GetEdges();
    }
}