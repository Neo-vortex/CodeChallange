using System.Security.Claims;
using CodeChallenge.Models.Types;
using MediatR;

namespace CodeChallenge.Services.DataAccess.Queries.Base;

public class GetAllMazes : IRequest<IEnumerable<Maze>>
{
    public GetAllMazes(ClaimsPrincipal user)
    {
        User = user;
    }

    public  ClaimsPrincipal User { get; set; }
}