using UnityEngine;

namespace MaroonSeal.Maths.Geometry {
    public interface IWireShape3D {
        public Vector3 LerpAlongPerimeter(float _time);
    }

    public interface ISDFShape3D {
        public float GetSignedDistance(Vector3 _point);
    }
}