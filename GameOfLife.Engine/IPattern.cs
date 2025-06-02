using System.Text;

namespace GameOfLife.Engine
{
    public interface IPattern
    {
        HashSet<(int Row, int Column)> AliveCells { get; set; }

        (int Rows, int Columns) GridSize { get; }
        void Evolve();
        string PrintCurrentState();
    }
}
