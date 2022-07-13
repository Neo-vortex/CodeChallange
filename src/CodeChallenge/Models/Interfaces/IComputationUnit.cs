using CodeChallenge.Models.Structs;
using CodeChallenge.Models.Types;

namespace CodeChallenge.Models.Interfaces;

public interface IComputationUnit
{
 public   Task<List<MazeNode>>  CalculateHeuristics(Maze maze);
}