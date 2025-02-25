
using UnityEngine;

namespace MaroonSeal.Maths {
    public struct CubicBezier3D : IVector3Interpolatable
    {
        public Vector3 anchorA;
        public Vector3 controlA;
        public Vector3 controlB;
        public Vector3 anchorB;

        #region Constructors and Operators
        public CubicBezier3D(Vector3 _anchorA, Vector3 _controlA, Vector3 _controlB, Vector3 _anchorB ) {
            anchorA = _anchorA;
            controlA = _controlA;
            controlB = _controlB;
            anchorB = _anchorB;
        }
        
        public static explicit operator CubicBezier2D(CubicBezier3D _current) {
            return new(_current.anchorA, _current.controlA, _current.controlB, _current.anchorB);
        }

        public static bool operator == (CubicBezier3D _a, CubicBezier3D _b) {
            return _a.anchorA == _b.anchorA && 
                _a.controlA == _b.controlA &&
                _a.controlB == _b.controlB &&
                _a.anchorB == _b.anchorB;
        }

        public static bool operator != (CubicBezier3D _a, CubicBezier3D _b) {
            return !(_a.anchorA == _b.anchorA && 
                _a.controlA == _b.controlA &&
                _a.controlB == _b.controlB &&
                _a.anchorB == _b.anchorB);
        }
    
        readonly public override bool Equals(object obj) {
            if (obj == null) { return false; }
            if (obj is not CubicBezier3D) { return false; }
            return (CubicBezier3D)obj == this;
        }

        readonly public override int GetHashCode() { return System.HashCode.Combine(anchorA, controlA, controlB, anchorB); }
        #endregion

        #region ILerpShape
        public readonly Vector3 InterpolateVector3(float _time) {
            float tm = 1.0f -_time;
            float tm2 = tm * tm;
            float tm3 = tm * tm * tm;

            float t2 = _time * _time;
            float t3 = _time * _time * _time;
            return (tm3 * anchorA) + (3 * tm2 * _time * controlA) + (3 * tm * t2 * controlB) + (t3 * anchorB);
        }
        #endregion
    }
}