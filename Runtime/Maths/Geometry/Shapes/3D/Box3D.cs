using UnityEngine;

namespace MaroonSeal.Maths {
    public struct Box3D : IPolygonShape3D, ISDFShape3D 
    {
        public Vector3 centre;
        public Vector3 size;

        readonly public int VertexCount => 6;

        #region ISDFShape
        readonly public float GetSignedDistance(Vector3 _point) {
            _point -= centre;
            _point = _point.Abs();
            Vector3 q = _point - size;
            return q.Max(0.0f).magnitude + Mathf.Min(Mathf.Max(q.x, Mathf.Max(q.y, q.z)), 0.0f);
        }
        #endregion

        #region IPolygonShape3D
        public Line3D[] GetEdges() {
            throw new System.NotImplementedException();
        }

        public Vector3[] GetVertices() {
            throw new System.NotImplementedException();
        }

        public Vector3 GetPointAtTime(float _time) {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}