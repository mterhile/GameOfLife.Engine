using System.Collections.Concurrent;

namespace GameOfLife.Engine
{
    public class GameOfLifeService : IGameOfLifeService
    {
        // live cells neighbors
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

            // For each alive cell, iterate through its 8 neighboring positions.
            // For each neighbor, if it lies within the grid bounds, increment its count in the neighborCounts dictionary.
            // This helps track how many live neighbors each cell (alive or dead) has,
            // which is used to determine the next state of the grid in the Game of Life.
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

                // cell is currently not alive, but has exactly 3 live neighbors
                if (count == 3 && !isAlive)  // reproduction
                {
                    nextGeneration.Add(cell);
                }     
                else if ((count == 2 || count == 3) && isAlive)  // survival
                {
                    nextGeneration.Add(cell);
                }

            }
            return nextGeneration;
        }
        private bool CellInGrid(int row, int column, (int row, int column) gridSize)
        {
            return row >= 0 && row < gridSize.row && column >= 0 && column < gridSize.column;
        }

    }
}
