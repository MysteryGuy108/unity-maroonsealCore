using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace MaroonSeal.Core.DataStructures.LUTs.Drawers {

    [CustomPropertyDrawer(typeof(OrderedLUT<>))]
    public class OrderedLUTDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {
            EditorGUI.BeginProperty(_position, _label, _property);
            EditorGUI.PropertyField(_position, _property.FindPropertyRelative("lookupTable"), _label, true);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) {
            return EditorGUI.GetPropertyHeight(_property.FindPropertyRelative("lookupTable"), _label, true);
        }
    }
}