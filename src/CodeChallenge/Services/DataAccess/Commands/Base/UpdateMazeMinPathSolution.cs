using CodeChallenge.Models.Types;
using MediatR;

namespace CodeChallenge.Services.DataAccess.Commands.Base;

public class UpdateMazeMinPathSolution : IRequest
{
    public  Maze Maze { get; set; }
    public UpdateMazeMinPathSolution(Maze maze)
    {
        Maze = maze;
    }
}