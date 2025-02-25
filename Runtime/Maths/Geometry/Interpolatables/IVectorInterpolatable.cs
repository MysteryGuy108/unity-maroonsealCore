using UnityEngine;


namespace MaroonSeal.Maths {
    #region Vector3
    public interface IVector3Interpolatable : IInterpolatable {
        public Vector3 InterpolateVector3(float _t);
    }

    public static class Vector3InterpolatableExtensions {
        public static Vector3 Interpolate(this IVector3Interpolatable _interpolatable, Transform _transform, float _time) {
            Vector3 localPoint = _interpolatable.InterpolateVector3(_time);
            if (!_transform) { return localPoint;}
            return _transform.TransformPoint(localPoint);
        }

        public static Vector3 InterpolateTowards(this IVector3Interpolatable _interpolatable, float _startTime, float _targetTime, float _delta) {
            float newTime = Mathf.MoveTowards(_startTime, _targetTime, _delta);
            return _interpolatable.InterpolateVector3(newTime);
        }

        public static Vector3 InterpolateTowardsWrapped(this IVector3Interpolatable _interpolatable, float _startTime, float _targetTime, float _delta) {
            float newTime = Mathf.MoveTowardsAngle(_startTime * 360.0f, _targetTime * 360.0f, _delta * 360.0f) / 360.0f;
            return _interpolatable.InterpolateVector3(newTime);
        }
    }
    #endregion

    #region Vector2
    public interface IVector2Interpolatable : IVector3Interpolatable {
        public Vector2 InterpolateVector2(float _t);
    }

    public static class Vector2InterpolatableExtensions {
        public static Vector2 Interpolate(this IVector2Interpolatable _interpolatable, Transform _transform, float _time) {
            return ((IVector3Interpolatable)_interpolatable).Interpolate(_transform, _time);
        }

        public static Vector2 InterpolateTowards(this IVector2Interpolatable _interpolatable, float _startTime, float _targetTime, float _delta) {
            return ((IVector3Interpolatable)_interpolatable).InterpolateTowards(_startTime, _targetTime, _delta);
        }

        public static Vector2 InterpolateTowardsWrapped(this IVector2Interpolatable _interpolatable, float _startTime, float _targetTime, float _delta) {
            return ((IVector3Interpolatable)_interpolatable).InterpolateTowardsWrapped(_startTime, _targetTime, _delta);
        }
    }
    #endregion
}