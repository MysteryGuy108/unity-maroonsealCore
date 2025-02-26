using System;
using UnityEngine;

using MaroonSeal.Maths.Interpolation;
using MaroonSeal.Maths.SDFs;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct Arc2D : IOpenShape2D, ISDFShape2D, IInterpolatable<Arc2D>, ILerpPath2D 
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
        #endregion

        #region IOpenShape2D
        public readonly Vector2 GetStartPoint() { return LerpAlongPath(0.0f); }
        public readonly Vector2 GetEndPoint() { return LerpAlongPath(1.0f); }
        #endregion

        #region ISDFShape2D
        //https://www.shadertoy.com/view/wl23RK
        public readonly float GetSignedDistance(Vector2 _point) {
            throw new NotImplementedException();
        }
        public readonly float GetSignedDistance(Vector3 _point) { return GetSignedDistance((Vector2)_point); }
        #endregion

        #region IInterpolation
        readonly public Arc2D LerpTowards(Arc2D _target, float _time) {
            return new(
                Vector2.Lerp(centre, _target.centre, _time),
                Mathf.Lerp(radius, _target.radius, _time),
                Mathf.Lerp(startDegrees, _target.startDegrees, _time),
                Mathf.Lerp(endDegrees, _target.endDegrees, _time));
        }
        readonly public Vector2 LerpAlongPath(float _time) {
            float lerpTheta = Mathf.Lerp(startDegrees, endDegrees, _time) * Mathf.Deg2Rad;
            return (new Vector2(Mathf.Cos(lerpTheta), Mathf.Sin(lerpTheta)) * radius) + centre;
        }
        readonly Vector3 ILerpPath.LerpAlongPath(float _time) { return LerpAlongPath(_time); }
        #endregion
    }
}