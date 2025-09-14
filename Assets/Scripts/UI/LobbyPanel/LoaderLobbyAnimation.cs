using UnityEngine;

namespace UI.LobbyPanel
{
    public class LoaderLobbyAnimation : MonoBehaviour
    {
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}