using Fusion;

namespace Network.NetworkRunnerProvider
{
    public interface INetworkRunnerProvider
    {
        NetworkRunner Runner { get; }
        void Remember(NetworkRunner runner);
        void Forget();
    }
}