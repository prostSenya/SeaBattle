using Core.Services.SceneObjectResolvers;
using UnityEngine;
using VContainer;

namespace Infrastructure.DI.SceneContext.Initializers
{
    public class RoomSceneInitializer : MonoBehaviour
    {
        private ISceneScopeResolverProxy _sceneScopeResolverProxy;

        [Inject]
        private void Construct(ISceneScopeResolverProxy sceneScopeResolverProxy, IObjectResolver objectResolver)
        {
            _sceneScopeResolverProxy = sceneScopeResolverProxy;
            _sceneScopeResolverProxy.Resolver =  objectResolver;
        }
    }
}