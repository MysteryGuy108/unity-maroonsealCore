using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MaroonSeal.Maths.Shapes;

namespace MaroonSeal.DataStructures.NodeGraphs.Generators {
    public class DelaunayTriangulatorGraphCalculator
    {
        static public NodeGraphWeighted<Vector2> CalculateGraph(List<Vector2> _points, float _pointsSquareBounds) {

            NodeGraphWeighted<Vector2> graph = new();
            
            // Adding points to graph.
            graph.PushNodeRange(_points);

            List<Triangle> triangles = CaluclateTrianglesFromPoints(_points, _pointsSquareBounds);

            for(int i = 0; i < triangles.Count; i++) {
                
                int indexA = graph[triangles[i].pointA];
                int indexB = graph[triangles[i].pointB];
                int indexC = graph[triangles[i].pointC];
                
                if (indexA < 0 || indexB < 0 || indexC < 0) { continue; }
                if (indexA == indexB || indexB == indexC || indexC == indexA) { continue; }
                
                graph.SetEdgeWeight(indexA, indexB, triangles[i].GetLengthAB());
                graph.SetEdgeWeight(indexB, indexC, triangles[i].GetLengthBC());
                graph.SetEdgeWeight(indexC, indexA, triangles[i].GetLengthCA());
            }

            return graph;
        }

        static private List<Triangle> CaluclateTrianglesFromPoints(List<Vector2> _points, float _pointsSquareBounds) {
            
            List<Triangle> triangles = new() {
                GetSuperTriangle(_pointsSquareBounds)
            };

            foreach(Vector2 point in _points) {
                
                List<Triangle> badTriangles =  new List<Triangle>();

                foreach(Triangle tri in triangles) {
                    if (tri.GetCircumcircle().IsPositionInCircle(point)) {
                        badTriangles.Add(tri);
                    }
                }

                List<Vector2[]> polygonEdges = new();

                foreach(Triangle badTri in badTriangles) { 
                    List<Vector2[]> edges = new() {
                        new Vector2[2] { badTri.pointA, badTri.pointB },
                        new Vector2[2] { badTri.pointB, badTri.pointC },
                        new Vector2[2] { badTri.pointC, badTri.pointA }
                    };

                    foreach(Vector2[] edge in edges) {

                        int count = 0;

                        foreach(Triangle badTri2 in badTriangles) { 
                            if (badTri2.ContainsPoint(edge[0]) && badTri2.ContainsPoint(edge[1])) {
                                count++;
                            }
                        }

                        if (count <= 1) {
                            polygonEdges.Add(edge);
                        }
                    }
                }

                for(int i = triangles.Count-1; i >=0; i--) {
                    foreach(Triangle badTri in badTriangles) {
                        if (triangles[i] == badTri) {
                            triangles.RemoveAt(i);
                            break;
                        }
                    }
                }

                badTriangles.Clear();

                foreach(Vector2[] edge in polygonEdges) {
                    Triangle newTri = new(point, edge[0], edge[1]);
                    triangles.Add(newTri);
                }
                polygonEdges.Clear();
            }

            return triangles;
        }

        static private Triangle GetSuperTriangle(float _squareSize) {

            float triangleSideLength = _squareSize / 0.464f;

            Vector3Int triA = new((int)(_squareSize * 0.5f - triangleSideLength * 0.5f), 0, 0);
            Vector3Int triB = new((int)(_squareSize * 0.5f + triangleSideLength * 0.5f), 0, 0);

            Vector3Int triC = new((int)(_squareSize * 0.5f), (int)(0.86602540378f * triangleSideLength), 0);

            return new Triangle(triA, triB, triC);
        }
    }
}