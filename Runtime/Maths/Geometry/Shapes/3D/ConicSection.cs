using System;

using UnityEngine;

using MaroonSeal.Maths.SDFs;

namespace MaroonSeal.Maths.Shapes {

    [System.Serializable]
    public struct ConicSection : IShape3D, IPolarShape, ISDFShape
    {
        public enum CurveType { Circle, Ellipse, Parabola, Hyperbola }

        public PointTransform transform;
        readonly public PointTransform Transform => transform;

        [Min(-1.0f)] public float eccentricity;
        [Min(0.0f)] public float minR;
        
        public readonly float SemiLatusRectum { get => minR * (1.0f + eccentricity); }
        public readonly float MajorAxis { get => SemiLatusRectum / (1.0f-(eccentricity * eccentricity)); }

        #region Shape3D
        public void Rotate(Quaternion _rotation) {
            this.transform.position = _rotation * transform.position;
            this.transform.Rotation = _rotation * transform.Rotation;
        }

        public void Translate(Vector3 _translation) =>
            this.transform.position += _translation;
        #endregion

        #region Conic Section
        readonly public float GetBetweenFociDistance() => 2.0f * eccentricity * MajorAxis;

        readonly public (Vector3, Vector3) GetFoci() {
            Vector3 foci2 = GetBetweenFociDistance() * -Vector3.right;
            return (transform.position, transform.TransformPosition(foci2));
        }

        readonly public CurveType GetCurveType() {
            if (eccentricity <= 0) { return CurveType.Circle; }
            else if (eccentricity > 0 && eccentricity < 1.0f) { return CurveType.Ellipse; }
            else if (eccentricity == 1.0f) { return CurveType.Parabola; }
            return CurveType.Hyperbola;
        }
        #endregion

        #region IPolarSpaceShape
        readonly public Vector3 EvaluatePositionAtTheta(float _theta)
        {
            float radius = SemiLatusRectum / (1.0f + (eccentricity * Mathf.Cos(_theta)));
            Vector3 localPoint = new Vector3(Mathf.Cos(_theta), Mathf.Sin(_theta)) * radius;
            return transform.TransformPosition(localPoint);
        }

        readonly public Vector3 EvaluateTangentAtTheta(float _theta) {
            float radius = eccentricity * Mathf.Sin(_theta) / (1.0f + eccentricity * Mathf.Cos(_theta));
            Vector3 localPoint = new Vector3(Mathf.Cos(_theta), Mathf.Sin(_theta)) * radius;
            return transform.TransformPosition(localPoint);
        }
        #endregion

        #region IInterpolationShape
        readonly public Vector3 EvaluatePositionAtTime(float _t) {
            return EvaluatePositionAtTheta(_t * Mathf.PI * 2.0f);
        }

        readonly public Vector3 EvaluateTangentAtTime(float _t) {
            throw new System.NotImplementedException();
        }
        #endregion

        #region ISDFShape
        public float GetSignedDistance(Vector3 _sample) {
            _sample = transform.InverseTransformPosition(_sample);

            throw new NotImplementedException();
        }
        #endregion
    }
}