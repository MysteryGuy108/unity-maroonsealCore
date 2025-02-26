using UnityEngine;

using MaroonSeal.Maths.SDFs;

namespace MaroonSeal.Maths {
    public struct Box : IPolygonShape, ISDFShape 
    {
        public Vector3 centre;
        public Vector3 size;



        #region Constructors and Operators
        public Box(Vector3 _centre, Vector3 _size) {
            centre = _centre;
            size = _size;
        }

        public static bool operator == (Box _a, Box _b) { return _a.centre == _b.centre && _a.size == _b.size; }

        public static bool operator != (Box _a, Box _b) { return !(_a.centre == _b.centre && _a.size == _b.size); }
    
        readonly public override bool Equals(object obj) { return ((Box)obj == this) && obj != null && obj is Box; }
        
        readonly public override int GetHashCode() { return System.HashCode.Combine(centre, size); }
        #endregion

        #region IPolygonShape3D
        readonly public int VertexCount => 6;

        public Vector3[] GetVertices() {
            throw new System.NotImplementedException();
        }

        public Line[] GetEdges() {
            throw new System.NotImplementedException();
        }
        #endregion

        #region ISDFShape
        readonly public float GetSignedDistance(Vector3 _point) {
            _point -= centre;
            _point = _point.Abs();
            Vector3 q = _point - size;
            return q.Max(0.0f).magnitude + Mathf.Min(Mathf.Max(q.x, Mathf.Max(q.y, q.z)), 0.0f);
        }
        #endregion
    }
}