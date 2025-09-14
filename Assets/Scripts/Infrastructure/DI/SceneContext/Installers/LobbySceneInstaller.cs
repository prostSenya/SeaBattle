using Infrastructure.DI.SceneContext.Initializers;
using UI.LobbyPanel;
using UI.LobbyPanel.Presenters;
using UI.LobbyPanel.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI.SceneContext.Installers
{
    public sealed class LobbySceneInstaller : MonoInstaller
    {
        [SerializeField] private LobbyView lobbyView;
        [SerializeField] private LobbySceneInitializer _lobbySceneInitializer;
        
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterComponent(_lobbySceneInitializer);

            builder.RegisterComponent(lobbyView).AsImplementedInterfaces();

            builder.Register<LobbyPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}