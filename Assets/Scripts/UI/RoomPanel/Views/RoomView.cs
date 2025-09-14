using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.RoomPanel.Views
{
    public class RoomView : MonoBehaviour, IRoomView
    {
        [SerializeField] private RoomAvatarView _firstPlayer;
        [SerializeField] private RoomAvatarView _secondPlayer;

        [SerializeField] private Button _exitButton;
        [SerializeField] private TMP_Text _timerText;

        private void OnEnable() => 
            _exitButton.onClick.AddListener(OnClicked);

        private void OnDisable() => 
            _exitButton.onClick.RemoveListener(OnClicked);

        private void OnClicked()
        {
        }

        public void EnableFirstPerson()
        {
            _firstPlayer.gameObject.SetActive(true);
        }

        public void EnableSecondPerson()
        {
            _secondPlayer.gameObject.SetActive(true);
        }
    }
}