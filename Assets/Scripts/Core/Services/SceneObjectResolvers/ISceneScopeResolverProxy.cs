using VContainer;

namespace Core.Services.SceneObjectResolvers
{
    public interface ISceneScopeResolverProxy
    {
        IObjectResolver Resolver { get; set; }
    }
}