using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Core.DataStructures.LUTs {
    [System.Serializable]
    public class OrderedLUT<TData> : IEnumerable, ISerializationCallbackReceiver{
        [SerializeField] private List<LUTItem<float, TData>> lookupTable;

        public int Count { get { return lookupTable.Count; } }
        public float MaxDistance {
            get {
                return lookupTable.Count <= 0 ? 0.0f : lookupTable[^1].Key;
            }
        }

        public OrderedLUT() { lookupTable = new(); }
        ~OrderedLUT() { lookupTable.Clear(); }

        #region Add
        public void AddItem(LUTItem<float, TData> _pair) {
            lookupTable.Add(_pair);
            RefreshOrderByWeights();
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
        public void AddItemRange(List<LUTItem<float, TData>> _elements) {
            lookupTable.AddRange(_elements);
            RefreshOrderByWeights();
        }

        public void AddItemRange(List<(float, TData)> _elements) {
            for(int i = 0; i < Mathf.Min(_elements.Count, _elements.Count);i++) {
                lookupTable.Add(new(_elements[i]));
            }
            RefreshOrderByWeights();
        }

        public void AddItemRange(List<float> _weightList, List<TData> _dataList) {
            if (_dataList.Count != _weightList.Count) { Debug.LogError("Weight list is not the same length as the data list!"); return; }

            for(int i = 0; i < Mathf.Min(_weightList.Count, _dataList.Count);i++) {
                lookupTable.Add(new(_weightList[i], _dataList[i]));
            }
            RefreshOrderByWeights();
        }
        #endregion

        public LUTItem<float, TData> GetItem(int _index) {
            if (_index < 0 || _index > Count - 1) { return default; }
            return lookupTable[_index];
        }

        public float GetItemWeight(int _index) {
            if (_index < 0 || _index > Count - 1) { return default; }
            return lookupTable[_index].Key;
        }

        public TData GetItemData(int _index) {
            if (_index < 0 || _index > Count - 1) { return default; }
            return lookupTable[_index].Data;
        }

        public void RemoveAt(int _index) { lookupTable.RemoveAt(_index); }

        public void Clear() { lookupTable.Clear(); }

        private void RefreshOrderByWeights() {
            lookupTable.Sort((x, y) => x.Key.CompareTo(y.Key));
        }

        #region Binary Search
        public (TData, TData) Search(float _weight) {
            (int, int) itemIndexes = BinarySearch(_weight, 0, Count);
            return (GetItemData(itemIndexes.Item1), GetItemData(itemIndexes.Item2));
        }

        public TData SearchForFloor(float _weight) { return Search(_weight).Item1; }

        public TData SearchForCeil(float _weight) { return Search(_weight).Item2; }

        public TData SearchForNearest(float _weight) {
            (int, int) itemIndexes = BinarySearch(_weight, 0, Count);

            TData floorData = GetItemData(itemIndexes.Item1);
            TData ceilData = GetItemData(itemIndexes.Item2);

            if (floorData.Equals(default) && ceilData.Equals(default)) { return default; }
            else if (floorData.Equals(default)) { return ceilData; }
            else if (ceilData.Equals(default)) { return floorData; }
            
            float distToFloor = Mathf.Abs(_weight - lookupTable[itemIndexes.Item1].Key);
            float distToCeil = Mathf.Abs(lookupTable[itemIndexes.Item2].Key - _weight);

            return distToFloor < distToCeil ? floorData : ceilData;
        }

        public float SearchForLerp(float _weight, out (TData, TData) _lerpValues) {
            (int, int) itemIndexes = BinarySearch(_weight, 0, Count);
            _lerpValues = (GetItemData(itemIndexes.Item1), GetItemData(itemIndexes.Item2));
            float floorDistance = GetItemWeight(itemIndexes.Item1);
            float ceilDistance = GetItemWeight(itemIndexes.Item2);
            return Mathf.InverseLerp(floorDistance, ceilDistance, _weight);
        }

        private (int, int) BinarySearch(float _value, int _min, int _max) {
            if (_value < 0.0f) { return (0, 0); }
            else if (_value > MaxDistance) { return (Count-1, Count-1);}

            int mid = (_min + _max)/2;

            if (_value >= lookupTable[mid].Key && _value < lookupTable[mid+1].Key) { return (mid, mid+1); }
            else if (_value < lookupTable[mid].Key) { return BinarySearch(_value, _min, mid); }
            else if (_value >= lookupTable[mid+1].Key) { return BinarySearch(_value, mid+1, _max); }

            return (-1, Count);
        }
        #endregion

        #region IEnumerable
        public IEnumerator GetEnumerator() { return lookupTable.GetEnumerator(); }
        #endregion

        #region ISerializationCallbackReceiver
        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize() { RefreshOrderByWeights(); }
        #endregion
    }
}