using UnityEngine;

namespace MaroonSeal.Maths.Geometry {
    [System.Serializable]
    public struct Box2D : IPolygon2D, ISDFShape2D
    {
        public Vector2 centre;
        public Vector2 size;

        #region Constructors and Operators
        public Box2D(Vector2 _centre, Vector2 _size) {
            centre = _centre;
            size = _size;
        }
        
        public static bool operator == (Box2D _a, Box2D _b) {
            return _a.centre == _b.centre && _a.size == _b.size;
        }

        public static bool operator != (Box2D _a, Box2D _b) {
            return !(_a.centre == _b.centre && _a.size == _b.size);
        }
    
        readonly public override bool Equals(object obj) {
            if (obj == null) { return false; }
            if (obj is not Box2D) { return false; }
            return (Box2D)obj == this;
        }
        readonly public override int GetHashCode() { return System.HashCode.Combine(centre, size); }
        #endregion

        #region IWireShape2D
        readonly public Vector2 LerpAlongPerimeter(float _time) {
            Line2D[] edges = GetEdges();
            _time *= 4;
            int segment = (int)Mathf.Clamp(_time, 0, 3);
            return edges[segment].LerpAlongPerimeter(_time - segment);
        }
        #endregion

        #region IPolygon2D
        readonly public int GetVertexCount() { return 4; }

        readonly public Vector2[] GetVertices() {
            Vector2 halfSize = size / 2.0f;

            return new Vector2[4] {
                new Vector2(-halfSize.x, -halfSize.y) + centre,
                new Vector2(halfSize.x, -halfSize.y) + centre,
                new Vector2(halfSize.x, halfSize.y) + centre,
                new Vector2(-halfSize.x, halfSize.y) + centre
            };
        }

        readonly public Line2D[] GetEdges() {
            Vector2[] vertices = GetVertices();
            return new Line2D[4] {
                new(vertices[0], vertices[1]),
                new(vertices[1], vertices[2]),
                new(vertices[2], vertices[3]),
                new(vertices[3], vertices[0])
            };
        }
        #endregion

        #region ISDFShape3D
        readonly public float GetSignedDistance(Vector2 _point) {
            _point = new(Mathf.Abs(_point.x), Mathf.Abs(_point.y));
            Vector2 d = _point-size;
            Vector2 max = new(Mathf.Max(d.x, 0.0f), Mathf.Max(d.y, 0.0f));
            return max.magnitude + Mathf.Min(Mathf.Max(d.x,d.y), 0.0f);
        }
        #endregion

        readonly public bool IsPointInBounds(Vector2 _point) {
            _point -= centre;
            Vector2 halfSize = size / 2.0f;
            return _point.x >= -halfSize.x && _point.x <= halfSize.x &&
                _point.y >= -halfSize.y && _point.y <= halfSize.y;
        }
    }
}