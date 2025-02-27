using UnityEngine;
using MaroonSeal.Serializing;
using System.Collections.Generic;
using System.Linq;

namespace MaroonSeal.DataStructures {
    [System.Serializable]
    public class NormalisedFloatLUT<TData> : OrderedKeyFloatLUT<TData>
    {
        #region SerializableDictionaryBase
        protected override List<SerializableKeyValuePair<float, TData>> ValidateItems(List<SerializableKeyValuePair<float, TData>> _list) {
            List<SerializableKeyValuePair<float, TData>> sortedList = new();

            for(int i = 0; i < _list.Count; i++) {
                sortedList.Add(new((float)i/(_list.Count-1), _list[i].Value));
            }

            return sortedList;
        }
        #endregion
    }
}