using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.MainMenuWindows
{
    public class MainMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _lobbyButton;

        private IMainMenuPresenter _presenter;
        
        [Inject]
        public void Construct(IMainMenuPresenter presenter) => 
            _presenter = presenter;

        private void OnEnable()
        {
             _exitButton.onClick.AddListener(OnExit);   
            _lobbyButton.onClick.AddListener(OnLobby);   
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExit);   
            _lobbyButton.onClick.RemoveListener(OnLobby);   
        }

        private void OnLobby()
        {
            _presenter.GoToLobby();
        }

        private void OnExit()
        {
            _presenter.ExitApplication();
        }
    }
}