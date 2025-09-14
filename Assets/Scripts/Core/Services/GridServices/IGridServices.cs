using Core.Data;
using Core.Data.Configs;

namespace Core.Services.GridServices
{
    public interface IGridServices
    {
        bool CanPlaceShip(PlayerId owner, GridPosition bow, int size, Orientation o);
        void PlaceShip(PlayerId owner, GridPosition bow, int size, Orientation o);
        bool AllShipsPlaced(PlayerId owner, BattleConfig cfg);
        bool IsAllSunk(PlayerId owner, BattleConfig cfg);
    }
}