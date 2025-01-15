using UnityEngine;

namespace MaroonSealEditor {
    [DisallowMultipleComponent]
    public class CustomEditorHierarchyLabel : MonoBehaviour
    {
        public bool useDefaultColours = true;

        [SerializeField] private Color defaultColour = new(0.2196f, 0.2196f, 0.2196f, 1.0f);
        [SerializeField] private Color selectedColour = new(0.1725f, 0.3647f, 0.5294f, 1.0f);
        [SerializeField] private Color selectedUnfocusedColour = new(0.3f, 0.3f, 0.3f, 1.0f);
        [SerializeField] private Color hoveredColour = new(0.2706f, 0.2706f, 0.2706f, 1.0f);

        public Color GetColour(bool _isSelected, bool _isHovered, bool _isFocused) {
            if (_isSelected) {
                return _isFocused ? selectedColour : selectedUnfocusedColour;
            }
            return _isHovered ? hoveredColour : defaultColour;
        }
    }
}