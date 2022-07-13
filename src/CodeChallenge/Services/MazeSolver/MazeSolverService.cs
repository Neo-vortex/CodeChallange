using System.Collections.Concurrent;
using CodeChallenge.Models.Interfaces;
using CodeChallenge.Models.Structs;
using CodeChallenge.Models.Types;
using CodeChallenge.Services.MazeSolver.Utilities;
using CodeChallenge.Utilities;

namespace CodeChallenge.Services.MazeSolver;

public class MazeSolverService : IMazeService
{
    private readonly IComputationUnit _computationUnit;

    public MazeSolverService(IComputationUnit computationUnit)
    {
        _computationUnit = computationUnit;
    }

    public async Task<Maze> ApplyPrimitiveAnalysis(Maze maze)
    {
       var result =   await CalculateHeuristics( (await CountExitPoints(maze)).Item1);
       result.PrimitiveAnalysis = true;
       return result;
    }

    public Task<Tuple<Maze,int>> CountExitPoints( Maze maze)
    {
        return Task.Run(() =>
        {
            if (maze.NoSolutionDefinitely)
            {
                return new Tuple<Maze, int>(maze,0);
            }

            var ExitPoit = new MazePoint();
            var exitCount = 0;
            var alreadyChecked = new ConcurrentBag<MazePoint>() {maze.Start};
            var currentPoints = new List<MazePoint>() {maze.Start};
            var nextPoints = new ConcurrentBag<MazePoint>();
            var possibleExitPoints =maze.CalculateExitPoints().ToList();
            do
            {
                Parallel.ForEach(currentPoints, point =>
                {
                    var neighbours = maze.CalculateNeighbours(point);
                    foreach (var neighbor in neighbours)
                    {
                        if (alreadyChecked.Contains(neighbor)) continue;
                        if (possibleExitPoints.Contains(neighbor))
                        {
                            Interlocked.Increment(ref exitCount) ;
                            ExitPoit = neighbor;
                        }
                        else
                        {
                            nextPoints.Add(neighbor);
                        }
                        alreadyChecked.Add(neighbor);
                    }
                });
                currentPoints = nextPoints.ToList();
                nextPoints.Clear();
            } while (currentPoints.Any());
            maze.NoSolutionDefinitely = exitCount != 1 ;
            if (!maze.NoSolutionDefinitely)
            {
                maze.End = ExitPoit;
            }
            return new Tuple<Maze, int>(maze,exitCount);


        });
    }

    public Task<Maze> SolveMinPath(Maze maze)
    {
        return Task.Run(() =>
        {
            if (maze.MinSolved)
            {
                return maze;
            }
            var Current= new MazeNode();
            var openList = new List<MazeNode>{ new() {Parrent = null , Point = maze.Start , FScore = maze.Start.CalculateFScore(maze)}};
            var closeList = new List<MazeNode>();
            while (openList.Any() && !closeList.Any(node => node.Point.Equals(maze.End)))
            { 
             openList.Sort((node1, node2) => node1.FScore.CompareTo(node2));
             Current = openList.First();
             closeList.Add(Current);
             openList.Remove(Current);
             var neighbours = maze.CalculateNeighbours(Current.Point);
             openList.AddRange(neighbours.Select(point => new MazeNode{Parrent = Current , Point = point , FScore = point.CalculateFScore(maze)}));
             openList = openList.Except(closeList, new MazeNode.MazeNodeEqualityComparer()).ToList();
            }
            if (!closeList.Any(node => node.Point.Equals(maze.End))) return maze;
            var finalPath = new List<MazeNode>();
            var currentParent = Current;
            while (currentParent != null)
            {
                finalPath.Add(currentParent);
                currentParent = currentParent.Parrent;
            }
            finalPath.Reverse();
            maze.ShortestPath = finalPath.Select(node => node.Point).ToArray();
            maze.MinSolved = true;
            maze.GenerateImage();
            return maze;
        });
    }

    public async Task<Maze> CalculateHeuristics(Maze maze)
    {
        if (maze.NoSolutionDefinitely) return maze;
        maze.Heuristics = (await _computationUnit.CalculateHeuristics(maze)).ToArray<MazePointHeuristic>();
        return maze;
    }

    public Task<Maze> SolveMaxPath(Maze maze)
    {
        return Task.Run(() =>
        {
            if (maze.MaxSolved)
            {
                return maze;
            }
            var Current= new MazeNode();
            var openList = new List<MazeNode>{ new() {Parrent = null , Point = maze.Start , FScore = maze.Start.CalculateFScore(maze)}};
            var closeList = new List<MazeNode>();
            while (openList.Any() && !closeList.Any(node => node.Point.Equals(maze.End)))
            { 
                openList.Sort((node1, node2) => node1.FScore.CompareTo(node2));
                Current = openList.Last();
                closeList.Add(Current);
                openList.Remove(Current);
                var neighbours = maze.CalculateNeighbours(Current.Point);
                openList.AddRange(neighbours.Select(point => new MazeNode{Parrent = Current , Point = point , FScore = point.CalculateFScore(maze)}));
                openList = openList.Except(closeList, new MazeNode.MazeNodeEqualityComparer()).ToList();
            }
            if (!closeList.Any(node => node.Point.Equals(maze.End))) return maze;
            var finalPath = new List<MazeNode>();
            var currentParent = Current;
            while (currentParent != null)
            {
                finalPath.Add(currentParent);
                currentParent = currentParent.Parrent;
            }
            finalPath.Reverse();
            maze.LongestPath = finalPath.Select(node => node.Point).ToArray();
            maze.MaxSolved = true;
            maze.GenerateImage();
            return maze;
        });
    }
}