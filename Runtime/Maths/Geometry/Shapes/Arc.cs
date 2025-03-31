using UnityEngine;

namespace MaroonSeal.Maths.Shapes {
    [System.Serializable]
    public struct Arc : IShape3D, IPolarSpaceShape
    {
        public PointTransform transform;
        readonly public PointTransform Transform => transform;
        [Space]
        public float radius;
        public float startDegrees;
        public float endDegrees;

        #region IArc
        readonly public Vector3 EvaluatePositionAtTheta(float _theta) {
            return transform.TransformPosition(IPolarSpaceShape.ToCartesian(radius, _theta));
        }

        readonly public Vector3 EvaluateTangentAtTheta(float _theta) {
            return transform.TransformVector(IPolarSpaceShape.GetCircleTangent(_theta));
        }

        readonly public Vector3 EvaluatePositionAtTime(float _time) {
            float lerpTheta = Mathf.Lerp(startDegrees, endDegrees, _time) * Mathf.Deg2Rad;
            return EvaluatePositionAtTheta(lerpTheta);
        }

        readonly public Vector3 EvaluateTangentAtTime(float _time) {
            float lerpTheta = Mathf.Lerp(startDegrees, endDegrees, _time) * Mathf.Deg2Rad;
            return EvaluateTangentAtTheta(lerpTheta);
        }
        #endregion
    }
}