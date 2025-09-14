using System;
using System.Collections.Generic;
using Core.Services.SceneLoaders;
using Cysharp.Threading.Tasks;
using Fusion;
using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States.Implementations;
using Network.LobbyServices;
using Network.NetworkRunnerProvider;
using UI.LobbyPanel.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace UI.LobbyPanel.Presenters
{
    public class LobbyPresenter : ILobbyPresenter
    {
        private readonly ILobbyView _lobbyView;
        private readonly ILobbyService _lobbyService;
        private readonly IStateMachine _stateMachine;
        private readonly INetworkRunnerProvider _networkRunnerProvider;
        private readonly Dictionary<string, ILobbyPanelPresenter> _presenters = new();

        public LobbyPresenter(
            ILobbyView lobbyView,
            ILobbyService lobbyService,
            IStateMachine stateMachine,
            INetworkRunnerProvider networkRunnerProvider)
        {
            _lobbyView = lobbyView;
            _lobbyService = lobbyService;
            _stateMachine = stateMachine;
            _networkRunnerProvider = networkRunnerProvider;
            _lobbyView.LobbyCreated += OnLobbyCreated;
        }

        public void UpdateLobbys(IReadOnlyList<SessionInfo> sessionInfos)
        {
            foreach (SessionInfo sessionInfo in sessionInfos)
            {
                string key = sessionInfo.Name;

                if (_presenters.TryGetValue(key, out ILobbyPanelPresenter lobbyPanelPresenter))
                {
                    lobbyPanelPresenter.UpdateSessionData(sessionInfo);
                }
                else
                {
                    ILobbyPanelPresenter newLobbyPanelPresenter = new LobbyPanelPresenter(sessionInfo, _lobbyService,
                        _stateMachine, _networkRunnerProvider);
                    _presenters[key] = newLobbyPanelPresenter;

                    _lobbyView.CreateLobbyPanelView(newLobbyPanelPresenter);
                }
            }
        }

        public void EnableLoadingAnimation() =>
            _lobbyView.EnableLoadAnimation();

        public void DisableLoadingAnimation() =>
            _lobbyView.DisableLoadAnimation();

        public void Dispose() =>
            _lobbyView.LobbyCreated -= OnLobbyCreated;

        // private async void OnLobbyCreated()
        // {
        //     try
        //     {
        //         await _lobbyService.CreateAndHostAsync();
        //     }
        //     catch (Exception e)
        //     {
        //         throw new Exception("Error creating lobby", e);
        //     }
        //     
        //     AsyncOperationHandle<IList<IResourceLocation>> handle =
        //         Addressables.LoadResourceLocationsAsync("Room", typeof(SceneInstance));
        //
        //     await handle.Task;
        //     SceneRef sceneRef = default;
        //
        //     if (handle.Status == AsyncOperationStatus.Succeeded)
        //     {
        //         foreach (var loc in handle.Result)
        //         {
        //             Debug.Log($"Нашёл сцену: {loc.PrimaryKey} | {loc.InternalId}");
        //             sceneRef = SceneRef.FromPath(loc.PrimaryKey);
        //             break;
        //         }
        //     }
        //     else
        //     {
        //         Debug.LogWarning("Локации не найдены или handle завершился с ошибкой");
        //     }
        //
        //     Addressables.Release(handle);
        //
        //     try
        //     {
        //         await _networkRunnerProvider.Runner.LoadScene(sceneRef).ToUniTask();
        //         _stateMachine.ChangeState<RoomState>();
        //     }
        //     catch (Exception e)
        //     {
        //         throw new Exception("Error loading scene", e);
        //     }
        // }
        //
        private async void OnLobbyCreated()
        {
            // 1) Создаём/хостим
            try
            {
                await _lobbyService.CreateAndHostAsync(); // внутри должен успешно стартовать Runner.StartGame(...)
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error creating lobby: {e}");
                return;
            }

            // 2) Убеждаемся, что у Runner есть SceneManager
            var runner = _networkRunnerProvider.Runner;
            if (!runner)
            {
                Debug.LogError("Runner is null");
                return;
            }

            if (runner.SceneManager == null)
                runner.gameObject.AddComponent<NetworkSceneManagerDefault>();

            if (runner.IsSceneAuthority == false)
            {
                Debug.LogWarning("This peer is not scene authority; skip LoadScene");
                return;
            }

            // 3) Адрес сцены (Addressable Address). НЕ InternalId!
            const string sceneAddress = "Room"; // или "Assets/Scenes/Room.unity"

            // (Необязательно, но можно проверить, что адрес существует)
            AsyncOperationHandle<IList<UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation>> handle =
                Addressables.LoadResourceLocationsAsync(sceneAddress, typeof(SceneInstance));
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded || handle.Result.Count == 0)
            {
                Debug.LogError($"Scene address '{sceneAddress}' not found in Addressables.");
                Addressables.Release(handle);
                return;
            }

            var loc = handle.Result[0];
            Debug.Log($"Found scene: address={loc.PrimaryKey} | internal={loc.InternalId}");

            // 4) Собираем SceneRef ИМЕННО из Address (PrimaryKey)
            var sceneRef = SceneRef.FromPath(loc.PrimaryKey);

            Addressables.Release(handle);

            // 5) Грузим через Fusion (Host триггерит, все клиенты подтянутся)
            try
            {
                // В Fusion 2 это Task → ToUniTask норм
                await runner.LoadScene(sceneRef, LoadSceneMode.Single).ToUniTask();
                _stateMachine.ChangeState<RoomState>();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading scene via Fusion: {e}");
            }
        }
    }
}