using CodeChallenge.Models.Interfaces;
using CodeChallenge.Models.Structs;
using CodeChallenge.Models.Types;

namespace CodeChallenge.Services.Computation.OpenClEngine;

public class OpenClEngine : IComputationUnit
{
    public Task<List<MazeNode>> CalculateHeuristics(Maze maze)
    {
        throw new NotImplementedException();
    }
}