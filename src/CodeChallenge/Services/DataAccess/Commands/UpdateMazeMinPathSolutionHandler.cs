using CodeChallenge.Models.Interfaces;
using CodeChallenge.Services.DataAccess.Commands.Base;
using MediatR;

namespace CodeChallenge.Services.DataAccess.Commands;

public class UpdateMazeMinPathSolutionHandler : IRequestHandler<UpdateMazeMinPathSolution>
{
    
    private readonly IDatabaseManager _databaseManager;

    public UpdateMazeMinPathSolutionHandler(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    
    public Task<Unit> Handle(UpdateMazeMinPathSolution request, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            var target = _databaseManager.Mazes.SingleOrDefault(mz => mz.Hash == request.Maze.Hash);
            if (target == null) return Unit.Value;
            target.MinSolved = request.Maze.MinSolved;
            target.ShortestPath = request.Maze.ShortestPath;
            target.Image = request.Maze.Image;
            _databaseManager.SaveChanges();
            return Unit.Value;
        }, cancellationToken);
    }
}