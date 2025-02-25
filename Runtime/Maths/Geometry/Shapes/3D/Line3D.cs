using UnityEngine;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct Line3D : IVector3Interpolatable
    {
        public Vector3 pointA;
        public Vector3 pointB;

        #region Constructors and Operators
        public Line3D(Vector3 _pointA, Vector3 _pointB) {
            pointA = _pointA;
            pointB = _pointB;
        }
        
        public static explicit operator Line2D(Line3D _current) {
            return new(_current.pointA, _current.pointB);
        }

        public static bool operator == (Line3D _a, Line3D _b) { return _a.pointA == _b.pointA && _a.pointB == _b.pointB; }

        public static bool operator != (Line3D _a, Line3D _b) { return !(_a.pointA == _b.pointA && _a.pointB == _b.pointB); }
    
        readonly public override bool Equals(object obj) {
            if (obj == null) { return false; }
            if (obj is not Line3D) { return false; }
            return (Line3D)obj == this;
        }

        readonly public override int GetHashCode() { return System.HashCode.Combine(pointA, pointB); }
        #endregion

        #region ILerpShape3D
        public readonly Vector3 InterpolateVector3(float _time) {
            return Vector3.Lerp(pointA, pointB, _time);
        }
        #endregion

        public readonly float GetLength() { return Vector3.Distance(pointA, pointB); }
    }
}