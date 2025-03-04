using UnityEngine;

namespace MaroonSeal.Maths {
    public struct Point {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        #region Constructors
        public Point(Vector3 _position) {
            position = _position;
            rotation = Quaternion.identity;
            scale = Vector3.one;
        }

        public Point(Vector3 _position, Quaternion _rotation) {
            position = _position;
            rotation = _rotation;
            scale = Vector3.one;
        }

        public Point(Vector3 _position, Vector3 _forward, Vector3 _up) {
            position = _position;
            rotation = Quaternion.LookRotation(_forward, _up);
            scale = Vector3.one;
        }

        public Point(Vector3 _position, Quaternion _rotation, Vector3 _scale) {
            position = _position;
            rotation = _rotation;
            scale = _scale;
        }

        public Point(Transform _transform) {
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
            set { Up = Vector3.Cross(Forward, value); }
        }

        public Vector3 Up { 
            readonly get { return rotation * Vector3.up; }
            set { rotation = Quaternion.LookRotation(Forward, value); }
        }
        public Vector3 Euler {
            readonly get { return rotation.eulerAngles;}
            set { rotation = Quaternion.Euler(value); }
        }

        #region Static
        static public Point Lerp(Point _a, Point _b, float _t) {
            return new Point(
                Vector3.Lerp(_a.position, _b.position, _t),
                Quaternion.Lerp(_a.rotation, _b.rotation, _t),
                Vector3.Lerp(_a.scale, _b.scale, _t)
            );
        }
        #endregion
    }
}
