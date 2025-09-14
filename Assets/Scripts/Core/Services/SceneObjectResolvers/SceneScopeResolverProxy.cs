using System;
using VContainer;

namespace Core.Services.SceneObjectResolvers
{
    public sealed class SceneScopeResolverProxy : ISceneScopeResolverProxy
    {
        private IObjectResolver _resolver;

        public IObjectResolver Resolver
        {
            get => _resolver;
            set => _resolver = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}