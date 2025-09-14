using System;
using TMPro;
using UI.RoomPanel.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace UI.RoomPanel.Views
{
    public class RoomAvatarView : MonoBehaviour
    {
        [SerializeField] private Button _readyButton;
        [SerializeField] private TMP_Text _readyText;
        [SerializeField] private TMP_Text _nickName;
        [SerializeField] private TMP_Text _connectionText;
        [SerializeField] private Image _avatar;

        private IRoomAvatarPresenter _presenter;

        
        public void Construct(IRoomAvatarPresenter roomAvatarPresenter)
        {
            _presenter = roomAvatarPresenter;
            _presenter.NickNameChanged += OnNickNameChanged;
            _presenter.ConnectionInfoChanged += OnConnectionInfoChanged;
            _presenter.AvatarChanged += OnAvatarChanged;
        }
        
        private void OnEnable() => 
            _readyButton.onClick.AddListener(OnClicked);

        private void OnDisable() => 
            _readyButton.onClick.RemoveListener(OnClicked);

        private void OnDestroy()
        {
            _presenter.NickNameChanged -= OnNickNameChanged;
            _presenter.ConnectionInfoChanged -= OnConnectionInfoChanged;
            _presenter.AvatarChanged -= OnAvatarChanged;
        }
        
        private void OnClicked()
        {
            
        }

        private void OnAvatarChanged(Sprite newAvatar)
        {
            throw new NotImplementedException();
        }

        private void OnConnectionInfoChanged(string newConnectionInfo)
        {
            throw new NotImplementedException();
        }

        private void OnNickNameChanged(string newNickName)
        {
            throw new NotImplementedException();
        }
    }
}