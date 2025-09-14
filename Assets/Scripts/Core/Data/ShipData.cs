using System;
using UnityEngine;

namespace Core.Data
{
    [Serializable]
    public struct ShipData
    {
        public ShipType ShipType;
        
        [Range(1, 4)]
        public int Size;

        [Range(1, 4)]
        public int Count;
    }
}