using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Packages.Data {
    
    [CreateAssetMenu(menuName = "Maroon Seal/Animation/Sprite Sequence", fileName = "New Sprite Sequence")]
    public class SpriteSequenceSO : ScriptableObject
    {
        [SerializeField] private Sprite[] sprites;
        [Space]
        [SerializeField] private float duration;
        public float Duration { get { return duration; } }
        [SerializeField] private AnimationCurve spriteIndexOverTime;

        public Sprite GetSpriteAtTime(float _time) {
            float normalisedTime = Mathf.Clamp01(_time / duration);
            int spriteIndex = (int)(spriteIndexOverTime.Evaluate(normalisedTime) * (sprites.Length-1));
            return sprites[spriteIndex];
        }
    }
}