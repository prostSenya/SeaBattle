using Core.Data;
using Scellecs.Morpeh;

namespace Core.ECS.Components
{
    public struct RevealShipRequest : IComponent
    {
        // когда корабль добит
        public PlayerId Owner;
        public int ShipEntityId;
    }
}