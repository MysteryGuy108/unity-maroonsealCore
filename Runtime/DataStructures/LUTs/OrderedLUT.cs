using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Core.DataStructures.LUTs {
    [System.Serializable]
    public class OrderedLUT<TData> : IEnumerable, ISerializationCallbackReceiver{
        [NonReorderable][SerializeField] protected List<LUTItem<float, TData>> lookupTable;

        public int Count { get { return lookupTable.Count; } }
        
        public float MinKey { get { return lookupTable.Count <= 0 ? 0.0f : lookupTable[0].Key; } }
        public float MaxKey { get { return lookupTable.Count <= 0 ? 0.0f : lookupTable[^1].Key; } }

        public TData MinData { get { return lookupTable.Count <= 0 ? default : lookupTable[0].Data; } }
        public TData MaxData { get { return lookupTable.Count <= 0 ? default : lookupTable[^1].Data; } }

        public OrderedLUT() { lookupTable = new(); }

        public OrderedLUT(List<LUTItem<float, TData>> _itemList) : this() { 
            AddItemRange(_itemList);
        }

        ~OrderedLUT() { lookupTable.Clear(); }

        #region Add
        public void AddItem(LUTItem<float, TData> _pair) {
            lookupTable.Add(_pair);
            RefreshOrderByKeys();
        }

        public void AddItem(float _weight, TData _data) {
            AddItem(new LUTItem<float, TData>(_weight, _data));
        }

        public void AddItem((float, TData) _pair) {
            AddItem(_pair.Item1, _pair.Item2);
        }

        public void AddItem(KeyValuePair<float, TData> _pair) {
            AddItem(_pair.Key, _pair.Value);
        }
        #endregion

        #region AddRange
        public void AddItemRange(List<LUTItem<float, TData>> _itemList) {
            lookupTable.AddRange(_itemList);
            RefreshOrderByKeys();
        }

        public void AddItemRange(List<(float, TData)> _itemList) {
            for(int i = 0; i < Mathf.Min(_itemList.Count, _itemList.Count);i++) {
                lookupTable.Add(new(_itemList[i]));
            }
            RefreshOrderByKeys();
        }

        public void AddItemRange(List<float> _keyList, List<TData> _dataList) {
            if (_dataList.Count != _keyList.Count) { Debug.LogError("Weight list is not the same length as the data list!"); return; }

            for(int i = 0; i < Mathf.Min(_keyList.Count, _dataList.Count);i++) {
                lookupTable.Add(new(_keyList[i], _dataList[i]));
            }
            RefreshOrderByKeys();
        }
        #endregion

        public LUTItem<float, TData> GetItem(int _index) {
            if (_index < 0 || _index >= Count) { return default; }
            return lookupTable[_index];
        }

        public float GetItemKey(int _index) {
            if (_index < 0 || _index >= Count) { return 0.0f; }
            return lookupTable[_index].Key;
        }

        public TData GetItemData(int _index) {
            if (_index < 0 || _index >= Count) { return default; }
            return lookupTable[_index].Data;
        }

        public void RemoveAt(int _index) { lookupTable.RemoveAt(_index); }

        public void Clear() { lookupTable.Clear(); }

        private void RefreshOrderByKeys() {
            lookupTable.Sort((x, y) => x.Key.CompareTo(y.Key));
        }

        #region Searching
        protected (int, int) BinarySearch(float _value, int _min, int _max, Func<int, float> _getValueDelegate) {
            int mid = (_min + _max)/2;

            if (_value >= _getValueDelegate(mid) && _value < _getValueDelegate(mid+1)) { return (mid, mid+1); }
            else if (_value < _getValueDelegate(mid)) { return BinarySearch(_value, _min, mid, _getValueDelegate); }
            else if (_value >= _getValueDelegate(mid+1)) { return BinarySearch(_value, mid+1, _max, _getValueDelegate); }

            return (-1, Count);
        }

        protected int NearestSearch(float _value, Func<int, float> _getValueDelegate) {
            (int, int) itemIndexes = BinarySearch(_value, 0, Count, _getValueDelegate);
            
            float distToFloor = Mathf.Abs(_value - _getValueDelegate(itemIndexes.Item1));
            float distToCeil = Mathf.Abs(_getValueDelegate(itemIndexes.Item2) - _value);

            return distToFloor < distToCeil ? itemIndexes.Item1 : itemIndexes.Item2;
        }

        protected float SearchForLerp(float _value, Func<int, float> _getValueDelegate, out (int, int) _lerpAnchorIndexes) {
            _lerpAnchorIndexes = BinarySearch(_value, 0, Count, _getValueDelegate);

            float floorValue = _getValueDelegate(_lerpAnchorIndexes.Item1);
            float ceilValue = _getValueDelegate(_lerpAnchorIndexes.Item2);

            return Mathf.InverseLerp(floorValue, ceilValue, _value);
        }
        #endregion

        #region Data Searching
        protected (TData, TData) SearchForData(float _key) {
            if (_key <= MinKey) { return (default, GetItemData(0)); }
            else if (_key >= MaxKey) { return (GetItemData(Count-1), default);}

            (int, int) itemIndexes = BinarySearch(_key, 0, Count, GetItemKey);
            return (GetItemData(itemIndexes.Item1), GetItemData(itemIndexes.Item2));
        }

        public TData SearchForFloorData(float _key) { return SearchForData(_key).Item1; }

        public TData SearchForCeilData(float _key) { return SearchForData(_key).Item2; }

        public TData SearchForNearestData(float _key) {
            return GetItemData(NearestSearch(_key, GetItemKey));
        }

        public float SearchForLerpData(float _key, out (TData, TData) _lerpAnchors) {
            float lerpValue = SearchForLerp(_key, GetItemKey, out (int, int) lerpIndexes);
            _lerpAnchors = (GetItemData(lerpIndexes.Item1), GetItemData(lerpIndexes.Item2));
            return lerpValue;
        }
        #endregion

        #region IEnumerable
        public IEnumerator GetEnumerator() { return lookupTable.GetEnumerator(); }
        #endregion

        #region ISerializationCallbackReceiver
        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize() { RefreshOrderByKeys(); }
        #endregion
    }
}