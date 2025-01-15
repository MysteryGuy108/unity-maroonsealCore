using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.DataStructures.LUTs {
    
    [System.Serializable]
    public struct LUTItem<TKey, TData> {

        [SerializeField] private TKey key;
        readonly public TKey Key { get { return key; } }
        [SerializeField] private TData data;
        readonly public TData Data { get { return data; } }

        public LUTItem(TKey _key, TData _value) { key = _key; data = _value;}
        public LUTItem((TKey, TData) _pair) { key = _pair.Item1; data = _pair.Item2; }
        public LUTItem(KeyValuePair<TKey, TData> _pair) { key = _pair.Key; data = _pair.Value; }

        readonly public override bool Equals(object _obj) {

            if (_obj is not LUTItem<TKey, TData>) { return false; }
            LUTItem<TKey, TData> item = (LUTItem<TKey, TData>) _obj;
            return this.Key.Equals(item.Key) && this.Data.Equals(item.Data);
        }

        readonly override public int GetHashCode() {
            unchecked {
                return HashCode.Combine(key.GetHashCode(), data.GetHashCode());
            }
        }
    }
}