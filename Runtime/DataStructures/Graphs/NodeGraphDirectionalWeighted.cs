using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Packages.DataStructures.NodeGraphs  {

    public class NodeGraphDirectionalWeighted<TData> : IEnumerable<TData>
    {
        protected List<TData> nodeList;
        protected Dictionary<TData, int> nodeIndexLookup;
        protected List<List<float>> adjacencyMatrix;

        #region Constructors
        public NodeGraphDirectionalWeighted() {
            nodeList = new List<TData>();
            nodeIndexLookup = new Dictionary<TData, int>();
            adjacencyMatrix = new List<List<float>>();
        }

        public NodeGraphDirectionalWeighted(NodeGraphDirectionalWeighted<TData> _baseGraph) : this() {
            this.Union(_baseGraph);
        }
        #endregion

        #region Getting Nodes
        public TData this[int _index] { get {  return nodeList[_index]; } }

        public int this[TData _node] { 
            get {
                if (!nodeIndexLookup.ContainsKey(_node)) { return -1;}
                return nodeIndexLookup[_node];
            } 
        }

        public int NodeCount { get { return nodeList.Count; } }
        
        public bool Contains(TData _node) {
            return nodeList.Contains(_node);
        }
        #endregion

        #region Adding Nodes
        public void AddNode(TData _node) {
            AddNode(_node, Mathf.Infinity);
        }

        virtual public void AddNode(TData _node, float _startWeight) {
            AddNodeToList(_node, _startWeight);
        }
        
        public void AddNodeRange(List<TData> _nodes) {
            AddNodeRange(_nodes, Mathf.Infinity);
        }

        virtual public void AddNodeRange(List<TData> _nodes, float _startWeight) {
            foreach(TData newNode in _nodes) {
                AddNodeToList(newNode, _startWeight);
            }
        }

        private void AddNodeToList(TData _node, float _startWeight) {
            if (nodeList.Contains(_node)) { return; }

            nodeList.Add(_node);
            nodeIndexLookup.Add(_node, nodeList.Count-1);

            for(int i = 0; i < nodeList.Count-1; i++) { adjacencyMatrix[i].Add(Mathf.Infinity); }

            adjacencyMatrix.Add(new List<float>());

            for(int j=0; j < nodeList.Count; j++) {
                adjacencyMatrix[nodeIndexLookup[_node]].Add(_startWeight);
            }
        }
        #endregion

        #region Edges
        public float GetEdgeWeight(TData _nodeA, TData _nodeB) {
            return GetEdgeWeight(nodeIndexLookup[_nodeA], nodeIndexLookup[_nodeB]);
        }

        public float GetEdgeWeight(int _nodeA, int _nodeB) {
            return adjacencyMatrix[_nodeA][_nodeB];
        }

        public void SetEdgeWeight(TData _nodeA, TData _nodeB, float _weight) {
            SetEdgeWeight(nodeIndexLookup[_nodeA], nodeIndexLookup[_nodeB], _weight);
        }

        virtual public void SetEdgeWeight(int _nodeA, int _nodeB, float _weight) {
            adjacencyMatrix[_nodeA][_nodeB] = _weight;
        }
        #endregion

        #region Adjacents
        public List<TData> GetAdjacentNodes(TData _node) {
            List<TData> adjNodes = new();

            for(int i = 0; i < NodeCount; i++) {
                if(adjacencyMatrix[this[_node]][i] >= Mathf.Infinity) { continue; }
                adjNodes.Add(nodeList[i]);
            }
            
            return adjNodes;
        }

        public List<int> GetAdjacentNodes(int _node) {
            List<int> adjNodes = new List<int>();

            for(int i = 0; i < NodeCount; i++) {
                if(adjacencyMatrix[_node][i] >= Mathf.Infinity) { continue; }
                adjNodes.Add(i);
            }

            return adjNodes;
        }
        #endregion

        #region Operators
        public void Union(NodeGraphDirectionalWeighted<TData> _other) {
            // Adding nodes.
            for(int i = 0; i < _other.NodeCount; i++) { AddNode(_other[i]); }

            foreach(TData node in _other) {
                List<TData> adjacents = _other.GetAdjacentNodes(node);
                foreach(TData neighbour in adjacents) {
                    this.SetEdgeWeight(node, neighbour, _other.GetEdgeWeight(node, neighbour));
                }
            }
        }
        #endregion

        #region IEnumerator
        public IEnumerator<TData> GetEnumerator()
        {
            return nodeList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear() {
            nodeList.Clear();
            nodeIndexLookup.Clear();
            
            for(int i=0; i < adjacencyMatrix.Count; i++) {
                adjacencyMatrix[i].Clear();
            }
            adjacencyMatrix.Clear();
        }
        #endregion

        #region Data
        public void WriteEdges<TOther>(NodeGraphDirectionalWeighted<TOther> _from) {
            int maxNodes = Mathf.Min(this.NodeCount, _from.NodeCount);

            for(int i = 0; i < maxNodes; i++) {
                for(int j = 0; j < maxNodes; j++) {
                    this.SetEdgeWeight(i, j, _from.GetEdgeWeight(i, j));
                }
            }
        }

        #endregion
        
        public string GetAdjacencyMatrixDebugString(int range) {
            string debugString = " |  ";
            for(int i =0; i< Mathf.Min(nodeList.Count, range); i++) {
                debugString += " "+ i.ToString() + "   ";
            }

            debugString += "\n";
            for(int i = 0; i < Mathf.Min(nodeList.Count, range); i++) {
                debugString += i.ToString() + "|";
                for(int j = 0; j < Mathf.Min(nodeList.Count, range); j++) {
                    if (adjacencyMatrix[i][j] < Mathf.Infinity) {
                        debugString += " "+adjacencyMatrix[i][j].ToString("N1");
                    }
                    else {
                        debugString += " inf ";
                    }
                }
                debugString += "\n";
            }
            return debugString;
        }
    }
}

