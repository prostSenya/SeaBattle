using Core.Data;
using Scellecs.Morpeh;

namespace Core.ECS.Components
{
    public struct CellComponent : IComponent
    {
        public GridPosition GridPosition;
        public PlayerId PlayerId; // чьё это поле
        public bool HasShip; // true если корабль в этой клетке (скрыто для оппонента)
        public bool IsHit;  // попадание по этой клетке
    }
}