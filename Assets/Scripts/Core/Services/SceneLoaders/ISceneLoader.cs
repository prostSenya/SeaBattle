using System;
using Cysharp.Threading.Tasks;

namespace Core.Services.SceneLoaders
{
    public interface ISceneLoader
    {
        public UniTask LoadScene(string sceneKey, bool additive = false, bool activateOnLoad = true);

        event Action SceneLoaded;
    }
}