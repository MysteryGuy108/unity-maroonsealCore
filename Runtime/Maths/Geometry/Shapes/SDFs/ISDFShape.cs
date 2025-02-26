using UnityEngine;

namespace MaroonSeal.Maths.SDFs {

    #region 3D
    public interface ISDFShape {
        public float GetSignedDistance(Vector3 _point);
    }

    public static class SDFShapeExtensions {
        public static float GetSignedDistance(this ISDFShape2D _shape, Transform _shapeTransform, Vector3 _globalPoint) {
            if (_shapeTransform) { _globalPoint = _shapeTransform.InverseTransformPoint(_globalPoint); }
            return  _shape.GetSignedDistance(_globalPoint);
        }
    }
    #endregion

    #region 2D
    public interface ISDFShape2D : ISDFShape {
        public float GetSignedDistance(Vector2 _point);
    }

    public static class SDFShape2DExtensions {
        public static float GetSignedDistance(this ISDFShape2D _shape, Transform _shapeTransform, Vector3 _globalPoint) {
            if (_shapeTransform) { _globalPoint = _shapeTransform.InverseTransformPoint(_globalPoint); }
            return  _shape.GetSignedDistance(_globalPoint);
        }
    }
    #endregion


}