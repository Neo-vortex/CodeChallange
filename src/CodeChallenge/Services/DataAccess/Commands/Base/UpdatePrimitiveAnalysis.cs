using CodeChallenge.Models.Types;
using MediatR;

namespace CodeChallenge.Services.DataAccess.Commands.Base;

public class UpdatePrimitiveAnalysis : IRequest
{
    public  Maze Maze { get; set; }

    public UpdatePrimitiveAnalysis(Maze maze)
    {
        Maze = maze;
    }
}