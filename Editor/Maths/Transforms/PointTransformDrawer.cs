
using UnityEngine;
using UnityEngine.UIElements;

using UnityEditor;
using UnityEditor.UIElements;

using MaroonSeal.Maths;


namespace MaroonSealEditor.Maths {
    [CustomPropertyDrawer(typeof(PointTransform))]
    public class PointTransformDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty _property) {
            Foldout root = new() {
                text = _property.displayName,
                bindingPath = _property.propertyPath
            };

            root.Add(new PropertyField(_property.FindPropertyRelative("position")));
            root.Add(new PropertyField(_property.FindPropertyRelative("eulerAngles")));
            root.Add(new PropertyField(_property.FindPropertyRelative("scale")));

            return root;
        }

        #region Property Conversions
        static public PointTransform FromProperty(SerializedProperty _property) {
            return new PointTransform() {
                position = _property.FindPropertyRelative("position").vector3Value,
                eulerAngles = _property.FindPropertyRelative("eulerAngles").vector3Value,
                scale = _property.FindPropertyRelative("scale").vector3Value
            };
        }

        static public void SetProperty(SerializedProperty _property, PointTransform _pointTransform) {
            _property.FindPropertyRelative("position").vector3Value = _pointTransform.position;
            _property.FindPropertyRelative("eulerAngles").vector3Value = _pointTransform.eulerAngles;
            _property.FindPropertyRelative("scale").vector3Value = _pointTransform.scale;
        }
        #endregion

        #region Point Transform Handles
        static public void DrawTransformHandle(PointTransform _pointTransform, Vector3? _scale = null, Color? _xColour = null, Color? _yColour = null, Color? _zColour = null,
                                        bool _negativeXAxis = true, bool _negativeYAxis = true, bool _negativeZAxis = true) {
            Vector3 scale = _scale ?? Vector3.one;

            Color xAxisColour = _xColour ?? Color.red;
            Color yAxisColour = _yColour ?? Color.green;
            Color zAxisColour = _zColour ?? Color.blue;

            Quaternion rotation = _pointTransform.Rotation;
            
            

            Handles.color = xAxisColour;
            Vector3 right = rotation * Vector3.right * _pointTransform.scale.x * 0.5f * scale.x;
            if (_negativeXAxis) { Handles.DrawLine(_pointTransform.position - right, _pointTransform.position + right); }
            else { Handles.DrawLine(_pointTransform.position, _pointTransform.position + right); }

            Handles.color = yAxisColour;
            Vector3 up = rotation * Vector3.up * _pointTransform.scale.y * 0.5f * scale.y;
            if (_negativeYAxis) { Handles.DrawLine(_pointTransform.position - up, _pointTransform.position + up); }
            else { Handles.DrawLine(_pointTransform.position, _pointTransform.position + up); }
            

            Handles.color = zAxisColour;
            Vector3 forward = rotation * Vector3.forward * _pointTransform.scale.z * 0.5f * scale.z;
            if (_negativeZAxis) { Handles.DrawLine(_pointTransform.position - forward, _pointTransform.position + forward); }
            else { Handles.DrawLine(_pointTransform.position, _pointTransform.position + forward); }
        }

        static public PointTransform DrawPositionHandle(PointTransform _transform, Quaternion? _rotationOverride = null) {
            Quaternion rotation = _rotationOverride ?? _transform.Rotation;
            _transform.position = Handles.PositionHandle(_transform.position, rotation);
            return _transform;
        }

        static public PointTransform DrawRotationHandle(PointTransform _transform, Quaternion? _rotationOverride = null) {
            Quaternion rotation = _rotationOverride ?? _transform.Rotation;
            _transform.Rotation = Handles.RotationHandle(rotation, _transform.position);
            return _transform;
        }

        static public PointTransform DrawScaleHandle(PointTransform _transform, Quaternion? _rotationOverride = null) {
            Quaternion rotation = _rotationOverride ?? _transform.Rotation;
            _transform.scale = Handles.ScaleHandle(_transform.scale, _transform.position, rotation);
            return _transform;
        }
        #endregion
    
        #region Property Handles
        static public void DrawAxisHandle(SerializedProperty _pointTransformProperty, Vector3? _scale = null, Color? _xAxisColour = null, Color? _yAxisColour = null, Color? _zAxisColour = null) {
            PointTransform transform = FromProperty(_pointTransformProperty);
            DrawTransformHandle(transform, _scale, _xAxisColour, _yAxisColour, _zAxisColour);
        }

        static public bool DrawPositionHandle(SerializedProperty _property, Quaternion? _rotationOverride = null) {
            PointTransform transform = FromProperty(_property);
            PointTransform newTransform = DrawPositionHandle(transform, _rotationOverride);
            SetProperty(_property, newTransform);
            return transform != newTransform;
        }

        static public bool DrawRotationHandle(SerializedProperty _property, Quaternion? _rotationOverride = null) {
            PointTransform transform = FromProperty(_property);
            PointTransform newTransform = DrawRotationHandle(transform, _rotationOverride);
            SetProperty(_property, newTransform);
            return transform != newTransform;
        }

        static public bool DrawScaleHandle(SerializedProperty _property, Quaternion? _rotationOverride = null) {
            PointTransform transform = FromProperty(_property);
            PointTransform newTransform = DrawScaleHandle(transform, _rotationOverride);
            SetProperty(_property, newTransform);
            return transform != newTransform;
        }
        #endregion
    }
}


