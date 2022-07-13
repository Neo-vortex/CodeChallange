using CodeChallenge.Models.Types;
using MediatR;

namespace CodeChallenge.Services.DataAccess.Commands.Base;

public class UpdateMazeMaxPathSolution : IRequest
{
    public Maze Maze { get; set; }
    public UpdateMazeMaxPathSolution(Maze maze)
    {
        Maze = maze;
    }
}