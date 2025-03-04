using System;
using UnityEngine;

using MaroonSeal.Maths.Interpolation;
using MaroonSeal.Maths.SDFs;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct Triangle2D : IPolygonShape2D, ISDFShape2D
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

        #region Triangle2D
        readonly public bool ContainsPoint(Vector2 _point) { return _point == pointA || _point == pointB || _point == pointC; }

        readonly public float GetLengthAB() { return Vector2.Distance(pointA, pointB); }
        readonly public float GetLengthBC() { return Vector2.Distance(pointB, pointC); }
        readonly public float GetLengthCA() { return Vector2.Distance(pointC, pointA); }

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
        #endregion
        
        #region IPolygon2D
        readonly public int VertexCount { get { return 3; } }
        readonly public Vector2[] GetVertices() { return new Vector2[3] { pointA, pointB, pointC} ; }
        readonly public Line2D[] GetEdges() { return new Line2D[3] { GetEdgeAB(), GetEdgeBC(), GetEdgeCA() }; }
        #endregion

        #region ISDFShape2D
        //https://www.shadertoy.com/view/XsXSz4
        readonly public float GetSignedDistance(Vector2 _position) {
            throw new NotImplementedException();
            /*
            vec2 e0 = p1-p0, e1 = p2-p1, e2 = p0-p2;
            vec2 v0 = p -p0, v1 = p -p1, v2 = p -p2;
            vec2 pq0 = v0 - e0*clamp( dot(v0,e0)/dot(e0,e0), 0.0, 1.0 );
            vec2 pq1 = v1 - e1*clamp( dot(v1,e1)/dot(e1,e1), 0.0, 1.0 );
            vec2 pq2 = v2 - e2*clamp( dot(v2,e2)/dot(e2,e2), 0.0, 1.0 );
            float s = sign( e0.x*e2.y - e0.y*e2.x );
            vec2 d = min(min(vec2(dot(pq0,pq0), s*(v0.x*e0.y-v0.y*e0.x)),
            vec2(dot(pq1,pq1), s*(v1.x*e1.y-v1.y*e1.x))),
            vec2(dot(pq2,pq2), s*(v2.x*e2.y-v2.y*e2.x)));
            return -sqrt(d.x)*sign(d.y);
            */
        }
        public readonly float GetSignedDistance(Vector3 _point) { return GetSignedDistance((Vector2)_point); }
        #endregion


        #region ILerpPath2D
        public readonly Vector2 GetPositionAtTime(float _time) {
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

        static public Triangle2D Lerp(Triangle2D _a, Triangle2D _b,float _time) {
            return new(
                Vector2.Lerp(_a.pointA, _b.pointA, _time),
                Vector2.Lerp(_a.pointB, _b.pointB, _time),
                Vector2.Lerp(_a.pointC, _b.pointC, _time));
        }
    }
}

