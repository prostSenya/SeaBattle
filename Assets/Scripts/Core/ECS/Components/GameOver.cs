using Core.Data;
using Scellecs.Morpeh;

namespace Core.ECS.Components
{
    public struct GameOver : IComponent
    {
        public PlayerId Winner;
        public PlayerId Loser;
    }
}