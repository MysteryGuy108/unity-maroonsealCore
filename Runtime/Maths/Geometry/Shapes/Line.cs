using System;
using UnityEngine;

namespace MaroonSeal.Maths.Shapes {
    [System.Serializable]
    public struct Line : IShape3D, IInterpolationShape
    {
        public Vector3 from;
        public Vector3 to;

        readonly public PointTransform Transform => PointTransform.Origin;

        #region Constructors
        public Line(Vector3 _pointA, Vector3 _pointB) {
            from = _pointA;
            to = _pointB;
        }
        #endregion
        
        #region Operators
        public static bool operator == (Line _a, Line _b) { return _a.from == _b.from && _a.to == _b.to; }

        public static bool operator != (Line _a, Line _b) { return !(_a.from == _b.from && _a.to == _b.to); }
    
        readonly public override bool Equals(object obj) {
            if (obj == null || obj is not Line) { return false; }
            return (Line)obj == this;
        }

        readonly public override int GetHashCode() { return System.HashCode.Combine(from, to); }
        #endregion

        #region Casting
        public static implicit operator Line2D(Line _line) => new(_line.from, _line.to);
        #endregion

        #region Line
        public readonly float GetLength() { return Vector3.Distance(from, to); }

        public readonly Vector3 GetVector() => to - from;
        public readonly Vector3 GetDirection() => GetVector().normalized;

        public bool IsPositionInBoundingBox(Vector3 _position) {
            throw new NotImplementedException();
        }
        #endregion

        #region IInterpolationShape
        public readonly Vector3 EvaluatePositionAtTime(float _time) { return Vector3.Lerp(from, to, _time); }
        public readonly Vector3 EvaluateTangentAtTime(float _time) { return GetDirection(); }
        #endregion
    }
}