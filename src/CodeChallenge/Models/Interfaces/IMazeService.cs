using CodeChallenge.Models.Structs;
using CodeChallenge.Models.Types;

namespace CodeChallenge.Models.Interfaces;

public interface IMazeService
{

    public Task<Maze> ApplyPrimitiveAnalysis(Maze maze);
    public  Task<Tuple<Maze,int>> CountExitPoints( Maze maze);
    public  Task<Maze> SolveMinPath(Maze maze);

    public Task<Maze> CalculateHeuristics(Maze maze);
    public  Task<Maze> SolveMaxPath(  Maze maze);
    
}