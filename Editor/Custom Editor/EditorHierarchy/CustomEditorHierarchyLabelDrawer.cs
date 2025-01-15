using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.Reflection;

namespace MaroonSealEditor {
    
    [UnityEditor.InitializeOnLoad]
    public class CustomEditorHierarchyLabelDrawer
    {
        static bool hierarchyEditorWindowHasFocus;

        static EditorWindow hierarchyEditorWindow;

        TreeViewController treeViewController;

        static CustomEditorHierarchyLabelDrawer() {
            EditorApplication.hierarchyWindowItemOnGUI += OnHirearchyWindowItemOnGUI;
            EditorApplication.update += OnEditorUpdate;

            var sceneHierarchyType = typeof(Editor).Assembly.GetType("UnityEditor.SceneHierarchy");
        } 

        static private void OnEditorUpdate() {  
            if (hierarchyEditorWindow == null) {
                hierarchyEditorWindow = EditorWindow.GetWindow(System.Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor"));
            }

            hierarchyEditorWindowHasFocus = EditorWindow.focusedWindow != null &&
                EditorWindow.focusedWindow == hierarchyEditorWindow;

            if (Event.current == null) { return; }
        }

        static private void OnHirearchyWindowItemOnGUI(int _instanceID, Rect _selectionRect) {
        
            GameObject obj = EditorUtility.InstanceIDToObject(_instanceID) as GameObject;
            if (obj == null) { return; }
            
            EditorHierarchyLabel hierarchyLabel = obj.GetComponent<EditorHierarchyLabel>();
            if (hierarchyLabel == null) { return; }

            GUIContent content = GetHierarchyContent(obj, hierarchyLabel);

            EditorGUI.DrawRect(_selectionRect, GetBackgroundColour(_instanceID, _selectionRect, hierarchyLabel));

            EditorGUI.LabelField(_selectionRect, content);

            _selectionRect.x += 18.5f;
            _selectionRect.width -= 18.5f;
            EditorGUI.LabelField(_selectionRect, obj.name);
        }

        static private GUIContent GetHierarchyContent(GameObject _obj, EditorHierarchyLabel _label) {
            Component[] components = _obj.GetComponents<Component>();
            if (components == null || components.Length <= 0) { return null; }

            Component targetComponent = null;
            for(int i = 0; i < components.Length; i++) {
                if (components[i] == _label) { continue; }
                if (components[i] is Transform && components.Length > 2) { continue; }

                targetComponent = components[i];
                break;
            }

            if (targetComponent == null) { return null; }

            Type targetType = targetComponent.GetType();

            GUIContent content = EditorGUIUtility.ObjectContent(targetComponent, targetType);
            content.text = null;
            content.tooltip = targetType.Name;

            return content;
        }

        static private Color GetBackgroundColour(int _instanceID, Rect _selectionRect, EditorHierarchyLabel _label) {
            
            Rect hoveringBox = _selectionRect;

            if (hierarchyEditorWindow != null) {
                hoveringBox.x -= hierarchyEditorWindow.position.width - hoveringBox.width; 
                hoveringBox.width = hierarchyEditorWindow.position.width + 16.0f;
            }

            bool isHovering = hoveringBox.Contains(Event.current.mousePosition);
            bool isSelected = Selection.instanceIDs.Contains(_instanceID);
            bool isFocus = hierarchyEditorWindowHasFocus;

            if (!_label.useDefaultColours && !isSelected){
                return isHovering ? _label.HoveringColour : _label.backgroundColour;
            }
            else {
                return UnityEditorBackgroundColourHelper.Get(isSelected, isHovering, isFocus);
            }
        }
    }
}