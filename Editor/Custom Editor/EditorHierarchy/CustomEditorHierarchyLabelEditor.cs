
using UnityEngine;
using UnityEditor;

namespace MaroonSealEditor {
    [CustomEditor(typeof(CustomEditorHierarchyLabel))]
    public class CustomEditorHierarchyLabelEditor : Editor
    {
        public override void OnInspectorGUI() {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("useDefaultColours"));
            
            if (!(target as CustomEditorHierarchyLabel).useDefaultColours) { 
                EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultColour"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("selectedColour"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("selectedUnfocusedColour"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("hoveredColour"));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

