using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Core.ECS.Components
{
    [Il2CppSetOption(Option.NullChecks, false)]
    public struct GridComponent : IComponent
    {
        public int Size;
    }
}