using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Maths.Geometry {
    [System.Serializable]
    public struct Sphere3D : ISDFShape3D
    {
        public Vector3 centre;
        [Min(0.0f)]public float radius;

        #region Constructors and Operators
        public Sphere3D(Vector3 _centre, float _radius) {
            centre = _centre;
            radius = _radius;
        }

        public static bool operator == (Sphere3D _a, Sphere3D _b) { return _a.centre == _b.centre && _a.radius == _b.radius; }

        public static bool operator != (Sphere3D _a, Sphere3D _b) { return !(_a.centre == _b.centre && _a.radius == _b.radius); }
    
        readonly public override bool Equals(object obj) {
            if (obj == null) { return false; }
            if (obj is not Sphere3D) { return false; }
            return (Sphere3D)obj == this;
        }
        
        readonly public override int GetHashCode() { return System.HashCode.Combine(centre, radius); }
        #endregion

        #region ISDFShape3D
        readonly public float GetSignedDistance(Vector3 _point) {
            return Vector3.Distance(_point, centre) - radius;
        }
        #endregion

        readonly public float GetArea() { return 4.0f * Mathf.PI * radius * radius; }
        readonly public bool IsPointInSphere(Vector3 _point) { return Vector3.Distance(_point, centre) < radius; }
    }
}