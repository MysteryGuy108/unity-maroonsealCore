using System;
using UnityEngine;

using MaroonSeal.Maths.Interpolation;
using MaroonSeal.Maths.SDFs;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct Arc2D : IOpenShape2D, ISDFShape2D 
    {
        public Vector2 centre;
        [Min(0.0f)] public float radius;

        public float startDegrees;
        public float endDegrees;

        #region Constructors and Operators
        public Arc2D(Vector2 _centre, float _radius, float _startDegrees = 0.0f, float _endDegrees = 360.0f) {
            centre = _centre;
            radius = _radius;

            startDegrees = _startDegrees;
            endDegrees = _endDegrees;
        }
        
        public static bool operator == (Arc2D _a, Arc2D _b) {
            return _a.centre == _b.centre && 
                _a.radius == _b.radius && 
                _a.startDegrees == _b.startDegrees &&
                _a.endDegrees == _b.endDegrees;
        }

        public static bool operator != (Arc2D _a, Arc2D _b) {
            return !(_a.centre == _b.centre && 
                _a.radius == _b.radius && 
                _a.startDegrees == _b.startDegrees &&
                _a.endDegrees == _b.endDegrees);
        }
    
        readonly public override bool Equals(object obj) {
            if (obj == null) { return false; }
            if (obj is not Arc2D) { return false; }
            return (Arc2D)obj == this;
        }

        readonly public override int GetHashCode() { return System.HashCode.Combine(centre, radius, startDegrees, endDegrees); }
        #endregion

        #region Arc2D
        readonly public float GetDegreesDelta() { return endDegrees - startDegrees; }

        readonly public float GetStartRadians() { return startDegrees * Mathf.Deg2Rad; }
        readonly public float GetEndRadians() { return endDegrees * Mathf.Deg2Rad; }
        readonly public float GetRadiansDelta() { return GetEndRadians() - GetStartRadians(); } 

        readonly public float GetArcLength() { return GetRadiansDelta() * 2.0f * Mathf.PI * radius; }

        readonly public Vector2 GetPositionAlongArcAtDegrees(float _theta) {
            _theta = Mathf.Clamp(_theta, Mathf.Min(startDegrees, endDegrees), Mathf.Max(startDegrees, endDegrees));
            _theta *= Mathf.Deg2Rad;
            return (new Vector2(Mathf.Cos(_theta), Mathf.Sin(_theta)) * radius) + centre;
        }

        readonly public Vector2 GetPositionAlongArcAtTime(float _time) {
            float lerpTheta = Mathf.Lerp(startDegrees, endDegrees, _time) * Mathf.Deg2Rad;
            return (new Vector2(Mathf.Cos(lerpTheta), Mathf.Sin(lerpTheta)) * radius) + centre;
        }
        #endregion

        #region IOpenShape2D
        public readonly Vector2 GetStartPoint() { return GetPositionAlongArcAtTime(0.0f); }
        public readonly Vector2 GetEndPoint() { return GetPositionAlongArcAtTime(1.0f); }
        #endregion

        #region ISDFShape2D
        //https://www.shadertoy.com/view/wl23RK
        public readonly float GetSignedDistance(Vector2 _point) {
            throw new NotImplementedException();
        }
        public readonly float GetSignedDistance(Vector3 _point) { return GetSignedDistance((Vector2)_point); }
        #endregion

        #region Static
        static public Arc2D Lerp(Arc2D _a, Arc2D _b, float _time) {
            return new(
                Vector2.Lerp(_a.centre, _b.centre, _time),
                Mathf.Lerp(_a.radius, _b.radius, _time),
                Mathf.Lerp(_a.startDegrees, _b.startDegrees, _time),
                Mathf.Lerp(_a.endDegrees, _b.endDegrees, _time));
        }

        #endregion
    }
}