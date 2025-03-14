using UnityEngine;

using MaroonSeal.Maths.Interpolation;

namespace MaroonSeal.Maths {

    public interface IShape {}

    public interface IPolygonShape : IShape {
        public int VertexCount { get; }
        public Vector3[] GetVertices();
        public Line[] GetEdges();
    }

    public interface ICurveShape : IShape {
        public Vector3 EvaluatePosition(float _t);
        public Vector3 EvaluateTangent(float _t);
    }

    #region 2D
    public interface IShape2D : IShape {}

    public interface IPolygonShape2D : IShape2D {
        public int VertexCount { get; }
        public Vector2[] GetVertices();
        public Line2D[] GetEdges();
    }

    public interface IOpenShape2D : IShape2D {
        public Vector2 GetStartPoint();
        public Vector2 GetEndPoint();
    }
    #endregion
}