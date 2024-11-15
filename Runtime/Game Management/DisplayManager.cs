using System;
using System.Collections;
using System.Collections.Generic;
using MaroonSeal.Core.DataStructures;
using UnityEngine;

namespace MaroonSeal.Core.Management {
    public class DisplayManager : MonoBehaviour
    {
        [Header("Fullscreen Settings")]
        [SerializeField] private Map<string, FullScreenMode> fullscreenOptions;

        [Header("Resolution Settings")]
        private int currentResolutionIndex;

        [Header("Quality Settings")]
        private int qualityIndex;

        private void Awake() {
            Debug.Log(fullscreenOptions["Fullscreen"].ToString());
        }

        public string GetQualityName(int _qualityLevel) {
            return _qualityLevel switch {
                0 => "LOW",
                1 => "MEDIUM",
                2 => "HIGH",
                _ => "N/A",
            };

            //qualityText.text = qualityName;
        }

        public void SetFullscreen(bool _isFullscreen) { 
            //setFullscreen = _isFullscreen; 
        }

        public void ApplyChanges() {
            //Screen.SetResolution(resolutionOptions[currentResolutionIndex].width, resolutionOptions[currentResolutionIndex].height, fullscreenMode);
            QualitySettings.SetQualityLevel(qualityIndex);
        }
    }
}