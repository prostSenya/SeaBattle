using Core.Data;
using Scellecs.Morpeh;

namespace Core.ECS.Components
{
    public struct HitResult : IComponent
    {
        public PlayerId Defender; // тот, по кому стреляли
        public GridPosition GridPosition;
        public bool IsHit;
        public bool SunkNow;
    }

    // игрок завершил расстановку
}