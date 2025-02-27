using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using MaroonSeal.Serializing;

namespace MaroonSeal.DataStructures {
    [System.Serializable]
    public class OrderedValueFloatLUT : NormalisedFloatLUT<float>
    {
        #region SerializableDictionaryBase
        protected override List<SerializableKeyValuePair<float, float>> ValidateItems(List<SerializableKeyValuePair<float, float>> _list) {
            List<SerializableKeyValuePair<float, float>> temp = _list;
            _list = _list.OrderBy(x => x.Value).ToList();
            temp.Clear();
            return base.ValidateItems(_list);
        }
        #endregion
    }
}