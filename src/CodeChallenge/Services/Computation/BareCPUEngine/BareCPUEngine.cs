using System.Collections.Concurrent;
using CodeChallenge.Models.Interfaces;
using CodeChallenge.Models.Structs;
using CodeChallenge.Models.Types;
using CodeChallenge.Utilities;

namespace CodeChallenge.Services.Computation.BareCPUEngine;

public class BareCPUEngine: IComputationUnit
{
    public Task<List<MazeNode>> CalculateHeuristics(Maze maze)
    {
        return Task.Run(() =>
        {
            var result = new ConcurrentBag<MazeNode>();
            Parallel.For(0, maze.Height,  (int i) =>
            {
                for (var j = 0; j < maze.Width; j++)
                {
                    var point = new MazePoint(i, j);
                    result.Add(new MazeNode() {Point = point , FScore =point.CalculateFScore(maze) });
                }
            });
            return result.ToList();
        });
        
       
    }
}
