using Core.Data;
using Core.Data.Configs;

namespace Core.Services.PlacementServices
{
    public interface IPlacementService
    {
        void Clear(PlayerId owner);
        bool TryAutoPlaceAll(PlayerId owner, BattleConfig cfg, int seed);
    }
}