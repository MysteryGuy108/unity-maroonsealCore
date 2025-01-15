using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;
using UnityEditor;

using MaroonSeal.Callbacks;

namespace MaroonSealEditor.Callbacks {

    [CustomPropertyDrawer(typeof(ComponentCallbackBase), true)]
    public class ComponentCallbackDrawer : PropertyDrawer
    {
        private struct MethodReference {
            public Component component;
            public string methodName;
            readonly public string GetPath() {
                if (component == null) { return methodName; }
                return component.GetType().Name + "/" + methodName;
            }
        }

        SerializedProperty activeProperty;

        #region Property Drawer
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {
            int currentIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            EditorGUI.BeginProperty(_position, _label, _property);
            Rect linePosition = _position;
            linePosition.height = 15.0f;

            _property.isExpanded = EditorGUI.Foldout(linePosition, _property.isExpanded, _label);
            if (!_property.isExpanded) { return; }
            
            linePosition.y += linePosition.height;
            linePosition.x += 15.0f;
            linePosition.width -= 15.0f;

            Rect contentPos = EditorGUI.PrefixLabel(linePosition, new GUIContent("Target"));

            SerializedProperty parentObjectProperty = _property.FindPropertyRelative("parentObject");
            SerializedProperty targetComponentProperty = _property.FindPropertyRelative("targetComponent");
            SerializedProperty methodNameProperty = _property.FindPropertyRelative("methodName");

            GameObject parentGameObject = (GameObject)EditorGUI.ObjectField(contentPos, parentObjectProperty.objectReferenceValue, typeof(GameObject), true);
            parentObjectProperty.objectReferenceValue = parentGameObject;

            Component targetComponent = targetComponentProperty.objectReferenceValue as Component;
            
            string msg = "None";
            if (parentGameObject != null && targetComponent != null) {
                msg = targetComponentProperty.objectReferenceValue.GetType().Name + "/" + methodNameProperty.stringValue;
            }

            linePosition.y += linePosition.height;
            contentPos = EditorGUI.PrefixLabel(linePosition, new GUIContent("Method"));

            EditorGUI.BeginDisabledGroup(!parentGameObject);
            if (EditorGUI.DropdownButton(contentPos, new GUIContent(msg), FocusType.Keyboard) && parentGameObject != null) {
                activeProperty = _property;
                DrawMethodSelectionMenu(parentGameObject);
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
            EditorGUI.indentLevel = currentIndent;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if (!property.isExpanded) { return 15.0f;}
            return 45.0f;
        }
        #endregion


        #region  Method Selection
        private void DrawMethodSelectionMenu(GameObject _parentObject) {
            GenericMenu methodSelectionMenu = new();

            MethodReference nullReference = new() {
                component = null,
                methodName = "No Method"
            };

            methodSelectionMenu.AddItem(new GUIContent(nullReference.GetPath()), false, SetComponentCallbackMethod, nullReference);
            methodSelectionMenu.AddSeparator("");

            bool isFunc = fieldInfo.FieldType.IsSubclassOf(typeof(ComponentCallbackFunc));

            foreach(Component component in _parentObject.GetComponents<Component>()) {
                List<MethodReference> methodReferences = GetValidMethods(component, isFunc);
                foreach(MethodReference method in methodReferences) {
                    methodSelectionMenu.AddItem(new GUIContent(method.GetPath()), false, SetComponentCallbackMethod, method);
                }
            }
            
            methodSelectionMenu.ShowAsContext();
        }

        private List<MethodReference> GetValidMethods(Component _component, bool _isFunc) {
        
            Type[] genericTypes = fieldInfo.FieldType.GenericTypeArguments;

            Type returnType;
            Type[] parametreTypes;

            if (_isFunc) { 
                returnType = ComponentCallbackFunc.GetReturnType(genericTypes);
                parametreTypes = ComponentCallbackFunc.GetParametreTypes(genericTypes); 
            }
            else {
                returnType = typeof(void);
                parametreTypes = ComponentCallbackAction.GetParametreTypes(genericTypes); 
            }
            
            List<MethodReference> methodReferences = new();
            foreach (MethodInfo method in _component.GetType().GetMethods()) {
                if (!ComponentCallbackBase.IsMethodValid(method, returnType, parametreTypes)) { continue; }
                methodReferences.Add(new MethodReference() { component = _component, methodName = method.Name});
            }

            return methodReferences;
        }
        
        private void SetComponentCallbackMethod(object _methodReferenceObject) {
            MethodReference reference = (MethodReference)_methodReferenceObject;

            activeProperty.FindPropertyRelative("targetComponent").objectReferenceValue = reference.component;
            activeProperty.FindPropertyRelative("methodName").stringValue = reference.methodName;
            activeProperty.serializedObject.ApplyModifiedProperties();
            activeProperty = null;
        }
        #endregion
    }
}


