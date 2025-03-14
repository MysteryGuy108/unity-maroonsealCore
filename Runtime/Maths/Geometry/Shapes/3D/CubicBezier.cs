using UnityEngine;


namespace MaroonSeal.Maths {
    public struct CubicBezier : ICurveShape
    {
        public Vector3 anchorA;
        public Vector3 controlA;
        public Vector3 controlB;
        public Vector3 anchorB;

        #region Constructors and Operators
        public CubicBezier(Vector3 _anchorA, Vector3 _controlA, Vector3 _controlB, Vector3 _anchorB ) {
            anchorA = _anchorA;
            controlA = _controlA;
            controlB = _controlB;
            anchorB = _anchorB;
        }
        
        public static explicit operator CubicBezier2D(CubicBezier _current) {
            return new(_current.anchorA, _current.controlA, _current.controlB, _current.anchorB);
        }

        public static bool operator == (CubicBezier _a, CubicBezier _b) {
            return _a.anchorA == _b.anchorA && 
                _a.controlA == _b.controlA &&
                _a.controlB == _b.controlB &&
                _a.anchorB == _b.anchorB;
        }

        public static bool operator != (CubicBezier _a, CubicBezier _b) {
            return !(_a.anchorA == _b.anchorA && 
                _a.controlA == _b.controlA &&
                _a.controlB == _b.controlB &&
                _a.anchorB == _b.anchorB);
        }
    
        readonly public override bool Equals(object obj) { return ((CubicBezier)obj == this) && obj != null && obj is CubicBezier; }
        readonly public override int GetHashCode() { return System.HashCode.Combine(anchorA, controlA, controlB, anchorB); }
        #endregion

        #region IOpenShape
        public readonly Vector3 GetStartPoint() { return anchorA; }
        public readonly Vector3 GetEndPoint() { return anchorB; }
        #endregion

        #region ILerpPath
        public readonly Vector3 EvaluatePosition(float _t) {
            float tm = 1.0f -_t;
            float tm2 = tm * tm;
            float tm3 = tm * tm * tm;

            float t2 = _t * _t;
            float t3 = _t * _t * _t;
            return (tm3 * anchorA) + (3 * tm2 * _t * controlA) + (3 * tm * t2 * controlB) + (t3 * anchorB);
        }
        public readonly Vector3 EvaluateTangent(float _t) {
            Vector3 pos0 = anchorA;
            Vector3 pos1 = controlA;
            Vector3 pos2 = controlB;
            Vector3 pos3 = anchorB;

            float tm = 1.0f -_t;
            float tm2 = tm * tm;
            float t2 = _t * _t;
            
            return (3.0f * tm2 * (pos1 - pos0)) + (6.0f * tm * _t * (pos2 - pos1)) + (3.0f * t2 * (pos3 - pos2));
        }
        #endregion

        static public CubicBezier Lerp(CubicBezier _a, CubicBezier _b, float _t) {
            return new CubicBezier(
                Vector3.Lerp(_a.anchorA, _b.anchorA, _t),
                Vector3.Lerp(_a.controlA, _b.controlA, _t),
                Vector3.Lerp(_a.controlB, _b.controlB, _t),
                Vector3.Lerp(_a.anchorA, _b.anchorB, _t));
        }
    }
}