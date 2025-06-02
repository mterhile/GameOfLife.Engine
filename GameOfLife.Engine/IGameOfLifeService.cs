namespace GameOfLife.Engine
{
    public interface IGameOfLifeService
    {
        HashSet<(int, int)> NextGeneration(HashSet<(int, int)> aliveCells, (int, int) gridSize);
    }
}
