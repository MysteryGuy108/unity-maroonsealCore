using UnityEngine;

using MaroonSeal.Maths.Interpolation;
using MaroonSeal.Maths.SDFs;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct Line2D : IOpenShape2D, ISDFShape2D
    {
        public Vector2 pointA;
        public Vector2 pointB;

        #region Constructors and Operators
        public Line2D(Vector2 _pointA, Vector2 _pointB) {
            pointA = _pointA;
            pointB = _pointB;
        }
        
        public static bool operator == (Line2D _a, Line2D _b) { return _a.pointA == _b.pointA && _a.pointB == _b.pointB; }

        public static bool operator != (Line2D _a, Line2D _b) { return !(_a.pointA == _b.pointA && _a.pointB == _b.pointB); }
    
        public readonly override bool Equals(object obj) {
            if (obj == null) { return false; }
            if (obj is not Line2D) { return false; }
            return (Line2D)obj == this;
        }

        readonly public override int GetHashCode() { return System.HashCode.Combine(pointA, pointB); }
        #endregion

        #region Line2D
        public readonly float GetLength() { return Vector3.Distance(pointA, pointB); }

        public readonly bool IsPointInBounds(Vector2 _point) {
            float minX, maxX;
            if (pointA.x <= pointB.x) { minX = pointA.x; maxX = pointB.x; }
            else { minX = pointB.x; maxX = pointA.x; }

            float minY, maxY;
            if (pointA.y <= pointB.y) { minY = pointA.y; maxY = pointB.y; }
            else { minY = pointB.y; maxY = pointA.y; }

            return _point.x >= minX && _point.x <= maxX && 
                _point.y >= minY && _point.y <= maxY;
        }
        #endregion

        #region IOpenShape2D
        public readonly Vector2 GetStartPoint() { return pointA; }
        public readonly Vector2 GetEndPoint() { return pointB; }
        #endregion

        #region ISDFShape2D
        public readonly float GetSignedDistance(Vector2 _point) {
            Vector2 pa = _point-pointA;
            Vector2 ba = pointB-pointA;
            float h = Mathf.Clamp( Vector2.Dot(pa,ba)/Vector2.Dot(ba,ba), 0.0f, 1.0f );
            return (pa - ba * h).magnitude;
        }

        public readonly float GetSignedDistance(Vector3 _point) { return GetSignedDistance((Vector2)_point); }
        #endregion

        #region ILerpPath2D
        public readonly Vector2 GetPositionAtTime(float _time) {
            return Vector2.Lerp(pointA, pointB, _time);
        }
        readonly Vector3 ILerpPathVector3.GetPositionAtTime(float _time) { return GetPositionAtTime(_time); }
        #endregion

        static public Line2D Lerp(Line2D _a, Line2D _b, float _time) {
            return new Line2D(Vector2.Lerp(_a.pointA, _b.pointA, _time), Vector2.Lerp(_a.pointB, _b.pointB, _time));
        }

    }
}