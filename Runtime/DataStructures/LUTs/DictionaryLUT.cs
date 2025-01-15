using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace MaroonSeal.DataStructures.LUTs {

    [System.Serializable]
    public class DictionaryLUT<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [NonReorderable][SerializeField] private List<LUTItem<TKey, TValue>> itemList;

        #region ISerializationCallbackReceiver
        public void OnBeforeSerialize() {
            itemList ??= new();
            itemList.Clear();

            foreach(KeyValuePair<TKey, TValue> pair in this) {
                itemList.Add(new(pair));
            }
        }

        public void OnAfterDeserialize() {
            this.Clear();
            
            Dictionary<TKey, TValue> previous = new(this);

            for(int i = 0; i < itemList.Count; i++) {

                if (this.ContainsKey(itemList[i].Key)) { 
                    Debug.LogError("Map elements cannot share the same key");
                    this.Clear();
                    foreach(KeyValuePair<TKey, TValue> pair in previous) { this.Add(pair.Key, pair.Value); }
                    break;
                }

                this.Add(itemList[i].Key, itemList[i].Data);
            }

            previous?.Clear();
        }
        #endregion
    }
}