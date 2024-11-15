using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using MaroonSeal.Core.EditorHelpers;

namespace MaroonSeal.Core.DataStructures.Drawers {

    [CustomPropertyDrawer(typeof(Map<,>.Element))]
    public class MapElementDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {
            EditorGUI.BeginProperty(_position, _label, _property);
            
            SerializedProperty valueProperty = _property.FindPropertyRelative("dataValue");
            if (valueProperty.propertyType == SerializedPropertyType.Generic) {
                DrawVerticalKeyValue(_position, _property, _label);
            }
            else {
                DrawInLineKeyValue(_position, _property, _label);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) {
            return EditorGUI.GetPropertyHeight(_property.FindPropertyRelative("dataValue"), _label, true);
        }

        private void DrawInLineKeyValue(Rect _position, SerializedProperty _property, GUIContent _label) {
            Rect headerPosition = _position;
            headerPosition.height = 18.0f;
            headerPosition.width = EditorGUIUtility.labelWidth;

            SerializedProperty keyProperty = _property.FindPropertyRelative("key");
            EditorGUI.PropertyField(headerPosition, keyProperty, GUIContent.none);

            Rect contentPosition = _position;
            contentPosition.x += EditorGUIUtility.labelWidth+8;
            contentPosition.width -= EditorGUIUtility.labelWidth+8;

            contentPosition.height = 18.0f;

            SerializedProperty valueProperty = _property.FindPropertyRelative("dataValue");
            EditorGUI.PropertyField(contentPosition, valueProperty, GUIContent.none);
        }

        private void DrawVerticalKeyValue(Rect _position, SerializedProperty _property, GUIContent _label) {
            Rect headerPosition = _position;
            headerPosition.x += 16.0f;
            headerPosition.height = 18.0f;
            headerPosition.width = EditorGUIUtility.labelWidth;

            SerializedProperty keyProperty = _property.FindPropertyRelative("key");
            EditorGUI.PropertyField(headerPosition, keyProperty, GUIContent.none);

            Rect contentPosition = _position;

            SerializedProperty valueProperty = _property.FindPropertyRelative("dataValue");
            EditorGUI.PropertyField(contentPosition, valueProperty, GUIContent.none, true);
        }
    }
}