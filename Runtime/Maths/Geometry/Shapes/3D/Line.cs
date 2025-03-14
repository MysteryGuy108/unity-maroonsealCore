using UnityEngine;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct Line
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

        #region Line
        public readonly float GetLength() { return Vector3.Distance(pointA, pointB); }

        public readonly Vector3 GetDirection() {
            return (pointA - pointB).normalized;
        }

        public readonly Vector3 EvaluatePosition(float _time) {
            if (pointA == pointB) { return pointA; }
            return Vector3.Lerp(pointA, pointB, _time);
        }

        public readonly Vector3 EvaluatePositionAtDistance(float _distance) {
            if (pointA == pointB) { return pointA; }
            return Vector3.Lerp(pointA, pointB, _distance / GetLength());
        }
        #endregion

        #region IOpenShape
        public readonly Vector3 GetStart() { return pointA; }
        public readonly Vector3 GetEnd() { return pointB; }
        #endregion

    }
}