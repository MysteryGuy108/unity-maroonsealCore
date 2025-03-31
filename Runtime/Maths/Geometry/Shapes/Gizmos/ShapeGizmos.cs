using UnityEngine;

namespace MaroonSeal.Maths.Shapes {
    public static class ShapeGizmos
    {
        public static void DrawLine(Line _line, float _pointRadii = 0.03125f) {
            Gizmos.DrawLine(_line.from, _line.to);
            Gizmos.DrawSphere(_line.from, _pointRadii);
            Gizmos.DrawSphere(_line.to, _pointRadii);
        }

        public static void DrawLine(Line2D _line, float _pointRadii = 0.03125f) {
            Gizmos.DrawLine(_line.from, _line.to);
            Gizmos.DrawSphere(_line.from, _pointRadii);
            Gizmos.DrawSphere(_line.to, _pointRadii);
        }

        public static void DrawTriangle(Triangle _triangle, float _pointRadii = 0.03125f) {
            DrawPolygonShape(_triangle, _pointRadii);
        }

        public static void DrawBox(Box _box) {
            Gizmos.DrawWireCube(_box.transform.position, _box.size);
        }

        public static void DrawCircle(Circle _circle, int _resolution = 32, float _pointSize = 0.125f) {
            DrawInterpolationShape(_circle, _resolution);
            ExtraGizmos.DrawCross(_circle.transform.position, _circle.radius * _pointSize * Vector2.one, _circle.transform.Rotation);
        }

        public static void DrawCircle(Circle2D _circle, int _resolution = 32, float _pointSize = 0.125f) {
            DrawInterpolationShape(_circle, _resolution);
            ExtraGizmos.DrawCross(_circle.transform.position, _circle.radius * _pointSize * Vector2.one, _circle.transform.Rotation);
        }

        public static void DrawArc(Arc _arc, int _resolution = 32, float _pointRadii = 0.03125f, float _pointSize = 0.125f) {
            DrawInterpolationShape(_arc, _resolution);
            ExtraGizmos.DrawCross(_arc.transform.position, _arc.radius * _pointSize * Vector2.one, _arc.transform.Rotation);

            Gizmos.DrawSphere(_arc.EvaluatePositionAtTime(0.0f), _pointRadii);
            Gizmos.DrawSphere(_arc.EvaluatePositionAtTime(1.0f), _pointRadii);
        }

        public static void DrawConicSection(ConicSection _conicSection, int _resolution = 32, float _pointSize = 0.125f) {
            DrawInterpolationShape(_conicSection, _resolution);
            (Vector3, Vector3) foci = _conicSection.GetFoci();
            
            Vector2 size = _conicSection.minimumFociRadius * _pointSize * Vector2.one;
            ExtraGizmos.DrawCross(foci.Item1, size, _conicSection.transform.Rotation);
            ExtraGizmos.DrawCross(foci.Item2, size, _conicSection.transform.Rotation);
        }

        public static void DrawSphere(Sphere _sphere, float _pointSize = 0.125f) {
            Gizmos.DrawSphere(_sphere.transform.position, _sphere.radius);
            ExtraGizmos.DrawCross(_sphere.transform.position, Vector3.one * _sphere.radius * _pointSize, _sphere.transform.Rotation);
        }

        public static void DrawCubicBezier(CubicBezier _bezier, int _resolution = 32, float _pointRadii = 0.03125f) {
            DrawInterpolationShape(_bezier, _resolution);

            Gizmos.DrawLine(_bezier.anchorA, _bezier.controlA);
            Gizmos.DrawSphere(_bezier.anchorA, _pointRadii);
            Gizmos.DrawSphere(_bezier.controlA, _pointRadii);
            
            Gizmos.DrawLine(_bezier.anchorB, _bezier.controlB);
            Gizmos.DrawSphere(_bezier.controlB, _pointRadii);
            Gizmos.DrawSphere(_bezier.anchorB, _pointRadii);
        }

        #region Interface Drawers
        public static void DrawPolygonShape(IPolygon _polygon, float _pointRadii = 0.03125f) {
            Line[] edges = _polygon.GetEdges();
            foreach(Line edge in edges) { 
                DrawLine(edge, _pointRadii);
            }
        }

        public static void DrawInterpolationShape(IInterpolationShape _shape, int _resolution = 32) {
            float timeStep = 1.0f / (_resolution-1);
            Vector3 prevPoint = _shape.EvaluatePositionAtTime(0.0f);
            for(int i = 1; i < _resolution; i++) {
                Vector3 currentPoint = _shape.EvaluatePositionAtTime(i * timeStep);
                Gizmos.DrawLine(prevPoint, currentPoint);
                prevPoint = currentPoint;
            }
        }
        #endregion
    }
}