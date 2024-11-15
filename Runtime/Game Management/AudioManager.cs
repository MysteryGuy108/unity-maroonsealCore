using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace MaroonSeal.Core.Management {
    public class AudioManager : MonoBehaviour
    {
        [Header("Audio Settings")]
        [SerializeField] private AudioMixer mainAudioMixer; 

        public void SetAudioLevel(string _exposedLevel, float _level) {
            mainAudioMixer.SetFloat(_exposedLevel, Mathf.Log10(_level) * 20.0f);
        }

        public float GetAudioLevel(string _exposedLevel) {
            mainAudioMixer.GetFloat(_exposedLevel, out float audioVolume);
            return Mathf.Pow(10.0f, audioVolume / 20.0f);
        }
    }
}