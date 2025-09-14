using System;
using UnityEngine;

namespace Core.Data
{
    [Serializable]
    public struct GridPosition
    {
        [Range(0, 15)]
        public int X;
        [Range(0, 15)]
        public int Y;
    }
}