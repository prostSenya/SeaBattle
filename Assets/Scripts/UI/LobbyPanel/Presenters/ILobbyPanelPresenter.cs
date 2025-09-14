using System;
using Fusion;

namespace UI.LobbyPanel
{
    public interface ILobbyPanelPresenter
    {
        string RoomName { get;}
        int PlayerCount { get; }
        event Action<int> PlayerCountChanged;
        event Action<string> RoomNameChanged;
        void JoinToRoom();
        void UpdateSessionData(SessionInfo sessionInfo);
    }
}