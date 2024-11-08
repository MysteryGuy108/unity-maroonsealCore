using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MaroonSeal.Packages.EditorExtras {

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ComponentCallback), true)]
    public class ComponentCallbackDrawer : PropertyDrawer
    {
        private struct MethodReference {
            public Component component;
            public string methodName;
            public string GetPath() { return component.GetType().Name + "/" + methodName;}
        }

        SerializedProperty activeProperty;

        #region Property Drawer
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {

            EditorGUI.BeginProperty(_position, _label, _property);
            Rect linePosition = _position;
            linePosition.height = 15.0f;

            _property.isExpanded = EditorGUI.Foldout(linePosition, _property.isExpanded, _label);
            if (!_property.isExpanded) { return; }
            
            linePosition.y += linePosition.height;

            Rect contentPos = EditorGUI.PrefixLabel(linePosition, new GUIContent("Target"));

            SerializedProperty targetComponentProperty = _property.FindPropertyRelative("targetComponent");
            SerializedProperty methodNameProperty = _property.FindPropertyRelative("methodName");

            GameObject parentGameObject = GetCurrentGameObject(targetComponentProperty);

            GameObject newParent = (GameObject)EditorGUI.ObjectField(contentPos, parentGameObject, typeof(UnityEngine.Object), true);
            
            string msg = "No GameObject";
            if (newParent != null) {
                msg = targetComponentProperty.objectReferenceValue.GetType().Name + "/" + methodNameProperty.stringValue;
            }

            linePosition.y += linePosition.height;
            contentPos = EditorGUI.PrefixLabel(linePosition, new GUIContent("Method"));

            EditorGUI.BeginDisabledGroup(!newParent);
            if (GUI.Button(contentPos, msg)) {
                if (!newParent) { return; }
                GenericMenu methodSelectionMenu = new();

                foreach(Component component in newParent.GetComponents<Component>()) {
                    List<MethodReference> methodReferences = GetValidMethods(component);
                    foreach(MethodReference method in methodReferences) {
                        methodSelectionMenu.AddItem(new GUIContent(method.GetPath()), false, SetComponentCallbackMethod, method);
                    }
                }
                activeProperty = _property;
                methodSelectionMenu.ShowAsContext();
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if (!property.isExpanded) { return 15.0f;}
            return 45.0f;
        }
        #endregion

        private GameObject GetCurrentGameObject(SerializedProperty _componentProperty) {
            if (_componentProperty.objectReferenceValue == null) { return null; }
            return (_componentProperty.objectReferenceValue as Component).gameObject;
        }

        private void SetComponentCallbackMethod(object _methodReferenceObject) {
            MethodReference reference = (MethodReference)_methodReferenceObject;

            activeProperty.FindPropertyRelative("targetComponent").objectReferenceValue = reference.component;
            activeProperty.FindPropertyRelative("methodName").stringValue = reference.methodName;
            activeProperty.serializedObject.ApplyModifiedProperties();
            activeProperty = null;
        }

        private List<MethodReference> GetValidMethods(Component _component) {
            Type returnType = typeof(void);

            Type[] genericTypes = fieldInfo.FieldType.GenericTypeArguments;
            if (genericTypes.Length > 0) { returnType = genericTypes[0]; }
            
            List<MethodReference> methodReferences = new();
            foreach (MethodInfo method in _component.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)) {
                if (method.ReturnType != returnType) { continue; }
                
                ParameterInfo[] parameters = method.GetParameters();
                bool hasValidArgs = parameters.Length == genericTypes.Length-1;
                
                for(int i = 0; i < parameters.Length && hasValidArgs; i++) {
                    hasValidArgs = parameters[i].ParameterType == genericTypes[i+1];
                }

                if (!hasValidArgs) { continue; }
                methodReferences.Add(new MethodReference() { component = _component, methodName = method.Name});
            }
            return methodReferences;
        }
    }
#endif

}


