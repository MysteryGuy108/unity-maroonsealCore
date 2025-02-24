using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Maths.Geometry {
    public struct Triangle3D : IWireShape3D
    {
        public Vector3 pointA;
        public Vector3 pointB;
        public Vector3 pointC;

        #region Constructors and Operators
        public Triangle3D(Vector3 _point1, Vector3 _point2, Vector3 _point3) {
            pointA = _point1;
            pointB = _point2;
            pointC = _point3;
        }
        
        public static bool operator == (Triangle3D _a, Triangle3D _b) {
            return _a.pointA == _b.pointA && _a.pointB == _b.pointB && _a.pointC == _b.pointC;
        }

        public static bool operator != (Triangle3D _a, Triangle3D _b) {
            return !(_a.pointA == _b.pointA && _a.pointB == _b.pointB && _a.pointC == _b.pointC);
        }
    
        readonly public override bool Equals(object obj) {
            if (obj == null) { return false; }
            if (obj is not Triangle3D) { return false; }
            return (Triangle3D)obj == this;
        }
        readonly public override int GetHashCode() { return System.HashCode.Combine(pointA, pointB, pointC); }
        #endregion

        #region IShape2D
        public readonly Vector3 LerpAlongPerimeter(float _time) {
            float totalTime = _time * 3.0f;
            if (totalTime <= 1.0f) {
                return Vector3.Lerp(pointA, pointB, totalTime);
            }
            else if (totalTime > 1.0f && totalTime <= 2.0f) {
                return Vector3.Lerp(pointB, pointC, totalTime-1.0f);
            }
            
            return Vector3.Lerp(pointC, pointA, totalTime-2.0f);
        }
        #endregion

        readonly public bool ContainsPoint(Vector3 _point) { return _point == pointA || _point == pointB || _point == pointC; }

        readonly public float GetLengthAB() { return Vector2.Distance(pointA, pointB); }
        readonly public float GetLengthBC() { return Vector2.Distance(pointB, pointC); }
        readonly public float GetLengthCA() { return Vector2.Distance(pointC, pointA); }

        readonly public Sphere3D GetCircumsphere() {
            throw new NotImplementedException();
            /*
            float d = 2 * (pointA.x * (pointB.y - pointC.y) + pointB.x * (pointC.y - pointA.y) + pointC.x * (pointA.y - pointB.y));

            float ux = ((pointA.x * pointA.x + pointA.y * pointA.y) * (pointB.y - pointC.y) + (pointB.x * pointB.x + pointB.y * pointB.y) * (pointC.y - pointA.y) + (pointC.x * pointC.x + pointC.y * pointC.y) * (pointA.y - pointB.y)) / d;
            float uy = ((pointA.x * pointA.x + pointA.y * pointA.y) * (pointC.x - pointB.x) + (pointB.x * pointB.x + pointB.y * pointB.y) * (pointA.x - pointC.x) + (pointC.x * pointC.x + pointC.y * pointC.y) * (pointB.x - pointA.x)) / d;

            Vector2 circumCentre = new(ux, uy);
            float circumRadius = Vector2.Distance(pointA, circumCentre);

            return new Sphere3D(circumCentre, circumRadius);
            */
        }
    }
}

