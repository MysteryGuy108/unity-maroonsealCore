using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Core.Maths.Geometry {
    public abstract class IntersectionCalculator
    {
        //https://stackoverflow.com/questions/23016676/line-segment-and-circle-intersection
        static public List<Vector2> LineIntersectionWithCircle(Vector2 _ls, Vector2 _le, Vector2 _c, float _r) {

            float dx, dy, A, B, C, det;

            dx = _le.x - _ls.x;
            dy = _le.y - _ls.y;

            A = dx * dx + dy * dy;
            B = 2 * (dx * (_ls.x - _c.x) + dy * (_ls.y - _c.y));
            C = (_ls.x - _c.x) * (_ls.x - _c.x) + (_ls.y - _c.y) * (_ls.y - _c.y) - _r * _r;

            det = B * B - 4 * A * C;
            if ((A <= 0.0000001) || (det < 0)) { // No real solutions.
                return null;
            }
            else if (det == 0)  { // One solution.
                float t = -B / (2 * A);
                Vector2 point = new Vector2(_ls.x + t * dx, _ls.x + t * dy);
                if (IsPointInLineBounds(point, _ls, _le)) { return new List<Vector2>(1) { point }; }
                else { return null; }
            }
            else { // Two solutions.
                float t1 = (-B + Mathf.Sqrt(det)) / (2 * A);
                float t2 = (-B - Mathf.Sqrt(det)) / (2 * A);

                List<Vector2> points = new List<Vector2>();

                Vector2 point1 = new Vector2(_ls.x + t1 * dx, _ls.y + t1 * dy);
                if (IsPointInLineBounds(point1, _ls, _le)) { points.Add(point1); }

                Vector2 point2 = new Vector2(_ls.x + t2 * dx, _ls.y + t2 * dy);
                if (IsPointInLineBounds(point2, _ls, _le)) { points.Add(point2); }

                return points.Count <= 0 ? null : points;
            }
        }

        static public List<Vector2> LineIntersectionWithCapsule(Vector2 _ls, Vector2 _le, Vector2 _cs, Vector2 _ce, float _r) {
            List<Vector2> intersectPoints = new List<Vector2>();

            Vector2 cDelta = (_ce - _cs).normalized;

            // Start circle points
            List<Vector2> circlePoints = LineIntersectionWithCircle(_ls, _le, _cs, _r);

            if (circlePoints != null) { 
                for(int i = circlePoints.Count-1; i >= 0; i--) {
                    Vector2 circleDirection = (circlePoints[i] - _cs).normalized;
                    if (Vector2.Dot(circleDirection, cDelta) >= 0.0f) {
                        circlePoints.RemoveAt(i);
                    }
                }

                intersectPoints.AddRange(circlePoints); 
            }
            
            // End circle points
            circlePoints = LineIntersectionWithCircle(_ls, _le, _ce, _r);

            if (circlePoints != null) { 
                for(int i = circlePoints.Count-1; i >= 0; i--) {
                    Vector2 pDir = (circlePoints[i] - _ce).normalized;

                    if (Vector2.Dot(pDir, cDelta) < 0.0f) {
                        circlePoints.RemoveAt(i);
                    }
                }
                intersectPoints.AddRange(circlePoints); 
            }

            // Connecting Lines
            Vector2 tangent = Vector3.Cross(_ce-_cs, Vector3.forward).normalized;

            Vector2 cl1s =  _cs + tangent * _r;
            Vector2 cl1e =  _ce + tangent * _r;

            Vector2 lineIntersectPoint = LineIntersectionWithLine(_ls, _le, cl1s, cl1e);
            if (lineIntersectPoint.magnitude != Mathf.Infinity) { intersectPoints.Add(lineIntersectPoint); }

            Vector2 cl2s =  _cs - tangent * _r;
            Vector2 cl2e =  _ce - tangent * _r;

            lineIntersectPoint = LineIntersectionWithLine(_ls, _le, cl2s, cl2e);
            if (lineIntersectPoint.magnitude != Mathf.Infinity) { intersectPoints.Add(lineIntersectPoint); }

            return intersectPoints.Count == 0 ? null : intersectPoints;
        }

        //https://rosettacode.org/wiki/Find_the_intersection_of_two_lines#C#
        static public Vector2 LineIntersectionWithLine(Vector2 _l1s, Vector2 _l1e, Vector2 _l2s, Vector2 _l2e) {
            float a1 = _l1e.y - _l1s.y;
            float b1 = _l1s.x - _l1e.x;
            float c1 = a1 * _l1s.x + b1 * _l1s.y;

            float a2 = _l2e.y - _l2s.y;
            float b2 = _l2s.x - _l2e.x;
            float c2 = a2 * _l2s.x + b2 * _l2s.y;

            float delta = a1 * b2 - a2 * b1;

            if (delta == 0.0f) { return Vector2.one * Mathf.Infinity; }

            Vector2 intersectPoint = new Vector2((b2 * c1 - b1 * c2) / delta, (a1 * c2 - a2 * c1) / delta);

            if (!IsPointInLineBounds(intersectPoint, _l1s, _l1e) ||
                !IsPointInLineBounds(intersectPoint, _l2s, _l2e)) {
                return Vector2.one * Mathf.Infinity;
            }

            return intersectPoint;
        }
    
        static public bool IsPointInLineBounds(Vector2 _p, Vector2 _ls, Vector2 _le) {
            float minX, maxX;
            if (_ls.x <= _le.x) { minX = _ls.x; maxX = _le.x; }
            else { minX = _le.x; maxX = _ls.x; }

            float minY, maxY;
            if (_ls.y <= _le.y) { minY = _ls.y; maxY = _le.y; }
            else { minY = _le.y; maxY = _ls.y; }

            return _p.x >= minX && _p.x <= maxX && 
                _p.y >= minY && _p.y <= maxY;
        }
    }
}

