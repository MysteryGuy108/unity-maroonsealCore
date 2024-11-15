using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using MaroonSeal.Core.EditorHelpers;

namespace MaroonSeal.Core.DataStructures.Drawers {

    [CustomPropertyDrawer(typeof(Map<,>))]
    public class MapDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {
            EditorGUI.BeginProperty(_position, _label, _property);

            SerializedProperty elementList = _property.FindPropertyRelative("elementList");

            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(_position, elementList, _label);

            if (EditorGUI.EndChangeCheck() && elementList.arraySize >= 2) {
                SerializedProperty lastElementKey = elementList.GetArrayElementAtIndex(elementList.arraySize-1).FindPropertyRelative("key");
                SerializedProperty secondToLastKey = elementList.GetArrayElementAtIndex(elementList.arraySize-2).FindPropertyRelative("key");

                if (GetPropertyElementKeysEqual(lastElementKey, secondToLastKey)) {
                    SetPropertyElementIterativeName(lastElementKey);
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) {
            SerializedProperty elementList = _property.FindPropertyRelative("elementList");
            return EditorGUI.GetPropertyHeight(elementList);
        }

        private bool GetPropertyElementKeysEqual(SerializedProperty _keyPropertyA, SerializedProperty _keyPropertyB) {

            return _keyPropertyA.propertyType switch {
                SerializedPropertyType.Integer => _keyPropertyA.intValue == _keyPropertyB.intValue,
                SerializedPropertyType.Float => _keyPropertyA.floatValue == _keyPropertyB.floatValue,
                SerializedPropertyType.String => _keyPropertyA.stringValue == _keyPropertyB.stringValue,
                _ => false,
            };
        }

        private void SetPropertyElementIterativeName(SerializedProperty _elementProperty) {
            switch(_elementProperty.propertyType) {
                case SerializedPropertyType.Integer:
                    _elementProperty.intValue++;
                    return;
                case SerializedPropertyType.Float:
                    _elementProperty.floatValue++;
                    return;
                case SerializedPropertyType.String:
                    _elementProperty.stringValue = StringFormatting.GetIterativeName(_elementProperty.stringValue);
                    return;
            }
        }
    }
}