using UnityEngine;

using MaroonSeal.Maths.Interpolation;

namespace MaroonSeal.Maths {
    public static class ShapeGizmos
    {
        #region 2D
        public static void DrawLine2D(Line2D _line, float _pointRadii = 0.03125f) {
            Gizmos.DrawLine(_line.pointA, _line.pointB);
            Gizmos.DrawSphere(_line.pointA, _pointRadii);
            Gizmos.DrawSphere(_line.pointB, _pointRadii);
        }

        public static void DrawTriangle2D(Triangle2D _triangle, float _pointRadii = 0.03125f) {
            DrawPolygon2D(_triangle, _pointRadii);
        }

        public static void DrawBox2D(Box2D _box, float _pointRadii = 0.03125f) {
            DrawPolygon2D(_box, _pointRadii);
        }

        public static void DrawPolygon2D(IPolygonShape2D _polygon, float _pointRadii = 0.03125f) {
            Line2D[] edges = _polygon.GetEdges();
            foreach(Line2D edge in edges) { 
                DrawLine2D(edge, _pointRadii);
            }
        }

        public static void DrawCircle2D(Circle2D _circle, int _resolution = 32, float _pointRadii = 0.03125f) {
            //DrawLerpPath(_circle, _resolution);
            Gizmos.DrawSphere(_circle.GetPositionAtTime(0.0f), _pointRadii);
        }

        public static void DrawArc2D(Arc2D _arc, int _resolution = 32, float _pointRadii = 0.03125f) {
            //DrawLerpPath(_arc, _resolution);
            Gizmos.DrawSphere(_arc.GetStartPoint(), _pointRadii);
            Gizmos.DrawSphere(_arc.GetEndPoint(), _pointRadii);
        }

        public static void DrawCubicBezier2D(CubicBezier2D _bezier, int _resolution = 32, float _pointRadii = 0.03125f) {
            //DrawLerpPath(_bezier, _resolution);

            Gizmos.DrawLine(_bezier.anchorA, _bezier.controlA);
            Gizmos.DrawSphere(_bezier.anchorA, _pointRadii);
            Gizmos.DrawSphere(_bezier.controlA, _pointRadii);
            
            Gizmos.DrawLine(_bezier.anchorB, _bezier.controlB);
            Gizmos.DrawSphere(_bezier.controlB, _pointRadii);
            Gizmos.DrawSphere(_bezier.anchorB, _pointRadii);
        }
        #endregion

        #region 3D
        public static void DrawLine(Line _line, float _pointRadii = 0.03125f) {
            Gizmos.DrawLine(_line.pointA, _line.pointB);
            Gizmos.DrawSphere(_line.pointA, _pointRadii);
            Gizmos.DrawSphere(_line.pointB, _pointRadii);
        }

        public static void DrawTriangle(Triangle _triangle, float _pointRadii = 0.03125f) {
            DrawPolygon3D(_triangle, _pointRadii);
        }

        public static void DrawBox(Box _box, float _pointRadii = 0.03125f) {
            DrawPolygon3D(_box, _pointRadii);
        }

        public static void DrawPolygon3D(IPolygonShape _polygon, float _pointRadii = 0.03125f) {
            Line[] edges = _polygon.GetEdges();
            foreach(Line edge in edges) { 
                DrawLine(edge, _pointRadii);
            }
        }

        public static void DrawSphere(Sphere _sphere) {
            Gizmos.DrawSphere(_sphere.centre, _sphere.radius);
        }

        public static void DrawCubicBezier(CubicBezier _bezier, int _resolution = 32, float _pointRadii = 0.03125f) {
            //DrawLerpPath(_bezier, _resolution);

            Gizmos.DrawLine(_bezier.anchorA, _bezier.controlA);
            Gizmos.DrawSphere(_bezier.anchorA, _pointRadii);
            Gizmos.DrawSphere(_bezier.controlA, _pointRadii);
            
            Gizmos.DrawLine(_bezier.anchorB, _bezier.controlB);
            Gizmos.DrawSphere(_bezier.controlB, _pointRadii);
            Gizmos.DrawSphere(_bezier.anchorB, _pointRadii);
        }
        #endregion

        /*
        #region Drawers
        public static void DrawLerpPath(ILerpPath _shape, int _resolution = 32) {
            float timeStep = 1.0f / (_resolution-1);
            Vector3 prevPoint = _shape.GetPositionAtTime(0.0f);
            for(int i = 1; i < _resolution; i++) {
                Vector3 currentPoint = _shape.GetPositionAtTime(i * timeStep);
                Gizmos.DrawLine(prevPoint, currentPoint);
                prevPoint = currentPoint;
            }
        }
        #endregion
        */
    }
}