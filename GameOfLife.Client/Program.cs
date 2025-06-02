// See https://aka.ms/new-console-template for more information
using GameOfLife.Engine;


var gameofLife = new GameOfLifeService();
var gliderPattern = new Glider(gameofLife);
int generationsCount =0;


Console.WriteLine("=====================");
Console.WriteLine("Initial state");
Console.WriteLine(gliderPattern.PrintCurrentState());

while (gliderPattern.AliveCells.Count > 0)
{
    generationsCount++;
    Console.WriteLine("=====================");
    Console.WriteLine($"Generation: {generationsCount}");
    gliderPattern.Evolve();
    Console.WriteLine(gliderPattern.PrintCurrentState());
    Console.WriteLine("=====================");
}

Console.WriteLine("done");