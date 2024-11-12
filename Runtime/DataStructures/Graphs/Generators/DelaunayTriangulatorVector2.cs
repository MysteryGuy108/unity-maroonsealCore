using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MaroonSeal.Core.Maths.Geometry;

namespace MaroonSeal.Core.DataStructures.NodeGraphs.Generators {
    public class DelaunayTriangulatorGraphCalculator
    {
        static public NodeGraphWeighted<Vector2> CalculateGraph(List<Vector2> _points, float _pointsSquareBounds) {

            NodeGraphWeighted<Vector2> graph = new();
            
            // Adding points to graph.
            graph.AddNodeRange(_points);

            List<TriangleVector2> triangles = CaluclateTrianglesFromPoints(_points, _pointsSquareBounds);

            for(int i = 0; i < triangles.Count; i++) {
                
                int indexA = graph[triangles[i].PointA];
                int indexB = graph[triangles[i].PointB];
                int indexC = graph[triangles[i].PointC];
                
                if (indexA < 0 || indexB < 0 || indexC < 0) { continue; }
                if (indexA == indexB || indexB == indexC || indexC == indexA) { continue; }
                
                graph.SetEdgeWeight(indexA, indexB, triangles[i].EdgeAB);
                graph.SetEdgeWeight(indexB, indexC, triangles[i].EdgeBC);
                graph.SetEdgeWeight(indexC, indexA, triangles[i].EdgeCA);
            }

            return graph;
        }

        static private List<TriangleVector2> CaluclateTrianglesFromPoints(List<Vector2> _points, float _pointsSquareBounds) {
            
            List<TriangleVector2> triangles = new() {
                GetSuperTriangle(_pointsSquareBounds)
            };

            foreach(Vector2 point in _points) {
                
                List<TriangleVector2> badTriangles =  new List<TriangleVector2>();

                foreach(TriangleVector2 tri in triangles) {
                    if (tri.CalculateCircumcircle().IsPointInCircle(point)) {
                        badTriangles.Add(tri);
                    }
                }

                List<Vector2[]> polygonEdges = new();

                foreach(TriangleVector2 badTri in badTriangles) { 
                    List<Vector2[]> edges = new() {
                        new Vector2[2] { badTri.PointA, badTri.PointB },
                        new Vector2[2] { badTri.PointB, badTri.PointC },
                        new Vector2[2] { badTri.PointC, badTri.PointA }
                    };

                    foreach(Vector2[] edge in edges) {

                        int count = 0;

                        foreach(TriangleVector2 badTri2 in badTriangles) { 
                            if (badTri2.HasVertex(edge[0]) && badTri2.HasVertex(edge[1])) {
                                count++;
                            }
                        }

                        if (count <= 1) {
                            polygonEdges.Add(edge);
                        }
                    }
                }

                for(int i = triangles.Count-1; i >=0; i--) {
                    foreach(TriangleVector2 badTri in badTriangles) {
                        if (triangles[i] == badTri) {
                            triangles.RemoveAt(i);
                            break;
                        }
                    }
                }

                badTriangles.Clear();

                foreach(Vector2[] edge in polygonEdges) {
                    TriangleVector2 newTri = new TriangleVector2(point, edge[0], edge[1]);
                    triangles.Add(newTri);
                }
                polygonEdges.Clear();
            }

            return triangles;
        }

        static private TriangleVector2 GetSuperTriangle(float _squareSize) {

            float triangleSideLength = _squareSize / 0.464f;

            Vector2Int triA = new((int)(_squareSize * 0.5f - triangleSideLength * 0.5f), 0);
            Vector2Int triB = new((int)(_squareSize * 0.5f + triangleSideLength * 0.5f), 0);

            Vector2Int triC = new((int)(_squareSize * 0.5f), (int)(0.86602540378f * triangleSideLength));

            return new TriangleVector2(triA, triB, triC);
        }
    }
}