using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Packages.Maths.Geometry {
    public struct CircleVector2
    {
        public Vector2 Centre {get; private set;}
        public float Radius {get; private set;}

        public CircleVector2(Vector2 _centre, float _radius) {
            Centre = _centre;
            Radius = _radius;
        }

        public bool IsPointInCircle(Vector2 _point) {
            return Vector2.Distance(_point, Centre) < Radius;
        }
    }
}