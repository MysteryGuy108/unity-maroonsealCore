using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Maths.Geometry {
    [System.Serializable]
    public struct Triangle2D : IPolygon2D
    {
        public Vector2 pointA;
        public Vector2 pointB;
        public Vector2 pointC;

        #region Constructors and Operators
        public Triangle2D(Vector2 _point1, Vector2 _point2, Vector2 _point3) {
            pointA = _point1;
            pointB = _point2;
            pointC = _point3;
        }
        
        public static bool operator == (Triangle2D _a, Triangle2D _b) {
            return _a.pointA == _b.pointA && _a.pointB == _b.pointB && _a.pointC == _b.pointC;
        }

        public static bool operator != (Triangle2D _a, Triangle2D _b) {
            return !(_a.pointA == _b.pointA && _a.pointB == _b.pointB && _a.pointC == _b.pointC);
        }
    
        readonly public override bool Equals(object obj) {
            if (obj == null) { return false; }
            if (obj is not Triangle2D) { return false; }
            return (Triangle2D)obj == this;
        }
        readonly public override int GetHashCode() { return System.HashCode.Combine(pointA, pointB, pointC); }
        #endregion

        #region IWireShape2D
        public readonly Vector2 LerpAlongPerimeter(float _time) {
            float totalTime = _time * 3.0f;
            if (totalTime <= 1.0f) {
                return Vector2.Lerp(pointA, pointB, totalTime);
            }
            else if (totalTime > 1.0f && totalTime <= 2.0f) {
                return Vector2.Lerp(pointB, pointC, totalTime-1.0f);
            }
            
            return Vector2.Lerp(pointC, pointA, totalTime-2.0f);
        }
        #endregion

        #region IPolygon2D
        readonly public int GetVertexCount() { return 3; }
        readonly public Vector2[] GetVertices() { return new Vector2[3] { pointA, pointB, pointC} ; }
        readonly public Line2D[] GetEdges() { return new Line2D[3] { GetEdgeAB(), GetEdgeBC(), GetEdgeCA() }; }
        #endregion

        readonly public bool ContainsPoint(Vector2 _point) { return _point == pointA || _point == pointB || _point == pointC; }

        readonly public float GetLengthAB() { return Vector2.Distance(pointA, pointB); }
        readonly public float GetLengthBC() { return Vector2.Distance(pointB, pointC); }
        readonly public float GetLengthCA() { return Vector2.Distance(pointC, pointA); }
        readonly public float[] GetSideLengths() { return new float[3] { GetLengthAB(), GetLengthBC(), GetLengthCA() }; }

        readonly public Line2D GetEdgeAB() { return new Line2D(pointA, pointB); }
        readonly public Line2D GetEdgeBC() { return new Line2D(pointB, pointC);  }
        readonly public Line2D GetEdgeCA() { return new Line2D(pointC, pointA);  }


        readonly public Circle2D GetCircumcircle() {
            float d = 2 * (pointA.x * (pointB.y - pointC.y) + pointB.x * (pointC.y - pointA.y) + pointC.x * (pointA.y - pointB.y));

            float ux = ((pointA.x * pointA.x + pointA.y * pointA.y) * (pointB.y - pointC.y) + (pointB.x * pointB.x + pointB.y * pointB.y) * (pointC.y - pointA.y) + (pointC.x * pointC.x + pointC.y * pointC.y) * (pointA.y - pointB.y)) / d;
            float uy = ((pointA.x * pointA.x + pointA.y * pointA.y) * (pointC.x - pointB.x) + (pointB.x * pointB.x + pointB.y * pointB.y) * (pointA.x - pointC.x) + (pointC.x * pointC.x + pointC.y * pointC.y) * (pointB.x - pointA.x)) / d;

            Vector2 circumCentre = new(ux, uy);
            float circumRadius = Vector2.Distance(pointA, circumCentre);

            return new Circle2D(circumCentre, circumRadius);
        }


    }
}

