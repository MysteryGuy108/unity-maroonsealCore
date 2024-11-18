using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Core.DataStructures {
    
    [System.Serializable]
    public struct LUTElement<TKey, TData> {
        [SerializeField] private TKey key;
        readonly public TKey Key { get { return key; } }
        [SerializeField] private TData data;
        readonly public TData Data { get { return data; } }

        public LUTElement(TKey _key, TData _value) { key = _key; data = _value;}
        public LUTElement((TKey, TData) _pair) { key = _pair.Item1; data = _pair.Item2; }
        public LUTElement(KeyValuePair<TKey, TData> _pair) { key = _pair.Key; data = _pair.Value; }
    }
}