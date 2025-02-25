using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct Arc2D : IShape2D, ISDFShape2D, IVector2Interpolatable 
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

        #region IInterpolation
        readonly public Vector2 InterpolateVector2(float _time) {
            float lerpTheta = Mathf.Lerp(startDegrees, endDegrees, _time) * Mathf.Deg2Rad;
            return (new Vector2(Mathf.Cos(lerpTheta), Mathf.Sin(lerpTheta)) * radius) + centre;
        }
        readonly public Vector3 InterpolateVector3(float _time) { return InterpolateVector2(_time); }
        #endregion

        #region ISDFShape2D
        //https://www.shadertoy.com/view/wl23RK
        public readonly float GetSignedDistance(Vector2 _point) {
            throw new NotImplementedException();
        }
        #endregion

        readonly public float GetDegreesDelta() { return endDegrees - startDegrees; }

        readonly public float GetArcLength() {
            return GetDegreesDelta() * Mathf.Deg2Rad * 2.0f * Mathf.PI * radius;
        }


    }
}