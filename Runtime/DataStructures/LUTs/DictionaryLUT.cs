using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace MaroonSeal.Core.DataStructures {

    [System.Serializable]
    public class DictionaryLUT<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [NonReorderable][SerializeField] private List<LUTElement<TKey, TValue>> elementList;

        #region ISerializationCallbackReceiver
        public void OnBeforeSerialize() {
            elementList ??= new();
            elementList.Clear();

            foreach(KeyValuePair<TKey, TValue> pair in this) {
                elementList.Add(new(pair));
            }
        }

        public void OnAfterDeserialize() {
            this.Clear();
            
            Dictionary<TKey, TValue> previous = new(this);

            for(int i = 0; i < elementList.Count; i++) {

                if (this.ContainsKey(elementList[i].Key)) { 
                    Debug.LogError("Map elements cannot share the same key");
                    this.Clear();
                    foreach(KeyValuePair<TKey, TValue> pair in previous) { this.Add(pair.Key, pair.Value); }
                    break;
                }

                this.Add(elementList[i].Key, elementList[i].Data);
            }

            previous?.Clear();
        }
        #endregion
    }
}