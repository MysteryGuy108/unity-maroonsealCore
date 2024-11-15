using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Core.DataStructures {
    [System.Serializable]
    public class Map<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [System.Serializable]
        public struct Element {
            [SerializeField] private TKey key;
            readonly public TKey Key { get { return key; } }
            [SerializeField] private TValue dataValue;
            readonly public TValue Value { get { return dataValue; } }

            public Element(TKey _key, TValue _value) { key = _key; dataValue = _value;}
            public Element(KeyValuePair<TKey, TValue> _pair) { key = _pair.Key; dataValue = _pair.Value; }
        }

        [SerializeField] private List<Element> elementList;

        #region ISerializationCallbackReceiver
        public void OnBeforeSerialize() {
            elementList ??= new();
            elementList.Clear();

            foreach(KeyValuePair<TKey, TValue> pair in this) {
                elementList.Add(new(pair));
            }
        }

        public void OnAfterDeserialize() {
            Dictionary<TKey, TValue> backup = new(this);
            this.Clear();
            for(int i = 0; i < elementList.Count; i++) {

                if (this.ContainsKey(elementList[i].Key)) { 
                    Debug.LogError("Map elements cannot share the same key");
                    this.Clear();
                    foreach(KeyValuePair<TKey, TValue> pair in backup) { this.Add(pair.Key, pair.Value); }
                    break;
                }

                this.Add(elementList[i].Key, elementList[i].Value);
            }

            backup.Clear();
        }
        #endregion
    }
}