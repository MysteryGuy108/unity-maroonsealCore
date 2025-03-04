using UnityEngine;

using MaroonSeal.Maths.Interpolation;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct CubicBezier2D : IOpenShape2D 
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

        #region IOpenShape2D
        public readonly Vector2 GetStartPoint() { return anchorA; }
        public readonly Vector2 GetEndPoint() { return anchorB; }
        #endregion

        #region ILerpPath2D
        readonly public Vector2 GetPositionAtTime(float _time) {
            float tm = 1.0f -_time;
            float tm2 = tm * tm;
            float tm3 = tm * tm * tm;

            float t2 = _time * _time;
            float t3 = _time * _time * _time;
            return (tm3 * anchorA) + (3 * tm2 * _time * controlA) + (3 * tm * t2 * controlB) + (t3 * anchorB);
        }
        #endregion

        static public CubicBezier2D Lerp(CubicBezier2D _a, CubicBezier2D _b, float _time) {
            return new(
                Vector2.Lerp(_a.anchorA, _b.anchorA, _time),
                Vector2.Lerp(_a.controlA, _b.controlA, _time),
                Vector2.Lerp(_a.controlB, _b.controlB, _time),
                Vector2.Lerp(_a.anchorA, _b.anchorB, _time));
        }
    }
}