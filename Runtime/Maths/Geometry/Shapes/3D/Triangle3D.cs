using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Maths {
    [System.Serializable]
    public struct Triangle3D : IPolygonShape3D, IVector3Interpolatable
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

        public static explicit operator Triangle2D(Triangle3D _current) {
            return new(_current.pointA, _current.pointB, _current.pointC);
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

        #region IPolygon3D
        public readonly int VertexCount => 3;
        readonly public Vector3[] GetVertices() {
            return new Vector3[3] { pointA, pointB, pointC };
        }

        readonly public Line3D[] GetEdges() {
            return new Line3D[3] { GetEdgeAB(), GetEdgeBC(), GetEdgeCA() };
        }
        #endregion

        #region IInterpolatable
        public readonly Vector3 InterpolateVector3(float _time) {
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

        readonly public float GetLengthAB() { return Vector3.Distance(pointA, pointB); }
        readonly public float GetLengthBC() { return Vector3.Distance(pointB, pointC); }
        readonly public float GetLengthCA() { return Vector3.Distance(pointC, pointA); }

        readonly public Line3D GetEdgeAB() { return new Line3D(pointA, pointB); }
        readonly public Line3D GetEdgeBC() { return new Line3D(pointB, pointC); }
        readonly public Line3D GetEdgeCA() { return new Line3D(pointC, pointA); }

        readonly public Vector3 GetCentroid() { return (pointA + pointB + pointC) / 3.0f; }

        readonly public Sphere3D GetCircumsphere() {
            Vector3 ac = pointC - pointA;
            Vector3 ab = pointB - pointA;
            Vector3 abXac = Vector3.Cross(ab, ac);

            // this is the vector from a TO the circumsphere center
            Vector3 toCircumsphereCenter = (Vector3.Cross(abXac, ab)*ac.sqrMagnitude + Vector3.Cross(ac, abXac )*ab.sqrMagnitude) / (2.0f*abXac.sqrMagnitude) ;
            float circumsphereRadius = toCircumsphereCenter.magnitude;

            // The 3 space coords of the circumsphere center then:
            Vector3 ccs = pointA  +  toCircumsphereCenter; // now this is the actual 3space location
            return new(ccs, circumsphereRadius);
        }
    }
}

