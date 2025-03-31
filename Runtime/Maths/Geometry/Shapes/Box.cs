using UnityEngine;

using MaroonSeal.Maths.SDFs;

namespace MaroonSeal.Maths.Shapes {
    public struct Box : IShape3D, ISDFShape 
    {
        public PointTransform transform;
        readonly public PointTransform Transform => transform;

        public Vector3 size;

        #region Constructors and
        public Box(PointTransform _transform, Vector3 _size) {
            transform = _transform;
            size = _size;
        }

        public Box(Vector3 _centre, Vector3 _size) {
            transform = new(_centre);
            size = _size;
        }
        #endregion

        #region Operators
        public static bool operator == (Box _a, Box _b) { return _a.transform == _b.transform && _a.size == _b.size; }

        public static bool operator != (Box _a, Box _b) { return !(_a.transform == _b.transform && _a.size == _b.size); }
    
        readonly public override bool Equals(object obj) { return ((Box)obj == this) && obj != null && obj is Box; }
        
        readonly public override int GetHashCode() { return System.HashCode.Combine(transform, size); }
        #endregion

        #region ISDFShape
        readonly public float GetSignedDistance(Vector3 _point) {
            _point = transform.InverseTransformPosition(_point);
            _point = _point.Abs();
            Vector3 q = _point - size;
            return q.Max(0.0f).magnitude + Mathf.Min(Mathf.Max(q.x, Mathf.Max(q.y, q.z)), 0.0f);
        }
        #endregion
    }
}