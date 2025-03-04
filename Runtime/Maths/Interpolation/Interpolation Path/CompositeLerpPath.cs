using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MaroonSeal.Serializing;

namespace MaroonSeal.Maths.Interpolation {
    [System.Serializable]
    public class CompositeLerpPath<TPath> : SerializableListBase<TPath>, ILerpPathVector3 where TPath : ILerpPathVector3 { 

        #region ILerpPath
        public Vector3 GetPositionAtTime(float _time) {
            if (Count <= 0) { return Vector2.zero; }
            _time *= this.Count;
            int index = (int)Mathf.Clamp(_time, 0, this.Count-1);
            return this[index].GetPositionAtTime(_time - index);
        }
        #endregion
    }

    [System.Serializable]
    public class CompositeLerpPath : CompositeLerpPath<ILerpPathVector3> {}

    [System.Serializable]
    public class CompositeLerpPath2D<TPath> : CompositeLerpPath<TPath>, ILerpPathVector2 where TPath : ILerpPathVector2 {
        #region ILerpPath2D
        new public Vector2 GetPositionAtTime(float _time) {
            if (Count <= 0) { return Vector2.zero; }
            _time *= this.Count;
            int index = (int)Mathf.Clamp(_time, 0, this.Count-1);
            return this[index].GetPositionAtTime(_time - index);
        }

        Vector3 ILerpPathVector3.GetPositionAtTime(float _time) { return base.GetPositionAtTime(_time); }
        #endregion
    }

    [System.Serializable]
    public class CompositeLerpPath2D : CompositeLerpPath2D<ILerpPathVector2> {}
}

