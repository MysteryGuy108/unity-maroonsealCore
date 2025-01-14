
using UnityEngine;
using UnityEditor;

namespace MaroonSealEditor {
    static public class UnityEditorBackgroundColourHelper
    {
        static readonly Color lightDefaultColour = new(0.7843f, 0.7843f, 0.7843f, 1.0f);
        static readonly Color darkDefaultColour = new(0.2196f, 0.2196f, 0.2196f, 1.0f);

        static readonly Color lightSelectedColour = new(0.22745f, 0.447f, 0.6902f, 1.0f);
        static readonly Color darkSelectedColour = new(0.1725f, 0.3647f, 0.5294f, 1.0f);

        static readonly Color lightSelectedUnfocusedColour = new(0.68f, 0.68f, 0.68f, 1.0f);
        static readonly Color darkSelectedUnfocusedColour = new(0.3f, 0.3f, 0.3f, 1.0f);

        static readonly Color lightHoveredColour = new(0.698f, 0.698f, 0.698f, 1.0f);
        static readonly Color darkHoveredColour = new(0.2706f, 0.2706f, 0.2706f, 1.0f);

        public static Color Get(bool _isSelected, bool _isHovered, bool _isFocused) {
            if (_isSelected) {
                if (_isFocused) {
                    return EditorGUIUtility.isProSkin ? darkSelectedColour : lightSelectedColour;
                }
                else {
                    return EditorGUIUtility.isProSkin ? darkSelectedUnfocusedColour : lightSelectedUnfocusedColour;
                }
            }
            else if (_isHovered) {
                return EditorGUIUtility.isProSkin ? darkHoveredColour : lightHoveredColour;
            }
            else {
                return EditorGUIUtility.isProSkin ? darkDefaultColour : lightDefaultColour;
            }
        }
    }
}

