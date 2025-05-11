using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

using UnityEditor;
using UnityEditor.UIElements;

namespace MaroonSealEditor.GeometryPaths {
    static public class AssetBuilderUtility
    {
        /*
        #region Visual Elements
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            visualTree.CloneTree(root);
            
            pathAssetProperty = root.Q<PropertyField>("property_pathAsset");
            pathAssetProperty.RegisterCallback<ChangeEvent<Object>>(OnPathAssetChanged);

            pathAssetCreationButton = root.Q<Button>("button_createNewAsset");
            pathAssetCreationButton.RegisterCallback<ClickEvent>(ShowCreateNewAssetMenu);

            pathEditingPanel = root.Q<VisualElement>("panel_pathAssetEditor");
            pathAssetInspectorPanel = pathEditingPanel.Q<VisualElement>("panel_pathAssetInspector");

            SetPathEditingPanel(pathAssetProperty.dataSource as Object);

            return root;
        }

        private void OnPathAssetChanged(ChangeEvent<Object> _objectChangeEvent) {
            SetPathEditingPanel(_objectChangeEvent.newValue);
        }
        #endregion

        #region Scene GUI
        public void OnSceneGUI() {
            if (!pathAssetEditor) { return; }
            if (pathAssetEditor is not GeometryPathAssetEditor) { return; }
            GeometryPathAssetEditor pathEditor = (GeometryPathAssetEditor)pathAssetEditor;
            
            pathEditor.DrawHandles((target as PathFilter).transform);
        }
        #endregion
        
        #region Asset Selection
        private void SetPathEditingPanel(Object _newObject) {
            if (pathAssetInspectorPanel.Contains(pathAssetEditorElement)) {
                pathAssetInspectorPanel.Remove(pathAssetEditorElement);
            }

            if (_newObject) {
                pathAssetCreationButton.style.display = DisplayStyle.None;
                
                pathAssetEditor = CreateEditor(_newObject);
                if (!pathAssetEditor) { return; }

                pathAssetEditorElement?.Clear();
                pathAssetEditorElement = new InspectorElement(pathAssetEditor);

                pathAssetInspectorPanel.Add(pathAssetEditorElement);
                pathEditingPanel.style.display = DisplayStyle.Flex;
                return;
            }

            pathAssetCreationButton.style.display = DisplayStyle.Flex;
            pathEditingPanel.style.display = DisplayStyle.None;

            pathAssetEditor = null;
            pathAssetEditorElement?.Clear();
        }
        #endregion
        */

        #region Asset Creation
        /*
        private void ShowCreateNewAssetMenu(ClickEvent _event) {
            GenericMenu menu = new();
            menu.AddItem(new GUIContent("Linear Path"), false, () => CreateNewPathDataAsset<LinearPathAsset>());
            menu.AddItem(new GUIContent("Spline Path"), false, () => CreateNewPathDataAsset<SplinePathAsset>());
            menu.ShowAsContext();
        }

        private void CreateNewPathDataAsset<TPathAsset>() where TPathAsset : GeometryPathAsset {
            TPathAsset newAsset = ScriptableObject.CreateInstance<TPathAsset>();
            
            string filePath = GetPathAssetDirectoryPath();
            string fileName = "New " + (target as Component).name + " Path Asset.asset";

            AssetDatabase.CreateAsset(newAsset, filePath + fileName);
            AssetDatabase.SaveAssets();

            (target as PathAssetFilter).SetSharedAsset(newAsset);

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = newAsset;
            
            serializedObject.ApplyModifiedProperties();
        }

        private string GetPathAssetDirectoryPath() {
            string directoryPath = SceneManager.GetActiveScene().path;
            string directoryName = SceneManager.GetActiveScene().name + " Path Assets";

            for(int i = directoryPath.Length-1; i >= 0; i--) {
                if (directoryPath[i] == '/') { break; }
                directoryPath = directoryPath[..i];
            }

            if (!AssetDatabase.IsValidFolder(directoryPath + directoryName)) {
                string folderPath = directoryPath[..^1];
                AssetDatabase.CreateFolder(folderPath, directoryName);
            }

            return directoryPath + directoryName + "/";
        }
        */
        #endregion
    }
    
}