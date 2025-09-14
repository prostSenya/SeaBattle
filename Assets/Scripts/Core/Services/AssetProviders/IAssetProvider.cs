using Cysharp.Threading.Tasks;

namespace Core.Services.AssetProviders
{
    public interface IAssetProvider
    {
        UniTask<T> LoadAsync<T>(string key) where T : UnityEngine.Object;
    }
}