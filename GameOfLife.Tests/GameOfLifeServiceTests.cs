using FluentAssertions;
using GameOfLife.Engine;

namespace GameOfLife.Tests
{
    public class GameOfLifeServiceTests
    {
        [Fact]
        public void Test_Still_Life() // still life on a 5X5 universe 
        {
            //Arrange
            var sut = new GameOfLifeService();
            var expected = new HashSet<(int, int)>()
            {
                (1,1), (1,2),
                (2,1), (2,2)
            };

            //Act
            var response = sut.NextGeneration(new HashSet<(int, int)>()
            {
                (1,1), (1,2),
                (2,1), (2,2)
            }, new(3, 3));

            //Assert
            response.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public void Test_Under_Population()
        {
            //Arrange
            var sut = new GameOfLifeService();
            var expected = new HashSet<(int, int)>();

            //Act
            var response = sut.NextGeneration(new HashSet<(int, int)>()
            {
               (2,2)
            }, new(5, 5));

            //Assert
            response.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public void Test_Over_Population()
        {
            //Arrange
            var sut = new GameOfLifeService();
            var expected = new HashSet<(int, int)>()
            {
                (0,0),(0,2),
                (2,0),(2,2),
            };

            //Act
            var response = sut.NextGeneration(new HashSet<(int, int)>()
            {
               (0,0), (0,1),(0,2),
               (1,0),(1,1), (1,2),
               (2,0),(2,1),(2,2)
            }, new(3, 3));

            //Assert
            response.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public void NoLiveCells_Return()
        {
            //Arrange
            var sut = new GameOfLifeService();

            //Act
            var response = sut.NextGeneration(new HashSet<(int, int)>(), new(0, 0));

            //Assert
            response.Should().BeEmpty();
        }

        [Fact]
        public void CellOfBound_Return()
        {
            //Arrange
            var sut = new GameOfLifeService();

            //Act
            var response = sut.NextGeneration(new HashSet<(int, int)>() { new(10, 10) }, new(0, 0));

            //Assert
            response.Should().BeEmpty();
        }
    }
}