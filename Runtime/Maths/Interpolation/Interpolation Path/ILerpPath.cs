using System.Collections;
using UnityEngine;

namespace MaroonSeal.Maths.Interpolation {
    public interface ILerpPathVector3 {
        public Vector3 GetPositionAtTime(float _t);
        static public Vector3 LerpBetween(ILerpPathVector3 _pathA, ILerpPathVector3 _pathB, float _time) {
            return Vector3.Lerp(_pathA.GetPositionAtTime(_time), _pathB.GetPositionAtTime(_time), _time);
        }
    }

    public static class ILerpPathVector3Extensions {
        public static Vector3 GetPositionAtTime(this ILerpPathVector3 _interpolatable, Transform _transform, float _time) {
            Vector3 localPoint = _interpolatable.GetPositionAtTime(_time);
            if (!_transform) { return localPoint;}
            return _transform.TransformPoint(localPoint);
        }

        public static Vector3 LerpAlongPathTowards(this ILerpPathVector3 _interpolatable, float _startTime, float _targetTime, float _delta, Transform _transform = null) {
            float newTime = Interpolation.MoveTimeTowards(_startTime, _targetTime, _delta);
            return _interpolatable.GetPositionAtTime(_transform, newTime);
        }

        public static Vector3 LerpAlongPathTowardsWrapped(this ILerpPathVector3 _interpolatable, float _startTime, float _targetTime, float _delta, Transform _transform = null) {
            float newTime = Interpolation.MoveTimeTowardsWrapped(_startTime * 360.0f, _targetTime * 360.0f, _delta * 360.0f) / 360.0f;
            return _interpolatable.GetPositionAtTime(_transform, newTime);
        }
    }

    #region 2D
    public interface ILerpPathVector2 : ILerpPathVector3 {
        new public Vector2 GetPositionAtTime(float _t);

        static public Vector2 LerpBetween(ILerpPathVector2 _shapeA, ILerpPathVector2 _shapeB, float _time) {
            return Vector2.Lerp(_shapeA.GetPositionAtTime(_time), _shapeB.GetPositionAtTime(_time), _time);
        }
    }

    public static class ILerpPathVector2Extensions {
        public static Vector2 GetPositionAtTime(this ILerpPathVector2 _interpolatable, Transform _transform, float _time) {
            return ((ILerpPathVector3)_interpolatable).GetPositionAtTime(_transform, _time);
        }

        public static Vector2 LerpAlongPathTowards(this ILerpPathVector2 _interpolatable, float _startTime, float _targetTime, float _delta, Transform _transform = null) {
            return ((ILerpPathVector3)_interpolatable).LerpAlongPathTowards(_startTime, _targetTime, _delta, _transform);
        }

        public static Vector2 LerpAlongPathTowardsWrapped(this ILerpPathVector2 _interpolatable, float _startTime, float _targetTime, float _delta, Transform _transform = null) {
            return ((ILerpPathVector3)_interpolatable).LerpAlongPathTowardsWrapped(_startTime, _targetTime, _delta, _transform);
        }
    }
    #endregion
}