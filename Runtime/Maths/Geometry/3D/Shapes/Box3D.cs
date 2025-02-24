using UnityEngine;

namespace MaroonSeal.Maths.Geometry {
    public struct Box3D : ISDFShape3D
    {
        public Vector3 centre;
        public Vector3 size;

        public float GetSignedDistance(Vector3 _point)
        {
            throw new System.NotImplementedException();
        }
    }
}