using CodeChallenge.Models.Interfaces;
using CodeChallenge.Models.Types;
using CodeChallenge.Services.DataAccess.Queries.Base;
using MediatR;

namespace CodeChallenge.Services.DataAccess.Queries;

public class GetAllMazesHandler : IRequestHandler<GetAllMazes , IEnumerable<Maze>>
{
    private readonly IDatabaseManager _dbContext;

    public GetAllMazesHandler(IDatabaseManager dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<IEnumerable<Maze>> Handle(GetAllMazes request, CancellationToken cancellationToken)
    {
        return  Task.Run(() => _dbContext.Users.SingleOrDefault(usr => usr.UserName == request.User.Identity!.Name)?.Mazes!.ToList().AsEnumerable())!;
    }
}