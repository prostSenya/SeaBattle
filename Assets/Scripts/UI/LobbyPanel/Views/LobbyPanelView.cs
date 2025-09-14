using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LobbyPanel
{
    public class LobbyPanelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _roomName;
        [SerializeField] private TMP_Text _playerCount;
        [SerializeField] private Button _buttonJoin;

        private ILobbyPanelPresenter _presenter;

        public void Construct(ILobbyPanelPresenter presenter)
        {
            _presenter = presenter;

            _presenter.RoomNameChanged += OnRoomNameChanged;
            _presenter.PlayerCountChanged += OnPlayerCountChanged;
            _roomName.text = _presenter.RoomName;
            _playerCount.text = _presenter.PlayerCount.ToString();
        }
        
        private void OnEnable()
        {
            _buttonJoin.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _buttonJoin.onClick.RemoveListener(OnClicked);
        }

        private void OnDestroy()
        {
            _presenter.RoomNameChanged -= OnRoomNameChanged;
            _presenter.PlayerCountChanged -= OnPlayerCountChanged;
        }

        private void OnClicked()
        {
            _presenter.JoinToRoom();
        }
        
        private void OnPlayerCountChanged(int newPlayerCount)
        {
            
        }

        private void OnRoomNameChanged(string newRoomName)
        {
            
        }
    }
}