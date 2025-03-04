using UnityEngine;

using MaroonSeal.Maths.Interpolation;
using MaroonSeal.Maths.SDFs;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct Sphere : ISDFShape
    {
        public Vector3 centre;
        [Min(0.0f)]public float radius;

        #region Constructors and Operators
        public Sphere(Vector3 _centre, float _radius) {
            centre = _centre;
            radius = _radius;
        }

        public static bool operator == (Sphere _a, Sphere _b) { return _a.centre == _b.centre && _a.radius == _b.radius; }

        public static bool operator != (Sphere _a, Sphere _b) { return !(_a.centre == _b.centre && _a.radius == _b.radius); }
    
        readonly public override bool Equals(object obj) {
            return ((Sphere)obj == this) && obj != null && obj is Sphere;
        }
        
        readonly public override int GetHashCode() { return System.HashCode.Combine(centre, radius); }
        #endregion

        #region Sphere3D
        readonly public float GetArea() { return 4.0f * Mathf.PI * radius * radius; }
        readonly public bool IsPointInSphere(Vector3 _point) { return Vector3.Distance(_point, centre) < radius; }
        #endregion

        #region ISDFShape3D
        readonly public float GetSignedDistance(Vector3 _point) {
            return Vector3.Distance(_point, centre) - radius;
        }
        #endregion

        #region IInterpolation
        static public Sphere Lerp(Sphere _a, Sphere _b, float _time) {
            return new(Vector3.Lerp(_a.centre, _b.centre, _time), Mathf.Lerp(_a.radius, _b.radius, _time));
        }
        #endregion
    }
}