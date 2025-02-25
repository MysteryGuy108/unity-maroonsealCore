using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct Circle2D : IShape2D, ISDFShape2D, IVector2Interpolatable 
    {
        public Vector2 centre;
        [Min(0.0f)] public float radius;

        #region Constructors and Operators
        public Circle2D(Vector2 _centre, float _radius) {
            centre = _centre;
            radius = _radius;
        }

        public static bool operator == (Circle2D _a, Circle2D _b) {
            return _a.centre == _b.centre && _a.radius == _b.radius;
        }

        public static bool operator != (Circle2D _a, Circle2D _b) {
            return !(_a.centre == _b.centre && _a.radius == _b.radius);
        }
    
        readonly public override bool Equals(object obj) {
            if (obj == null) { return false; }
            if (obj is not Circle2D) { return false; }
            return (Circle2D)obj == this;
        }
        readonly public override int GetHashCode() { return System.HashCode.Combine(centre, radius); }
        #endregion


        #region ILerpShape
        readonly public Vector2 InterpolateVector2(float _time) {
            float lerpTheta = Mathf.Lerp(0.0f, Mathf.PI * 2.0f, _time);
            return (new Vector2(Mathf.Cos(lerpTheta), Mathf.Sin(lerpTheta)) * radius) + centre;
        }

        readonly public Vector3 InterpolateVector3(float _time) { return InterpolateVector2(_time); }
        #endregion

        #region ISDFShape
        public readonly float GetSignedDistance(Vector2 _point) {
            return Vector2.Distance(_point, centre) - radius;
        }
        #endregion

        readonly public float GetCircumference() { return 2.0f * Mathf.PI * radius; }
        readonly public bool IsPointInCircle(Vector2 _point) { return Vector2.Distance(_point, centre) < radius; }


    }
}