using Core.Data;
using Scellecs.Morpeh;

namespace Core.ECS.Components
{
    public struct ShipComponent : IComponent
    {
        public PlayerId PlayerId;
        public int Size;
        public Orientation Orientation;
        public GridPosition GridPosition; // нос
        public int Hits; // количество попаданий
        public bool IsSunk;
    }
}