using UI.MainMenuWindows;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI.SceneContext.Installers
{
    public sealed class MainMenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuPanel _mainMenuPanel;
        
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterComponent(_mainMenuPanel).AsSelf();
            builder.Register<MainMenuPresenter>(Lifetime.Transient).AsImplementedInterfaces();
        }
    }
}