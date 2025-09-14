using Core.Services;
using Core.Services.SceneLoaders;
using Core.Services.SceneObjectResolvers;
using Core.Services.StateFactories;
using Infrastructure.DI.ProjectContext.Initializers;
using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States.Implementations;
using Network;
using Network.LobbyServices;
using Network.NetworkRunnerFactories;
using Network.NetworkRunnerProvider;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI.ProjectContext.Installers
{
    public class ProjectInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private ProjectInitializer _projectInitializer;
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterComponent(_projectInitializer).AsImplementedInterfaces();
            RegisterServices(builder);
            RegisterGlobalStateMachine(builder);
        }

        private void RegisterGlobalStateMachine(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<BootState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<LoadSceneState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<MainMenuState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<LobbyState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<RoomState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.RegisterComponent(this).As<ICoroutineRunner>();
            
            builder.Register<SceneScopeResolverProxy>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StateFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SceneLoader>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<LobbyService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FusionCallbacks>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<NetworkRunnerProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<NetworkRunnerFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}