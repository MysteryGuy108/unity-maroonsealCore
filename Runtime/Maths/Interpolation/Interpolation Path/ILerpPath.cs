using System.Collections;
using UnityEngine;

namespace MaroonSeal.Maths.Interpolation {
    public interface ILerpPath {
        public Vector3 LerpAlongPath(float _t);
        static public Vector3 LerpBetween(ILerpPath _shapeA, ILerpPath _shapeB, float _time) {
            return Vector3.Lerp(_shapeA.LerpAlongPath(_time), _shapeB.LerpAlongPath(_time), _time);
        }
    }

    public static class ILerpPathExtensions {
        public static Vector3 LerpAlongPath(this ILerpPath _interpolatable, Transform _transform, float _time) {
            Vector3 localPoint = _interpolatable.LerpAlongPath(_time);
            if (!_transform) { return localPoint;}
            return _transform.TransformPoint(localPoint);
        }

        public static Vector3 LerpAlongPathTowards(this ILerpPath _interpolatable, float _startTime, float _targetTime, float _delta, Transform _transform = null) {
            float newTime = Interpolation.MoveTimeTowards(_startTime, _targetTime, _delta);
            return _interpolatable.LerpAlongPath(_transform, newTime);
        }

        public static Vector3 LerpAlongPathTowardsWrapped(this ILerpPath _interpolatable, float _startTime, float _targetTime, float _delta, Transform _transform = null) {
            float newTime = Interpolation.MoveTimeTowardsWrapped(_startTime * 360.0f, _targetTime * 360.0f, _delta * 360.0f) / 360.0f;
            return _interpolatable.LerpAlongPath(_transform, newTime);
        }
    }

    #region 2D
    public interface ILerpPath2D : ILerpPath {
        new public Vector2 LerpAlongPath(float _t);

        static public Vector2 LerpBetween(ILerpPath2D _shapeA, ILerpPath2D _shapeB, float _time) {
            return Vector2.Lerp(_shapeA.LerpAlongPath(_time), _shapeB.LerpAlongPath(_time), _time);
        }
    }

    public static class ILerpPath2DExtensions {
        public static Vector2 LerpAlongPath(this ILerpPath2D _interpolatable, Transform _transform, float _time) {
            return ((ILerpPath)_interpolatable).LerpAlongPath(_transform, _time);
        }

        public static Vector2 LerpAlongPathTowards(this ILerpPath2D _interpolatable, float _startTime, float _targetTime, float _delta, Transform _transform = null) {
            return ((ILerpPath)_interpolatable).LerpAlongPathTowards(_startTime, _targetTime, _delta, _transform);
        }

        public static Vector2 LerpAlongPathTowardsWrapped(this ILerpPath2D _interpolatable, float _startTime, float _targetTime, float _delta, Transform _transform = null) {
            return ((ILerpPath)_interpolatable).LerpAlongPathTowardsWrapped(_startTime, _targetTime, _delta, _transform);
        }
    }
    #endregion
}