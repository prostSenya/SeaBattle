using Core.Data;
using Scellecs.Morpeh;

namespace Core.ECS.Components
{
    public struct ShotRequest : IComponent
    {
        public PlayerId PlayerId;
        public GridPosition GridPosition; // координата на поле соперника
    }
}