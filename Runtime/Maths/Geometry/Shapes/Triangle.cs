using UnityEngine;

using MaroonSeal.Maths.SDFs;

namespace MaroonSeal.Maths.Shapes {
    [System.Serializable]
    public struct Triangle : IShape3D, IPolygon, ISDFShape
    {
        public Vector3 pointA;
        public Vector3 pointB;
        public Vector3 pointC;

        readonly public PointTransform Transform => PointTransform.Origin;

        #region Constructors
        public Triangle(Vector3 _point1, Vector3 _point2, Vector3 _point3) {
            pointA = _point1;
            pointB = _point2;
            pointC = _point3;
        }
        #endregion

        #region Operators
        public static bool operator == (Triangle _a, Triangle _b) {
            return _a.pointA == _b.pointA && _a.pointB == _b.pointB && _a.pointC == _b.pointC;
        }

        public static bool operator != (Triangle _a, Triangle _b) {
            return !(_a.pointA == _b.pointA && _a.pointB == _b.pointB && _a.pointC == _b.pointC);
        }
    
        readonly public override bool Equals(object obj) {
            return ((Triangle)obj == this) && obj != null && obj is Triangle;
        }
        readonly public override int GetHashCode() { return System.HashCode.Combine(pointA, pointB, pointC); }
        #endregion

        #region Triangle
        readonly public bool ContainsPoint(Vector3 _point) { return _point == pointA || _point == pointB || _point == pointC; }

        readonly public float GetLengthAB() { return Vector3.Distance(pointA, pointB); }
        readonly public float GetLengthBC() { return Vector3.Distance(pointB, pointC); }
        readonly public float GetLengthCA() { return Vector3.Distance(pointC, pointA); }

        readonly public Line GetEdgeAB() { return new Line(pointA, pointB); }
        readonly public Line GetEdgeBC() { return new Line(pointB, pointC); }
        readonly public Line GetEdgeCA() { return new Line(pointC, pointA); }

        readonly public Vector3 GetNormal() {
            Vector3 deltaA = pointB - pointA;
            Vector3 deltaB = pointC - pointA;
            return Vector3.Cross(deltaA, deltaB).normalized;
        }

        readonly public Vector3 GetCentroid() { return (pointA + pointB + pointC) / 3.0f; }

        readonly public Sphere GetCircumsphere() {
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

        readonly public Circle GetCircumcircle() {
            float d = 2 * (pointA.x * (pointB.y - pointC.y) + pointB.x * (pointC.y - pointA.y) + pointC.x * (pointA.y - pointB.y));

            float ux = ((pointA.x * pointA.x + pointA.y * pointA.y) * (pointB.y - pointC.y) + (pointB.x * pointB.x + pointB.y * pointB.y) * (pointC.y - pointA.y) + (pointC.x * pointC.x + pointC.y * pointC.y) * (pointA.y - pointB.y)) / d;
            float uy = ((pointA.x * pointA.x + pointA.y * pointA.y) * (pointC.x - pointB.x) + (pointB.x * pointB.x + pointB.y * pointB.y) * (pointA.x - pointC.x) + (pointC.x * pointC.x + pointC.y * pointC.y) * (pointB.x - pointA.x)) / d;

            Vector2 circumCentre = new(ux, uy);
            float circumRadius = Vector2.Distance(pointA, circumCentre);

            PointTransform circumcircleTransform = new(circumCentre, Quaternion.FromToRotation(Vector3.up, GetNormal()), Vector3.one);
            return new Circle(circumcircleTransform, circumRadius);
        }
        #endregion

        #region IPolygon3D
        public readonly int VertexCount => 3;
        readonly public Vector3[] GetVertices() {
            return new Vector3[3] { pointA, pointB, pointC };
        }

        readonly public Line[] GetEdges() {
            return new Line[3] { GetEdgeAB(), GetEdgeBC(), GetEdgeCA() };
        }
        #endregion

        #region ISDFShape
        public float GetSignedDistance(Vector3 _point) {
            throw new System.NotImplementedException();
        }
        #endregion

        static public Vector3 GetNormal(Vector3 _p0, Vector3 _p1, Vector3 _p2) {
            Vector3 deltaA = _p1 - _p0;
            Vector3 deltaB = _p2 - _p0;
            return Vector3.Cross(deltaA, deltaB).normalized;
        }
    }
}
