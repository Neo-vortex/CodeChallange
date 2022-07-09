using CodeChallenge.Models.Types;
using CodeChallenge.Services.DataAccess.Queries.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CodeChallenge.Services.DataAccess.Queries;

public class GetMazeHandler : IRequestHandler<GetMaze , Maze?>
{
    private readonly ApplicationDbContext _databaseManager;

    public GetMazeHandler(ApplicationDbContext databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public Task<Maze?> Handle(GetMaze request, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            var collection = _databaseManager.Users.SingleOrDefault(usr => usr.UserName == request.User.Identity!.Name)?.Mazes;
            return collection?.SingleOrDefault(mz => mz.Hash == request.MazeId);
        });
    }
}