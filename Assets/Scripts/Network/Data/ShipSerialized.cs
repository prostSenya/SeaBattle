using System;
using Core.Data;

namespace Network.Data
{
    [Serializable]
    public struct ShipSerialized
    {
        public GridPosition bow;
        public int size;
        public Orientation o;
    }
}