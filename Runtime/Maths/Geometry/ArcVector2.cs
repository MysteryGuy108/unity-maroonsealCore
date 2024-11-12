using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Core.Maths.Geometry {
    public struct ArcVector2
    {
        public Vector2 Centre { get; private set; }
        public float Radius { get; private set; }

        public float StartTheta { get; private set; }
        public float EndTheta { get; private set; }
    }
}