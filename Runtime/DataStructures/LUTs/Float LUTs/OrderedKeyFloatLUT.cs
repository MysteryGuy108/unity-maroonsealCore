using UnityEngine;

using System.Collections.Generic;
using System.Linq;

using MaroonSeal.Serializing;

namespace MaroonSeal.DataStructures {
    [System.Serializable]
    public class OrderedKeyFloatLUT<TData> : FloatLUT<TData>
    {
        #region SerializableDictionaryBase
        protected override List<SerializableKeyValuePair<float, TData>> ValidateItems(List<SerializableKeyValuePair<float, TData>> _list) {
            return _list.OrderBy(x => x.Key).ToList();;
        }
        #endregion
    }
}