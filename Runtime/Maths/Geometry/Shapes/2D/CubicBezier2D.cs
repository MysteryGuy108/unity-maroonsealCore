using UnityEngine;

using MaroonSeal.Maths.Interpolation;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct CubicBezier2D : IOpenShape2D, IInterpolatable<CubicBezier2D>, ILerpPath2D 
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

        #region IInterpolation
        readonly public CubicBezier2D LerpTowards(CubicBezier2D _target, float _time) {
            return new(
                Vector2.Lerp(anchorA, _target.anchorA, _time),
                Vector2.Lerp(anchorA, _target.controlA, _time),
                Vector2.Lerp(anchorA, _target.controlB, _time),
                Vector2.Lerp(anchorA, _target.anchorB, _time));
        }
        readonly public Vector2 LerpAlongPath(float _time) {
            float tm = 1.0f -_time;
            float tm2 = tm * tm;
            float tm3 = tm * tm * tm;

            float t2 = _time * _time;
            float t3 = _time * _time * _time;
            return (tm3 * anchorA) + (3 * tm2 * _time * controlA) + (3 * tm * t2 * controlB) + (t3 * anchorB);
        }

        readonly Vector3 ILerpPath.LerpAlongPath(float _t) {
            return LerpAlongPath(_t);
        }
        #endregion
    }
}