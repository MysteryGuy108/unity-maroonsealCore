using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Packages.Maths.Geometry {
    public struct TriangleVector2
    {
        public Vector2 PointA { get; private set; }
        public Vector2 PointB { get; private set; }
        public Vector2 PointC { get; private set; }

        public float EdgeAB { get { return Vector2.Distance(PointA, PointB); }}
        public float EdgeBC { get { return Vector2.Distance(PointB, PointC); }}
        public float EdgeCA { get { return Vector2.Distance(PointC, PointA); }}

        public TriangleVector2(Vector2 _point1, Vector2 _point2, Vector2 _point3) {
            PointA = _point1;
            PointB = _point2;
            PointC = _point3;
        }
        
        public static bool operator == (TriangleVector2 _a, TriangleVector2 _b) {
            return _a.PointA == _b.PointA && _a.PointB == _b.PointB && _a.PointC == _b.PointC;
        }

        public static bool operator != (TriangleVector2 _a, TriangleVector2 _b) {
            return !(_a.PointA == _b.PointA && _a.PointB == _b.PointB && _a.PointC == _b.PointC);
        }
    
        public bool HasVertex(Vector2 _vertex) {
            return _vertex == PointA || _vertex == PointB || _vertex == PointC;
        }

        public CircleVector2 CalculateCircumcircle() {
            float d = 2 * (PointA.x * (PointB.y - PointC.y) + PointB.x * (PointC.y - PointA.y) + PointC.x * (PointA.y - PointB.y));

            float ux = ((PointA.x * PointA.x + PointA.y * PointA.y) * (PointB.y - PointC.y) + (PointB.x * PointB.x + PointB.y * PointB.y) * (PointC.y - PointA.y) + (PointC.x * PointC.x + PointC.y * PointC.y) * (PointA.y - PointB.y)) / d;
            float uy = ((PointA.x * PointA.x + PointA.y * PointA.y) * (PointC.x - PointB.x) + (PointB.x * PointB.x + PointB.y * PointB.y) * (PointA.x - PointC.x) + (PointC.x * PointC.x + PointC.y * PointC.y) * (PointB.x - PointA.x)) / d;

            Vector2 circumCentre = new Vector2(ux, uy);
            float circumRadius = Vector2.Distance(PointA, circumCentre);

            return new CircleVector2(circumCentre, circumRadius);
        }
    }
}

