using System;
using System.Collections.Generic;
using Fusion;

namespace UI.LobbyPanel.Presenters
{
    public interface ILobbyPresenter : IDisposable
    {
        void UpdateLobbys(IReadOnlyList<SessionInfo> sessionInfos);
        void EnableLoadingAnimation();
        void DisableLoadingAnimation();
    }
}