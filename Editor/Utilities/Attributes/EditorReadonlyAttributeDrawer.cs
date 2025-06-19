using UnityEngine;
using UnityEngine.UIElements;

using UnityEditor;
using UnityEditor.UIElements;

using MaroonSeal;

namespace MaroonSealEditor
{
    [CustomPropertyDrawer(typeof(EditorReadonlyAttribute))]
    public class EditorReadonlyAttributeDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            PropertyField root = new(property) {
                enabledSelf = false
            };
            return root;
        }
    }
}