using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Core.DataStructures.LUTs {
    public class OrderedValuesLUT : OrderedLUT<float>
    {
        #region Constructors
        public OrderedValuesLUT(List<LUTItem<float, float>> _itemList) : base(_itemList) { }
        #endregion

        #region Data Search
        public float SearchForLerpData(float _key) {
            float lerpValue = SearchForLerpData(_key, out (float, float) lerpAnchors);

            if (lerpAnchors.Item1 <= Mathf.NegativeInfinity) { return lerpAnchors.Item2; }
            if (lerpAnchors.Item2 >= Mathf.Infinity) { return lerpAnchors.Item1; }

            return Mathf.Lerp(lerpAnchors.Item1, lerpAnchors.Item2, lerpValue);
        }
        #endregion

        #region Key Search
        protected (float, float) SearchForKey(float _data) {
            if (_data < MinData) { return (Mathf.NegativeInfinity, MinKey); }
            else if (_data > MaxData) { return (MaxKey, Mathf.Infinity);}

            (int, int) itemIndexes = BinarySearch(_data, 0, Count, GetItemData);
            return (GetItemKey(itemIndexes.Item1), GetItemKey(itemIndexes.Item2));
        }

        public float SearchForFloorKey(float _data) { return SearchForKey(_data).Item1; }

        public float SearchForCeilKey(float _data) { return SearchForKey(_data).Item2; }

        public float SearchForNearestKey(float _data) { return GetItemKey(NearestSearch(_data, GetItemData)); }

        public float SearchForLerpKey(float _data, out (float, float) _lerpAnchorsData) {
            float lerpValue = SearchForLerp(_data, GetItemData, out (int, int) lerpIndexes);
            _lerpAnchorsData = (GetItemKey(lerpIndexes.Item1), GetItemKey(lerpIndexes.Item2));
            return lerpValue;
        }

        public float SearchForLerpKey(float _data) {
            float lerpValue = SearchForLerpKey(_data, out (float, float) lerpAnchors);

            if (lerpAnchors.Item1 <= Mathf.NegativeInfinity) { return lerpAnchors.Item2; }
            if (lerpAnchors.Item2 >= Mathf.Infinity) { return lerpAnchors.Item1; }

            return Mathf.Lerp(lerpAnchors.Item1, lerpAnchors.Item2, lerpValue);
        }
        #endregion
    }
}