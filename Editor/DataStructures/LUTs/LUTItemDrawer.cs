using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using MaroonSeal.DataStructures.LUTs;

namespace MaroonSealEditor.DataStructures.LUTs {

    [CustomPropertyDrawer(typeof(LUTItem<,>))]
    public class LUTItemDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {
            EditorGUI.BeginProperty(_position, _label, _property);
            
            SerializedProperty valueProperty = _property.FindPropertyRelative("data");
            if (valueProperty.propertyType == SerializedPropertyType.Generic) {
                DrawVerticalKeyValue(_position, _property, _label);
            }
            else {
                DrawInLineKeyValue(_position, _property, _label);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) {
            return EditorGUI.GetPropertyHeight(_property.FindPropertyRelative("data"), _label, true);
        }

        private void DrawInLineKeyValue(Rect _position, SerializedProperty _property, GUIContent _label) {
            Rect headerPosition = _position;
            headerPosition.height = 18.0f;
            headerPosition.width = EditorGUIUtility.labelWidth;

            DrawDelayedKeyField(headerPosition, _property.FindPropertyRelative("key"));

            Rect contentPosition = _position;
            contentPosition.x += EditorGUIUtility.labelWidth+8;
            contentPosition.width -= EditorGUIUtility.labelWidth+8;

            contentPosition.height = 18.0f;

            SerializedProperty valueProperty = _property.FindPropertyRelative("data");
            EditorGUI.PropertyField(contentPosition, valueProperty, GUIContent.none);
        }

        private void DrawVerticalKeyValue(Rect _position, SerializedProperty _property, GUIContent _label) {
            Rect headerPosition = _position;
            headerPosition.x += 16.0f;
            headerPosition.height = 18.0f;
            headerPosition.width = EditorGUIUtility.labelWidth;

            DrawDelayedKeyField(headerPosition, _property.FindPropertyRelative("key"));

            Rect contentPosition = _position;

            SerializedProperty valueProperty = _property.FindPropertyRelative("data");
            EditorGUI.PropertyField(contentPosition, valueProperty, GUIContent.none, true);
        }

        private void DrawDelayedKeyField(Rect _position, SerializedProperty _keyProperty) {
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);

            switch(_keyProperty.propertyType) {
                case SerializedPropertyType.Float:
                    EditorGUI.DelayedFloatField(_position, _keyProperty, GUIContent.none);
                    break;
                case SerializedPropertyType.Integer:
                    EditorGUI.DelayedIntField(_position, _keyProperty, GUIContent.none);
                    break;
                case SerializedPropertyType.String:
                    EditorGUI.DelayedTextField(_position, _keyProperty, GUIContent.none);
                    break;
                default:
                    EditorGUI.PropertyField(_position, _keyProperty, GUIContent.none);
                    break;
            }

            EditorGUI.EndDisabledGroup();
        }
    }
}