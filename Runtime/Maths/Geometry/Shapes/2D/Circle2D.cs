using UnityEngine;

using MaroonSeal.Maths.Interpolation;
using MaroonSeal.Maths.SDFs;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct Circle2D : IShape2D, ISDFShape2D 
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

        #region Circle2D
        readonly public float GetCircumference() { return 2.0f * Mathf.PI * radius; }
        readonly public bool IsPointInCircle(Vector2 _point) { return Vector2.Distance(_point, centre) < radius; }
        #endregion

        #region ISDFShape
        public readonly float GetSignedDistance(Vector2 _point) {
            return Vector2.Distance(_point, centre) - radius;
        }
        public readonly float GetSignedDistance(Vector3 _point) { return GetSignedDistance((Vector2)_point); }
        #endregion

        readonly public Vector2 GetPositionAtRadians(float _radians) {
            _radians = Mathf.Repeat(_radians, Mathf.PI * 2.0f);
            return (new Vector2(Mathf.Cos(_radians), Mathf.Sin(_radians)) * radius) + centre;
        }

        readonly public Vector2 GetPositionAtDegrees(float _degrees) {
            float radTheta = Mathf.Repeat(_degrees, 360.0f) * Mathf.Deg2Rad;
            return GetPositionAtRadians(radTheta);
        }

        readonly public Vector2 GetPositionAtTime(float _time) {
            float lerpTheta = Mathf.Lerp(0.0f, Mathf.PI * 2.0f, _time);
            return GetPositionAtRadians(lerpTheta);
        }

        static public Circle2D Lerp(Circle2D _a, Circle2D _b, float _time) {
            return new Circle2D(
                Vector2.Lerp(_a.centre, _b.centre, _time),
                Mathf.Lerp(_a.radius, _b.radius, _time));
        }
    }
}