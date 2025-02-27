using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Serializing {
    [System.Serializable]
    abstract public class SerializableDictionaryBase<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        private bool m_Initialised;
        [SerializeField] protected List<SerializableKeyValuePair<TKey, TValue>> itemList = new();
        private Dictionary<TKey, TValue> dictionary = new();

        #region SerializableDictionaryBase
        private void ValidateItemList() {
            List<SerializableKeyValuePair<TKey, TValue>> temp = itemList;
            itemList = ValidateItems(itemList);
            if (itemList != temp) { temp.Clear(); }
        }
        protected virtual List<SerializableKeyValuePair<TKey, TValue>> ValidateItems(List<SerializableKeyValuePair<TKey, TValue>> _list) { return _list; }
        #endregion

        #region IDictionary<,>
        public ICollection<TKey> Keys => dictionary.Keys;
        public ICollection<TValue> Values => dictionary.Values;

        public int Count => dictionary.Count;
        public bool IsReadOnly => false;

        public TValue this[TKey key] { get => dictionary[key]; set => dictionary[key] = value; }

        public void Add(TKey _key, TValue _value) {
            dictionary.Add(_key, _value);
            itemList.Add(new(_key, _value));
            ValidateItemList();
        }

        public void Add(KeyValuePair<TKey, TValue> item) { Add(item.Key, item.Value); }

        public bool Remove(TKey _key) {
            bool wasRemoved = dictionary.Remove(_key);
            itemList.RemoveAll(x => x.Key.Equals(_key));

            if (wasRemoved) { ValidateItemList(); }
            return wasRemoved;
        }
        public bool Remove(KeyValuePair<TKey, TValue> item) { return Remove(item.Key); }

        public void Clear() { dictionary.Clear(); itemList.Clear(); }

        public bool ContainsKey(TKey _key) { return dictionary.ContainsKey(_key); }

        public bool TryGetValue(TKey _key, out TValue _value) {
            return dictionary.TryGetValue(_key, out _value);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) {
            if (!dictionary.ContainsKey(item.Key)) { return false;}
            return dictionary[item.Key].Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
            //dictionary.CopyTo()
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() { return dictionary.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        #endregion

        #region ISerializationCallbackReceiver
        // Save Dictionary<,> to list
        public void OnBeforeSerialize() {
            if (!m_Initialised) { 
                itemList = new();
                dictionary = new();
                m_Initialised = true;
            }
        }

        // Load Dictionary<,> from list
        public void OnAfterDeserialize() {
            Dictionary<TKey, TValue> fallback = new(dictionary);
            dictionary.Clear();

            ValidateItemList();

            for(int i = 0; i < itemList.Count; i++) {

                if (dictionary.ContainsKey(itemList[i].Key)) { 
                    dictionary.Clear(); 
                    dictionary = fallback;
                    Debug.LogError("Dictionary cannot contain two keys of the same value");
                    return;
                }

                dictionary.Add(itemList[i].Key, itemList[i].Value);
            }

            fallback.Clear();
        }
        #endregion
    }
}