using System;
using Core.Data;
using Cysharp.Threading.Tasks;
using Network.Data;

namespace Core.Services.NetworkServices
{
    public interface INetworkService
    {
        // Жизненный цикл
        UniTask StartHostAsync(string room);
        UniTask StartClientAsync(string room);
        void Shutdown();

        // Игровые действия (в RPC на хост)
        void RpcClientReady(PlayerId who, ShipPlacementPayload payload);
        void RpcFire(PlayerId shooter, GridPosition target);

        // События от сети к игре
        event Action<PlayerId> OnPeerConnected;
        event Action<PlayerId> OnPeerDisconnected;
        event Action<PlayerId, GridPosition, bool, bool> OnShotResolved; // defender, cell, isHit, sunkNow
        event Action<PlayerId, ShipRevealPayload> OnShipRevealed; // добит — показать целиком
        event Action<PlayerId, PlayerId> OnGameOver; // winner, loser
        event Action<PlayerId> OnReadyAck; // сервер принял расстановку
    }
}