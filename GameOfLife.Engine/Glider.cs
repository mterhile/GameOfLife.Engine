



using System.Text;

namespace GameOfLife.Engine
{
    // Glider pattern concrete implementation - we could implementation to a micro-service
    // so we can easily scale it, persit generation state to data storage, implement caching and may 
    // provide a more robust rendering of generations 
    public class Glider : IPattern
    {
        public HashSet<(int Row, int Column)> AliveCells { get; set; } = new();

        public (int Rows, int Columns) GridSize { get; } = (25, 25);

        private readonly IGameOfLifeService _gameOfLifeService;
        // init glider at center of 25X25 grid
        public Glider(IGameOfLifeService gameOfLifeService)
        {
            _gameOfLifeService = gameOfLifeService ?? throw new ArgumentNullException(nameof(gameOfLifeService));
           
           // because the board can be really big, to improve performance, I decided to track live cells only
            int midRow = GridSize.Rows / 2;
            int midCol = GridSize.Columns / 2;

            AliveCells.Add((midRow, midCol + 1));
            AliveCells.Add((midRow + 1, midCol + 2));
            AliveCells.Add((midRow + 2, midCol));
            AliveCells.Add((midRow + 2, midCol + 1));
            AliveCells.Add((midRow + 2, midCol + 2));
        }

        public string PrintCurrentState()
        {

            var (rows, cols) = GridSize;
            var grid = new char[rows, cols];

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    grid[r, c] = '.';

            foreach (var (r, c) in AliveCells)
                grid[r, c] = 'O';

            var sb = new StringBuilder();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                    sb.Append(grid[r, c]);
                sb.AppendLine();
            }
            return sb.ToString();

        }

        public void Evolve()
        {
            AliveCells = _gameOfLifeService.NextGeneration(AliveCells, GridSize);
        }
    }
}
