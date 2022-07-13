using CodeChallenge.Models.Structs;
using CodeChallenge.Models.Types;

namespace CodeChallenge.Services.MazeSolver.Utilities;

public static class MazeUtilities
{
  
    public static IEnumerable<MazePoint> CalculateExitPoints(this Maze maze)
    {
        var possibleExitPoints = new List<MazePoint>(((2*maze.Height) + (2 * maze.Width)) );
        for (var i = 0; i < maze.Width; i++)
        {
            possibleExitPoints.Add(new MazePoint(i, maze.Height - 1));
        }
        possibleExitPoints.Remove(maze.Start);
        possibleExitPoints.RemoveAll(point => maze.Walls.Contains(point));
        return possibleExitPoints;
    }


   
}