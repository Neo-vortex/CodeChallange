using System.Security.Claims;
using CodeChallenge.Models.DTOs;
using CodeChallenge.Models.Identity;
using MediatR;

namespace CodeChallenge.Services.DataAccess.Commands.Base;

public class AddNewMaze : IRequest<string>
{
    public  MazeDTO Maze { get; set; }
    public  ClaimsPrincipal User { get; set; }
    public AddNewMaze(MazeDTO maze, ClaimsPrincipal user)
    {
        Maze = maze;
        User = user;
    }

}