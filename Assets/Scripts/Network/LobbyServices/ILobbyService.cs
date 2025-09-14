using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Fusion;

namespace Network.LobbyServices
{
    public interface ILobbyService
    {
        Task CreateAndHostAsync(NetworkSceneInfo sceneBuildIndex =  default);
        Task JoinByNameAsync(string sessionName);
        Task JoinRandomAsync();
        Task LeaveAsync();
        UniTask JoinToLobbySession(SessionLobby sessionLobby);
        event Action JoinedToLobbySession;
        Task StartHost();
    }
}