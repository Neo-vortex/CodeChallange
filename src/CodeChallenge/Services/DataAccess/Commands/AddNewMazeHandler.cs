using CodeChallenge.Models.Identity;
using CodeChallenge.Models.Types;
using CodeChallenge.Services.DataAccess.Commands.Base;
using CodeChallenge.Utilities;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Services.DataAccess.Commands;

public class AddNewMazeHandler : IRequestHandler<AddNewMaze, string>
{
    private readonly ApplicationDbContext _databaseManager;
    public AddNewMazeHandler(ApplicationDbContext databaseManager)
    {
        _databaseManager = databaseManager ;
    }

    public async Task<string> Handle(AddNewMaze request, CancellationToken cancellationToken)
    {
        var maze = new Maze( request.Maze.Walls , request.Maze.Entrance, request.Maze.GridSize, request.Maze.Tojson());
        var mazeExist = await _databaseManager.Mazes.AnyAsync(mz => mz.Hash == maze.Hash, cancellationToken: cancellationToken);
        if (mazeExist)
        {
            var targetMaze = _databaseManager.Mazes.Single(mz => mz.Hash == maze.Hash);
            var mazes = _databaseManager.Users.Single(usr => usr.UserName == request.User.Identity!.Name).Mazes;
            if (!mazes!.Contains(targetMaze))
            {
                mazes.Add(targetMaze);
            }
            await _databaseManager.SaveChangesAsync(cancellationToken);
            return targetMaze.Hash;
        }
        _databaseManager.Users.Single(usr => usr.UserName == request.User.Identity!.Name).Mazes?.Add(maze);
        await _databaseManager.SaveChangesAsync(cancellationToken);
        return maze.Hash;
    }
}