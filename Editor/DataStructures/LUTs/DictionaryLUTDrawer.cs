using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using MaroonSeal.DataStructures.LUTs;

namespace MaroonSealEditor.DataStructures.LUTs {

    [CustomPropertyDrawer(typeof(DictionaryLUT<,>))]
    public class DictionaryLUTDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {
            EditorGUI.BeginProperty(_position, _label, _property);

            SerializedProperty itemListProperty = _property.FindPropertyRelative("itemList");

            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(_position, itemListProperty, _label);

            if (EditorGUI.EndChangeCheck() && itemListProperty.arraySize >= 2) {
                SerializedProperty lastElementKey = itemListProperty.GetArrayElementAtIndex(itemListProperty.arraySize-1).FindPropertyRelative("key");
                SerializedProperty secondToLastKey = itemListProperty.GetArrayElementAtIndex(itemListProperty.arraySize-2).FindPropertyRelative("key");

                if (GetPropertyElementKeysEqual(lastElementKey, secondToLastKey)) {
                    SetPropertyElementIterativeName(lastElementKey);
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) {
            SerializedProperty itemListProperty = _property.FindPropertyRelative("itemList");
            return EditorGUI.GetPropertyHeight(itemListProperty);
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