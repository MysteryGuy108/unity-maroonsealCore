using UnityEngine;

namespace MaroonSeal.Maths.Geometry {
    public static class GeometryGizmos3D
    {
        public static void DrawLine(Line3D _line, float _pointRadii = 0.03125f) {
            Gizmos.DrawLine(_line.pointA, _line.pointB);
            Gizmos.DrawSphere(_line.pointA, _pointRadii);
            Gizmos.DrawSphere(_line.pointB, _pointRadii);
        }

        public static void DrawCubicBezier(CubicBezier3D _bezier, int _resolution = 32, float _pointRadii = 0.03125f) {
            DrawWireShape(_bezier, _resolution);

            Gizmos.DrawLine(_bezier.anchorA, _bezier.controlA);
            Gizmos.DrawSphere(_bezier.anchorA, _pointRadii);
            Gizmos.DrawSphere(_bezier.controlA, _pointRadii);
            
            Gizmos.DrawLine(_bezier.anchorB, _bezier.controlB);
            Gizmos.DrawSphere(_bezier.controlB, _pointRadii);
            Gizmos.DrawSphere(_bezier.anchorB, _pointRadii);
        }

        private static void DrawWireShape(IWireShape3D _shape, int _resolution = 32) {
            float timeStep = 1.0f / (_resolution-1);
            Vector3 prevPoint = _shape.LerpAlongPerimeter(0.0f);
            for(int i = 1; i < _resolution; i++) {
                Vector3 currentPoint = _shape.LerpAlongPerimeter(i * timeStep);
                Gizmos.DrawLine(prevPoint, currentPoint);
                prevPoint = currentPoint;
            }
        }
    }
}