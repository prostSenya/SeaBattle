using System.Threading.Tasks;
using Fusion;

namespace Network.NetworkRunnerFactories
{
    public interface INetworkRunnerFactory
    {
        NetworkRunner Create();
        Task DestroyAsync();
    }
}