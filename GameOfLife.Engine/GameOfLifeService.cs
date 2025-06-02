using System.Collections.Concurrent;

namespace GameOfLife.Engine
{
    public class GameOfLifeService : IGameOfLifeService
    {
        private static readonly (int deltaRow, int deltaColum)[] _neighbors =
        [
            (-1, -1),
            (-1, 0),
            (-1, 1),
            (0,-1),
            (0,1),
            (1,-1),
            (1,0),
            (1,1)
        ];

        public HashSet<(int, int)> NextGeneration(HashSet<(int, int)> aliveCells, (int, int) gridSize)
        {
            var neighborCounts = new Dictionary<(int, int), int>();
            var nextGeneration = new HashSet<(int, int)>(aliveCells.Count);

            if (aliveCells.Count < 1) // dead cells have no impact
            {
                return nextGeneration;
            }
            foreach (var (row, column) in aliveCells)
            {
                foreach (var (deltaRow, deltaColum) in _neighbors)
                {
                    var newRow = row + deltaRow;
                    var newColumn = column + deltaColum;
                    if (!CellInGrid(newRow, newColumn, gridSize)) // cell is out of bounds
                    {
                        continue;
                    }
                    var key = (newRow, newColumn);
                    neighborCounts[key] = neighborCounts.GetValueOrDefault(key) + 1; //increment this neighbor live cells count

                }
            }
            foreach (var (cell, count) in neighborCounts)
            {
                bool isAlive = aliveCells.Contains(cell);

                if (count == 3 && !isAlive) nextGeneration.Add(cell);      // Reproduction
                else if ((count == 2 || count == 3) && isAlive) nextGeneration.Add(cell); // Survival

            }
            return nextGeneration;
        }
        private bool CellInGrid(int row, int column, (int row, int column) gridSize)
        {
            return row >= 0 && row < gridSize.row && column >= 0 && column < gridSize.column;
        }

    }
}
