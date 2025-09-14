using System;
using System.Collections.Generic;
using Core.Data;

namespace Core.ECS.Utils
{
    public static class GridUtils
    {
        public static bool InBounds(int size, GridPosition p) => p.X >= 0 && p.Y >= 0 && p.X < size && p.Y < size;

        public static IEnumerable<GridPosition> CellsForShip(GridPosition bow, int size, Orientation o)
        {
            for (int i = 0; i < size; i++)
            {
                yield return o == Orientation.Horizontal
                    ? new GridPosition { X = bow.X + i, Y = bow.Y }
                    : new GridPosition { X = bow.X, Y = bow.Y + i };
            }
        }

        // с буфером 1 клетка вокруг
        public static IEnumerable<GridPosition> NeighborhoodWithPadding(int gridSize, GridPosition bow, int len,
            Orientation o)
        {
            int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;

            foreach (var c in CellsForShip(bow, len, o))
            {
                minX = Math.Min(minX, c.X);
                maxX = Math.Max(maxX, c.X);
                minY = Math.Min(minY, c.Y);
                maxY = Math.Max(maxY, c.Y);
            }

            minX -= 1;
            minY -= 1;
            maxX += 1;
            maxY += 1;
            
            for (int y = minY; y <= maxY; y++)
            for (int x = minX; x <= maxX; x++)
            {
                var p = new GridPosition() { X = x, Y = y };

                if (InBounds(gridSize, p))
                    yield return p;
            }
        }
    }
}