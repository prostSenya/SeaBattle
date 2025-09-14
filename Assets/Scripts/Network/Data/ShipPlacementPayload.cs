using System;
using System.Collections.Generic;

namespace Network.Data
{
    // Для передачи сетевых данных (минимально)
    [Serializable]
    public struct ShipPlacementPayload
    {
        public List<ShipSerialized> ships;
    }
}