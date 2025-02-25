using UnityEngine;

namespace MaroonSeal.Maths {
    public interface IShape2D {}
    public interface IPolygonShape2D : IShape2D {
        public int VertexCount { get; }
        public Vector2[] GetVertices();
        public Line2D[] GetEdges();
    }

    public interface IShape3D {}
    public interface IPolygonShape3D : IShape3D {
        public int VertexCount { get; }
        public Vector3[] GetVertices();
        public Line3D[] GetEdges();
    }
}