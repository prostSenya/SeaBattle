using Infrastructure.DI.SceneContext.Initializers;
using UI.LobbyPanel.Presenters;
using UI.RoomPanel.Presenters;
using UI.RoomPanel.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI.SceneContext.Installers
{
    public sealed class RoomSceneInstaller : MonoInstaller
    {
        [SerializeField] private RoomView _roomView;
        [SerializeField] private RoomSceneInitializer _roomSceneInitializer;
        
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterComponent(_roomSceneInitializer);
            builder.RegisterComponent(_roomView).AsImplementedInterfaces();
            builder.Register<RoomPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}