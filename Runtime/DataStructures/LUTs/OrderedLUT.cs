using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Core.DataStructures {
    [System.Serializable]
    public class OrderedLUT<TData> : IEnumerable, ISerializationCallbackReceiver{
        [SerializeField] private List<LUTElement<float, TData>> lookupTable;

        public TData this[int _index] {
            get { return lookupTable[_index].Data; }
        }

        public int Count { get { return lookupTable.Count; } }
        public float MaxDistance {
            get {
                return lookupTable.Count <= 0 ? 0.0f : lookupTable[^1].Key;
            }
        }

        #region Add
        public void Add(LUTElement<float, TData> _pair) {
            lookupTable.Add(_pair);
            RefreshOrderByDistance();
        }

        public void Add(float _distance, TData _data) {
            Add(new LUTElement<float, TData>(_distance, _data));
        }

        public void Add((float, TData) _pair) {
            Add(_pair.Item1, _pair.Item2);
        }

        public void Add(KeyValuePair<float, TData> _pair) {
            Add(_pair.Key, _pair.Value);
        }
        #endregion

        #region AddRange
        public void AddRange(List<LUTElement<float, TData>> _elements) {
            lookupTable.AddRange(_elements);
            RefreshOrderByDistance();
        }

        public void AddRange(List<(float, TData)> _elements) {
            for(int i = 0; i < Mathf.Min(_elements.Count, _elements.Count);i++) {
                lookupTable.Add(new(_elements[i]));
            }
            RefreshOrderByDistance();
        }

        public void AddRange(List<float> _distanceList, List<TData> _dataList) {
            if (_dataList.Count != _distanceList.Count) { Debug.LogError("distance list is not the same length as the data list!"); return; }

            for(int i = 0; i < Mathf.Min(_distanceList.Count, _dataList.Count);i++) {
                lookupTable.Add(new(_distanceList[i], _dataList[i]));
            }
            RefreshOrderByDistance();
        }
        #endregion

        public TData FindDataAtIndex(int _index) {
            if (_index < 0 || _index > Count - 1) { return default; }
            return lookupTable[_index].Data;
        }

        public void RemoveAt(int _index) { lookupTable.RemoveAt(_index); }

        public void Clear() {
            lookupTable.Clear();
        }

        private void RefreshOrderByDistance() {
            lookupTable.Sort((x, y) => x.Key.CompareTo(y.Key));
        }

        #region Binary Search
        public TData SearchForFloor(float _distance) {
            (int, int) elements = BinarySearch(_distance, 0, Count);
            return FindDataAtIndex(elements.Item1);
        }

        public TData SearchForCeil(float _distance) {
            (int, int) elements = BinarySearch(_distance, 0, Count);
            return FindDataAtIndex(elements.Item2);
        }

        public TData SearchForNearest(float _distance) {
            (int, int) elements = BinarySearch(_distance, 0, Count);
            TData floorData = FindDataAtIndex(elements.Item1);
            TData ceilData = FindDataAtIndex(elements.Item2);

            if (floorData.Equals(default) && ceilData.Equals(default)) { return default; }
            else if (floorData.Equals(default)) { return ceilData; }
            else if (ceilData.Equals(default)) { return floorData; }
            
            float distToFloor = Mathf.Abs(_distance - lookupTable[elements.Item1].Key);
            float distToCeil = Mathf.Abs(lookupTable[elements.Item2].Key - _distance);

            return distToFloor < distToCeil ? floorData : ceilData;
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
        public IEnumerator GetEnumerator() {
            return lookupTable.GetEnumerator();
        }
        #endregion

        #region ISerializationCallbackReceiver
        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize() { RefreshOrderByDistance(); }
        #endregion
    }
}