using UnityEngine;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct CubicBezier2D : IShape2D, IVector2Interpolatable 
    {
        public Vector2 anchorA;
        public Vector2 controlA;
        public Vector2 controlB;
        public Vector2 anchorB;

        #region Constructors and Operators
        public CubicBezier2D(Vector2 _anchorA, Vector2 _controlA, Vector2 _controlB, Vector2 _anchorB ) {
            anchorA = _anchorA;
            controlA = _controlA;
            controlB = _controlB;
            anchorB = _anchorB;
        }
        
        public static bool operator == (CubicBezier2D _a, CubicBezier2D _b) {
            return _a.anchorA == _b.anchorA && 
                _a.controlA == _b.controlA &&
                _a.controlB == _b.controlB &&
                _a.anchorB == _b.anchorB;
        }

        public static bool operator != (CubicBezier2D _a, CubicBezier2D _b) {
            return !(_a.anchorA == _b.anchorA && 
                _a.controlA == _b.controlA &&
                _a.controlB == _b.controlB &&
                _a.anchorB == _b.anchorB);
        }
    
        readonly public override bool Equals(object obj) {
            if (obj == null) { return false; }
            if (obj is not CubicBezier2D) { return false; }
            return (CubicBezier2D)obj == this;
        }

        readonly public override int GetHashCode() { return System.HashCode.Combine(anchorA, controlA, controlB, anchorB); }
        #endregion

        #region ILerpShape
        public readonly Vector2 InterpolateVector2(float _time) {
            float tm = 1.0f -_time;
            float tm2 = tm * tm;
            float tm3 = tm * tm * tm;

            float t2 = _time * _time;
            float t3 = _time * _time * _time;
            return (tm3 * anchorA) + (3 * tm2 * _time * controlA) + (3 * tm * t2 * controlB) + (t3 * anchorB);
        }
        readonly public Vector3 InterpolateVector3(float _time) { return InterpolateVector2(_time); }
        #endregion
    }
}