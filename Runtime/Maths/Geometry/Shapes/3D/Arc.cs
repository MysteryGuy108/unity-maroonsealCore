using UnityEngine;

namespace MaroonSeal.Maths.Shapes {
    [System.Serializable]
    public struct Arc : IShape3D, IPolarShape
    {
        public PointTransform transform;
        readonly public PointTransform Transform => transform;
        [Space]
        public float radius;
        public float startDegrees;
        public float endDegrees;

        public float StartRadians {
            readonly get => startDegrees * Mathf.Deg2Rad;
            set => startDegrees = value * Mathf.Rad2Deg;
        }

        public float EndRadians {
            readonly get => endDegrees * Mathf.Deg2Rad;
            set => endDegrees = value * Mathf.Rad2Deg;
        }

        #region Constructors
        public Arc(PointTransform transform, float radius, float _startDegrees, float _endDegrees) {
            this.transform = transform;
            this.radius = radius;
            this.startDegrees = _startDegrees;
            this.endDegrees = _endDegrees;
        }
        #endregion

        readonly public float GetLength() => Mathf.Abs(GetRadiansDelta() * radius);

        readonly public float GetDegreesDelta() => Mathf.DeltaAngle(startDegrees, endDegrees);
        readonly public float GetRadiansDelta() => GetDegreesDelta() * Mathf.Deg2Rad;

        #region IPolarSpaceShape
        readonly public Vector3 EvaluatePositionAtTheta(float _theta) {
            return transform.TransformPosition(IPolarShape.ToCartesian(radius, _theta));
        }

        readonly public Vector3 EvaluateTangentAtTheta(float _theta) {
            return transform.TransformVector(IPolarShape.GetCircleTangent(_theta));
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