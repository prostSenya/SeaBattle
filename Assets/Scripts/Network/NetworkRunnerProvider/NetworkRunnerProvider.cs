using System;
using Fusion;
using UnityEngine;

namespace Network.NetworkRunnerProvider
{
    public class NetworkRunnerProvider : INetworkRunnerProvider
    {
        public NetworkRunner Runner { get; private set; }
        
        public void Remember(NetworkRunner runner)
        {
            if (runner == null)
                throw new NullReferenceException($"{runner} is null in NetworkRunnerProvider.Remember()");

            Runner = runner;
        }
        
        public void Forget() => 
            Runner = null;
    }
}