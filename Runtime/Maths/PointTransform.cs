using System;
using UnityEngine;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct PointTransform : IEquatable<PointTransform> {
        public Vector3 position;
        public Vector3 eulerAngles;
        public Vector3 scale;

        public Vector3 Forward { 
            readonly get { return Rotation * Vector3.forward; }
            set { Rotation = Quaternion.LookRotation(value, Up); }
        }

        public Vector3 Right { 
            readonly get { return Rotation * Vector3.right; }
            set { Up = Vector3.Cross(Forward, value); }
        }

        public Vector3 Up { 
            readonly get { return Rotation * Vector3.up; }
            set { Rotation = Quaternion.LookRotation(Forward, value); }
        }

        public Quaternion Rotation { 
            readonly get => Quaternion.Euler(eulerAngles);
            set => eulerAngles = value.eulerAngles;
        }


        #region Constructors
        public PointTransform(Vector3 _position) {
            position = _position;
            eulerAngles = Vector3.zero;
            scale = Vector3.one;
        }

        public PointTransform(Vector3 _position, Quaternion _rotation) {
            position = _position;
            eulerAngles = _rotation.eulerAngles;
            scale = Vector3.one;
        }

        public PointTransform(Vector3 _position, Vector3 _forward, Vector3 _up) {
            position = _position;
            eulerAngles = Quaternion.LookRotation(_forward, _up).eulerAngles;
            scale = Vector3.one;
        }

        public PointTransform(Vector3 _position, Quaternion _rotation, Vector3 _scale) {
            position = _position;
            eulerAngles = _rotation.eulerAngles;
            scale = _scale;
        }

        public PointTransform(Transform _transform) {
            position = _transform.position;
            eulerAngles = _transform.eulerAngles;
            scale = _transform.localScale;
        }
        #endregion

        #region Operators
        readonly public bool Equals(PointTransform _other) {
            return this.position == _other.position && 
                this.eulerAngles == _other.eulerAngles && 
                this.scale == _other.scale;
        }
        public override readonly bool Equals(object obj) => this.Equals((PointTransform)obj);

        public override readonly int GetHashCode() {
            unchecked {
                return HashCode.Combine(position, eulerAngles, scale);
            }
        }
        public static bool operator ==(PointTransform _a, PointTransform _b) => _a.Equals(_b);
        public static bool operator !=(PointTransform _a, PointTransform _b) => !_a.Equals(_b);
        #endregion

        readonly public PointTransform GetLocalPoint(Transform _transform) {
            PointTransform localisedPoint = new(this.position, this.Rotation, this.scale);
            localisedPoint.position = _transform.InverseTransformPoint(localisedPoint.position);
            localisedPoint.Forward = _transform.InverseTransformDirection(localisedPoint.Forward);
            localisedPoint.Up = _transform.InverseTransformDirection(localisedPoint.Up);

            return localisedPoint;
        }

        readonly public PointTransform GetGlobalPoint(Transform _transform) {
            PointTransform globalPoint = new(this.position, this.Rotation, this.scale);
            globalPoint.position = _transform.TransformPoint(globalPoint.position);
            globalPoint.Forward = _transform.TransformDirection(globalPoint.Forward);
            globalPoint.Up = _transform.TransformDirection(globalPoint.Up);
            return globalPoint;
        }

        #region Static
        static public PointTransform Lerp(PointTransform _a, PointTransform _b, float _t) {
            return new PointTransform(
                Vector3.Lerp(_a.position, _b.position, _t),
                Quaternion.Lerp(_a.Rotation, _b.Rotation, _t),
                Vector3.Lerp(_a.scale, _b.scale, _t)
            );
        }
        #endregion
    }
}
