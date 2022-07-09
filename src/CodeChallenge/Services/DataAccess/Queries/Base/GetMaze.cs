using System.Security.Claims;
using CodeChallenge.Models.Types;
using MediatR;

namespace CodeChallenge.Services.DataAccess.Queries.Base;

public class GetMaze : IRequest<Maze>
{
    public  string MazeId { get; set; }
    public  ClaimsPrincipal User { get; set; }
    public GetMaze(string mazeId, ClaimsPrincipal user)
    {
        MazeId = mazeId;
        User = user;
    }
}