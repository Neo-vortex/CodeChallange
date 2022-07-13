using CodeChallenge.Models.Interfaces;
using CodeChallenge.Services.DataAccess.Commands.Base;
using MediatR;

namespace CodeChallenge.Services.DataAccess.Commands;

public class UpdatePrimitiveAnalysisHandler : IRequestHandler<UpdatePrimitiveAnalysis>
{
    private readonly IDatabaseManager _databaseManager;

    public UpdatePrimitiveAnalysisHandler(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }


    public Task<Unit> Handle(UpdatePrimitiveAnalysis request, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            var target = _databaseManager.Mazes.SingleOrDefault(mz => mz.Hash == request.Maze.Hash);
            if (target == null) return Unit.Value;
            target.NoSolutionDefinitely = request.Maze.NoSolutionDefinitely;
            target.Heuristics = request.Maze.Heuristics;
            target.PrimitiveAnalysis = request.Maze.PrimitiveAnalysis;
            if (!request.Maze.NoSolutionDefinitely) target.End = request.Maze.End;
            _databaseManager.SaveChanges();
            return Unit.Value;
        }, cancellationToken);
    }
}