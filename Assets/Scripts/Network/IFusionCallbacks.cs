using System;
using System.Collections.Generic;
using Fusion;

namespace Network
{
    public interface IFusionCallbacks : INetworkRunnerCallbacks
    {
        public event Action<IReadOnlyList<SessionInfo>> SessionListUpdated;
    }
}