using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MaroonSeal.Serializing;

namespace MaroonSeal.Maths.Interpolation {
    [System.Serializable]
    public class CompositeLerpPath<TPath> : SerializableListBase<TPath>, ILerpPath where TPath : ILerpPath { 

        #region ILerpPath
        public Vector3 LerpAlongPath(float _time) {
            if (Count <= 0) { return Vector2.zero; }
            _time *= this.Count;
            int index = (int)Mathf.Clamp(_time, 0, this.Count-1);
            return this[index].LerpAlongPath(_time - index);
        }
        #endregion
    }

    [System.Serializable]
    public class CompositeLerpPath : CompositeLerpPath<ILerpPath> {}

    [System.Serializable]
    public class CompositeLerpPath2D<TPath> : CompositeLerpPath<TPath>, ILerpPath2D where TPath : ILerpPath2D {
        #region ILerpPath2D
        new public Vector2 LerpAlongPath(float _time) {
            if (Count <= 0) { return Vector2.zero; }
            _time *= this.Count;
            int index = (int)Mathf.Clamp(_time, 0, this.Count-1);
            return this[index].LerpAlongPath(_time - index);
        }

        Vector3 ILerpPath.LerpAlongPath(float _time) { return base.LerpAlongPath(_time); }
        #endregion
    }

    [System.Serializable]
    public class CompositeLerpPath2D : CompositeLerpPath2D<ILerpPath2D> {}
}

