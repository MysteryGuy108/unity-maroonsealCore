using UnityEngine;

namespace MaroonSeal.Maths.Geometry {
    public interface IWireShape2D {
        public Vector2 LerpAlongPerimeter(float _time);
    }

    public interface IPolygon2D : IWireShape2D{
        public int GetVertexCount();
        public Vector2[] GetVertices();
        public Line2D[] GetEdges();
    }

    public interface ISDFShape2D {
        public float GetSignedDistance(Vector2 _point);
    }
}