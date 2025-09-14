using System.Threading.Tasks;
using Fusion;
using Network.NetworkRunnerProvider;
using UnityEngine;

namespace Network.NetworkRunnerFactories
{
    public class NetworkRunnerFactory : INetworkRunnerFactory
    {
        private readonly INetworkRunnerProvider _networkRunnerProvider;
        private readonly IFusionCallbacks _callbacks;

        public NetworkRunnerFactory(INetworkRunnerProvider  networkRunnerProvider, IFusionCallbacks callbacks)
        {
            _networkRunnerProvider = networkRunnerProvider;
            _callbacks = callbacks;
        }        
        
        public NetworkRunner Create()
        {
            if (_networkRunnerProvider.Runner && _networkRunnerProvider.Runner.gameObject)
            {
                Debug.LogError("NetworkRunner is exist");
                return _networkRunnerProvider.Runner;
            }
            
            GameObject networkRunner = new GameObject("[NetworkRunner]");
            GameObject.DontDestroyOnLoad(networkRunner);

            NetworkRunner runner = networkRunner.AddComponent<NetworkRunner>();

            runner.ProvideInput = true;
            runner.AddCallbacks(_callbacks);

            networkRunner.AddComponent<NetworkSceneManagerDefault>();
            
            _networkRunnerProvider.Remember(runner);
            
            return runner;
        }

        public async Task DestroyAsync()
        {
            if (_networkRunnerProvider.Runner == null)
                return;

            await _networkRunnerProvider.Runner.Shutdown();
            _networkRunnerProvider.Runner.RemoveCallbacks(_callbacks);
            GameObject.Destroy(_networkRunnerProvider.Runner.gameObject);
            _networkRunnerProvider.Forget();
        }
    }
}