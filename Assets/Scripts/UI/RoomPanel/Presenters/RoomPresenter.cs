using Network.NetworkRunnerProvider;
using UI.RoomPanel.Views;

namespace UI.RoomPanel.Presenters
{
    public class RoomPresenter : IRoomPresenter
    {
        private readonly IRoomView _roomView;
        private readonly INetworkRunnerProvider _networkRunnerProvider;

        public RoomPresenter(IRoomView roomView, INetworkRunnerProvider  networkRunnerProvider)
        {
            _roomView = roomView;
            _networkRunnerProvider = networkRunnerProvider;
        }

        public void Enable()
        {
            if (_networkRunnerProvider.Runner.IsServer)
            {
                _roomView.EnableFirstPerson();
            }
            else
            {
                _roomView.EnableSecondPerson();
            }
        }

        public void Disable()
        {
            
        }
    }
}