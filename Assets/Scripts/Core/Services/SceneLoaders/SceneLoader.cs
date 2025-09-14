using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Core.Services.SceneLoaders
{
    public sealed class SceneLoader : ISceneLoader
    {
        public event Action SceneLoaded;
        public event Action<Scene> SceneLoadedDetailed;

        private readonly SemaphoreSlim _mutex = new(1, 1);

        // Текущий адресабл-handle и сцена
        private bool _hasHandle;
        private AsyncOperationHandle<SceneInstance> _handle;
        private bool _hasScene;
        private Scene _scene;

        public bool HasCurrentScene => _hasScene;

        public bool TryGetCurrentScene(out Scene scene)
        {
            scene = _scene;
            return _hasScene;
        }

        public async UniTask LoadScene(string sceneKey, bool additive = false, bool activateOnLoad = true)
        {
            // Совместимая обёртка поверх расширенного метода
            await LoadSceneAndGet(sceneKey, additive, activateOnLoad, setActiveScene: true, ct: default);
        }

        public async UniTask<Scene> LoadSceneAndGet(
            string sceneKey,
            bool additive = false,
            bool activateOnLoad = true,
            bool setActiveScene = true,
            CancellationToken ct = default)
        {
            await _mutex.WaitAsync(ct);
            try
            {
                // Сохраняем предыдущий handle, чтобы корректно освободить ПОСЛЕ успешной загрузки новой сцены.
                bool hadPrev = _hasHandle && _handle.IsValid();
                var prevHandle = _handle;

                // Стартуем загрузку новой сцены
                var mode = additive ? LoadSceneMode.Additive : LoadSceneMode.Single;
                _handle = Addressables.LoadSceneAsync(sceneKey, mode, activateOnLoad);
                _hasHandle = true;

                SceneInstance newSceneInstance;
                try
                {
                    newSceneInstance = await _handle.ToUniTask(cancellationToken: ct);
                }
                catch (Exception e)
                {
                    // Если загрузка упала — не трогаем старую; сбрасываем текущий handle
                    _hasHandle = false;
                    Debug.LogError($"[SceneLoader] Failed to load scene '{sceneKey}': {e}");
                    throw;
                }

                // Если не активировали при загрузке — можем активировать позже по запросу
                if (!activateOnLoad)
                {
                    // Ничего не делаем здесь; активация — через ActivateLastLoaded(...)
                }
                else if (setActiveScene)
                {
                    // Если сцена уже активирована, можно сделать её ActiveScene
                    SceneManager.SetActiveScene(newSceneInstance.Scene);
                }

                // Только после успешной загрузки/активации — корректно освобождаем предыдущий адресабл-handle (если был)
                if (hadPrev)
                {
                    try
                    {
                        await Addressables.UnloadSceneAsync(prevHandle).ToUniTask(cancellationToken: ct);
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"[SceneLoader] Unload previous scene failed: {e}");
                    }
                }

                // Обновляем текущее состояние
                _scene = newSceneInstance.Scene;
                _hasScene = true;

                // События
                try { SceneLoaded?.Invoke(); } catch { /* не роняем поток */ }
                try { SceneLoadedDetailed?.Invoke(_scene); } catch { /* не роняем поток */ }

                return _scene;
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async UniTask ActivateLastLoaded(CancellationToken ct = default)
        {
            await _mutex.WaitAsync(ct);
            try
            {
                if (!_hasHandle || !_handle.IsValid())
                    return;

                // Если сцена уже активна, ActivateAsync завершится мгновенно
                try
                {
                    var instance = _handle.Result; // безопасно: _handle уже завершён после LoadSceneAndGet
                    await instance.ActivateAsync().ToUniTask(cancellationToken: ct);

                    // После активации имеет смысл сделать её активной
                    if (_hasScene)
                        SceneManager.SetActiveScene(_scene);
                }
                catch (Exception e)
                {
                    Debug.LogError($"[SceneLoader] ActivateLastLoaded failed: {e}");
                    throw;
                }
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async UniTask UnloadCurrent(CancellationToken ct = default)
        {
            await _mutex.WaitAsync(ct);
            try
            {
                if (_hasHandle && _handle.IsValid())
                {
                    try
                    {
                        await Addressables.UnloadSceneAsync(_handle).ToUniTask(cancellationToken: ct);
                    }
                    finally
                    {
                        _hasHandle = false;
                        _hasScene = false;
                        _scene = default;
                    }
                }
            }
            finally
            {
                _mutex.Release();
            }
        }
    }
}
