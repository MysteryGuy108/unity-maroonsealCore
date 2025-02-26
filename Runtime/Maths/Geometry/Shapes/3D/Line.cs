using UnityEngine;

using MaroonSeal.Maths.Interpolation;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct Line : ILerpPath
    {
        public Vector3 pointA;
        public Vector3 pointB;

        #region Constructors and Operators
        public Line(Vector3 _pointA, Vector3 _pointB) {
            pointA = _pointA;
            pointB = _pointB;
        }
        
        public static explicit operator Line2D(Line _current) {
            return new(_current.pointA, _current.pointB);
        }

        public static bool operator == (Line _a, Line _b) { return _a.pointA == _b.pointA && _a.pointB == _b.pointB; }

        public static bool operator != (Line _a, Line _b) { return !(_a.pointA == _b.pointA && _a.pointB == _b.pointB); }
    
        readonly public override bool Equals(object obj) {
            return ((Line)obj == this) && obj != null && obj is Line;
        }

        readonly public override int GetHashCode() { return System.HashCode.Combine(pointA, pointB); }
        #endregion

        #region Line3D
        public readonly float GetLength() { return Vector3.Distance(pointA, pointB); }
        #endregion

        #region IOpenShape3D
        public readonly Vector3 GetStart() { return pointA; }
        public readonly Vector3 GetEnd() { return pointB; }
        #endregion

        #region IInterpolation
        public readonly Vector3 LerpAlongPath(float _time) {
            return Vector3.Lerp(pointA, pointB, _time);
        }
        #endregion
    }
}