using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LobbyPanel.Views
{
    public class LobbyView : MonoBehaviour, ILobbyView
    {
        [SerializeField] private LobbyPanelView _lobbyPanelViewPrefab;
        [SerializeField] private GameObject _content;
        [SerializeField] private Button _createLobbyButton;
        [SerializeField] private LoaderLobbyAnimation _loaderLobbyAnimation;

        private Dictionary<string, LobbyPanelView> _lobbyPanelViews = new Dictionary<string, LobbyPanelView>();

        public event Action LobbyCreated;

        public void OnEnable() => 
            _createLobbyButton.onClick.AddListener(OnCreateLobbyClicked);

        public void OnDisable() => 
            _createLobbyButton.onClick.AddListener(OnCreateLobbyClicked);

        public void EnableLoadAnimation() => 
            _loaderLobbyAnimation.Enable();

        public void DisableLoadAnimation() => 
            _loaderLobbyAnimation.Disable();

        public void CreateLobbyPanelView(ILobbyPanelPresenter lobbyPanelPresenter)
        {
            LobbyPanelView lobbyPanelView = Instantiate(_lobbyPanelViewPrefab, _content.transform);
            lobbyPanelView.Construct(lobbyPanelPresenter);
        }

        public bool Contains(string key) => 
            _lobbyPanelViews.ContainsKey(key);

        public void RemoveLobby(string key)
        {
            if (_lobbyPanelViews.TryGetValue(key, out LobbyPanelView lobbyPanelView))
            {
                Destroy(lobbyPanelView.gameObject);
                _lobbyPanelViews.Remove(key);
            }
        }

        public void ClearAll() => 
            _lobbyPanelViews.Clear();

        private void OnCreateLobbyClicked()
        {
            LobbyCreated?.Invoke();
        }
    }
}