using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaroonSeal.Packages.Maths.Cardinals {
    [System.Serializable]
    public struct Cardinal2DArray<TData> {
        [SerializeField] private TData north;
        [SerializeField] private TData east;
        [SerializeField] private TData south;
        [SerializeField] private TData west;

        #region Constructors
        public Cardinal2DArray(TData _blankData) {
            north = _blankData;
            east = _blankData;
            south = _blankData;
            west = _blankData;
        }

        public Cardinal2DArray(TData _north, TData _east, TData _south, TData _west) {
            north = _north;
            east = _east;
            south = _south;
            west = _west;
        }

        public Cardinal2DArray(TData[] _edges) {
            north = _edges[0];
            east = _edges[1];
            south = _edges[2];
            west = _edges[3];
        }

        public Cardinal2DArray(Cardinal2DArray<TData> _other) {
            north = _other[0];
            east = _other[1];
            south = _other[2];
            west = _other[3];
        }
        #endregion

        #region Get/Set
        public TData this[int i] {
            get { 
                switch (i) {
                    case 0:
                        return north;
                    case 1:
                        return east;
                    case 2:
                        return south;
                    case 3:
                        return west;
                }
                return default(TData);
            }

            set {
                switch (i) {
                    case 0:
                        north = value; break;
                    case 1:
                        east = value; break;
                    case 2:
                        south = value; break;
                    case 3:
                        west = value; break;
                }
            }
        }

        public TData this[Cardinal2D _i] {
            get { return this[_i.Index]; }
            set { this[_i.Index] = value; }
        }

        private TData GetData(Cardinal2D _direction) {
            switch(_direction.cardinalDirection) {
                case Cardinal2D.CardinalIndex.East:
                    return east;
                case Cardinal2D.CardinalIndex.South:
                    return south;
                case Cardinal2D.CardinalIndex.West:
                    return west;
                default:
                    return north;
            }
        }

        public void SetData(TData _data, Cardinal2D _direction) {
            switch(_direction.cardinalDirection) {
                case Cardinal2D.CardinalIndex.East:
                    east = _data;
                    return;
                case Cardinal2D.CardinalIndex.South:
                    south = _data;
                    return;
                case Cardinal2D.CardinalIndex.West:
                    west = _data;
                    return;
                default:
                    north = _data;
                    return;
            }
        }

        public TData[] GetArray() {
            return new TData[4] { north, east, south, west };
        }
        #endregion

        #region Operators
        public static bool operator == (Cardinal2DArray<TData> _x, Cardinal2DArray<TData> _y) {
            return _x.north.Equals(_y.north) && 
                _x.east.Equals(_y.east) &&
                _x.south.Equals(_y.south) &&
                _x.west.Equals(_y.west);
        }

        public static bool operator != (Cardinal2DArray<TData> _x, Cardinal2DArray<TData> _y) {
            return !_x.north.Equals(_y.north) && 
                    _x.east.Equals(_y.east) &&
                    _x.south.Equals(_y.south) &&
                    _x.west.Equals(_y.west);
        }

        override public bool Equals(object obj) {
            if (obj is not Cardinal2DArray<TData>) { return false; }

            Cardinal2DArray<TData> mys = (Cardinal2DArray<TData>) obj;
            return this == mys;
        }
        #endregion
    
        public Cardinal2DArray<TData> GetRotated(Cardinal2D _rotation) {
            return GetRotated(_rotation, true);
        }

        public Cardinal2DArray<TData> GetRotated(Cardinal2D _rotation, bool _isClockwise) {

            TData[] rotatedEdges = new TData[4];
            TData[] edgeArray = GetArray();

            for (int i = 0; i < 4; i++) {
                int index;
                if (_isClockwise) {
                    index = (i + _rotation.Index) % 4;
                }
                else {
                    index = (i - _rotation.Index) % 4;
                }
                rotatedEdges[index] = edgeArray[i];
            }

            return new Cardinal2DArray<TData>(rotatedEdges);
        }
    }
}
