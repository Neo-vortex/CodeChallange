using CodeChallenge.Models.Interfaces;
using CodeChallenge.Models.Types;
using CodeChallenge.Services.DataAccess.Commands.Base;
using CodeChallenge.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Services.DataAccess.Commands;

public class AddNewMazeHandler : IRequestHandler<AddNewMaze, string>
{
    private readonly IDatabaseManager _databaseManager;
    public AddNewMazeHandler(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager ;
    }

    public async Task<string> Handle(AddNewMaze request, CancellationToken cancellationToken)
    {
        var maze = new Maze( request.Maze.Walls , request.Maze.Entrance, request.Maze.GridSize, request.Maze.Tojson());
        var mazeExist =  _databaseManager.Mazes  != null &&  await _databaseManager.Mazes.AnyAsync(mz => mz.Hash == maze.Hash, cancellationToken: cancellationToken);
        if (mazeExist)
        {
            var targetMaze = _databaseManager.Mazes.Single(mz => mz.Hash == maze.Hash);
            if (_databaseManager.Users.Single(usr => usr.UserName == request.User.Identity!.Name).Mazes!.Any(mz => mz.Hash == maze.Hash)) return targetMaze.Hash;
            _databaseManager.Users.Single(usr => usr.UserName == request.User.Identity!.Name).Mazes!.Add(targetMaze);
            await _databaseManager.SaveChangesAsync(cancellationToken);
            return targetMaze.Hash;
        }
        _databaseManager.Users.Single(usr => usr.UserName == request.User.Identity!.Name).Mazes?.Add(maze);
        await _databaseManager.SaveChangesAsync(cancellationToken);
        return maze.Hash;
    }
}