using UnityEngine;

namespace MaroonSeal.Maths.Geometry {
    public static class GeometryGizmos2D
    {
        public static void DrawLine(Line2D _line, float _pointRadii = 0.03125f) {
            Gizmos.DrawLine(_line.pointA, _line.pointB);
            Gizmos.DrawSphere(_line.pointA, _pointRadii);
            Gizmos.DrawSphere(_line.pointB, _pointRadii);
        }

        public static void DrawPolygon(IPolygon2D _polygon, float _pointRadii = 0.03125f) {
            DrawWireShape(_polygon, _polygon.GetVertexCount()+1);
            Vector2[] vertices = _polygon.GetVertices();
            foreach(Vector2 vertex in vertices) { Gizmos.DrawSphere(vertex, _pointRadii); }
        }

        public static void DrawCircle(Circle2D _circle, int _resolution = 32, float _pointRadii = 0.03125f) {
            DrawWireShape(_circle, _resolution);
            Gizmos.DrawSphere(_circle.LerpAlongPerimeter(0.0f), _pointRadii);
        }

        public static void DrawArc(Arc2D _arc, int _resolution = 32, float _pointRadii = 0.03125f) {
            DrawWireShape(_arc, _resolution);
            Gizmos.DrawSphere(_arc.LerpAlongPerimeter(0.0f), _pointRadii);
            Gizmos.DrawSphere(_arc.LerpAlongPerimeter(1.0f), _pointRadii);
        }

        public static void DrawCubicBezier(CubicBezier2D _bezier, int _resolution = 32, float _pointRadii = 0.03125f) {
            DrawWireShape(_bezier, _resolution);

            Gizmos.DrawLine(_bezier.anchorA, _bezier.controlA);
            Gizmos.DrawSphere(_bezier.anchorA, _pointRadii);
            Gizmos.DrawSphere(_bezier.controlA, _pointRadii);
            
            Gizmos.DrawLine(_bezier.anchorB, _bezier.controlB);
            Gizmos.DrawSphere(_bezier.controlB, _pointRadii);
            Gizmos.DrawSphere(_bezier.anchorB, _pointRadii);
        }

        private static void DrawWireShape(IWireShape2D _shape, int _resolution = 32) {
            float timeStep = 1.0f / (_resolution-1);
            Vector2 prevPoint = _shape.LerpAlongPerimeter(0.0f);
            for(int i = 1; i < _resolution; i++) {
                Vector2 currentPoint = _shape.LerpAlongPerimeter(i * timeStep);
                Gizmos.DrawLine(prevPoint, currentPoint);
                prevPoint = currentPoint;
            }
        }
    }
}