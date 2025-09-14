namespace Core.Data
{
    public enum CellState : byte
    {
        Unknown = 0,
        Miss = 1,
        Hit = 2,
        ShipHidden = 3,
        ShipRevealed = 4
    }
}