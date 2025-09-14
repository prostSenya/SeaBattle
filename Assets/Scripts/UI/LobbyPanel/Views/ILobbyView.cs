using System;

namespace UI.LobbyPanel.Views
{
    public interface ILobbyView
    {
        void CreateLobbyPanelView(ILobbyPanelPresenter lobbyPanelPresenter);
        bool Contains(string key);
        void RemoveLobby(string key);
        void ClearAll();
        event Action LobbyCreated;
        void EnableLoadAnimation();
        void DisableLoadAnimation();
    }
}