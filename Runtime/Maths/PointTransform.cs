using UnityEngine;

namespace MaroonSeal.Maths {
    public struct PointTransform {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        #region Constructors
        public PointTransform(Vector3 _position) {
            position = _position;
            rotation = Quaternion.identity;
            scale = Vector3.one;
        }

        public PointTransform(Vector3 _position, Quaternion _rotation) {
            position = _position;
            rotation = _rotation;
            scale = Vector3.one;
        }

        public PointTransform(Vector3 _position, Quaternion _rotation, Vector3 _scale) {
            position = _position;
            rotation = _rotation;
            scale = _scale;
        }

        public PointTransform(Transform _transform) {
            position = _transform.position;
            rotation = _transform.rotation;
            scale = _transform.localScale;
        }
        #endregion

        #region Operators
        #endregion

        public Vector3 Forward { 
            readonly get { return rotation * Vector3.forward; }
            set { rotation = Quaternion.LookRotation(value, Up); }
        }

        public Vector3 Right { 
            readonly get { return rotation * Vector3.right; }
            set { 
                Forward = value;
                rotation *= Quaternion.LookRotation(Vector3.right, Vector3.up);
            }
        }
        
        readonly public Vector3 Up { get { return rotation * Vector3.up; }}

        public Vector3 EulerAngles {
            readonly get { return rotation.eulerAngles;}
            set { rotation = Quaternion.Euler(value); }
        }
    }

    public struct PointTransform2D {
        public Vector2 position;
        public float rotation;
        public Vector2 scale;

    }
}
