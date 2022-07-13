using CodeChallenge.Models.Identity;
using CodeChallenge.Models.Interfaces;
using CodeChallenge.Services.DataAccess.Queries.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CodeChallenge.Services.DataAccess.Queries;

public class CheckUserExistsHandler : IRequestHandler<CheckUserExists, bool>
{
    private readonly  IDatabaseManager _databaseManager;

    public CheckUserExistsHandler( IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public Task<bool> Handle(CheckUserExists request, CancellationToken cancellationToken)
    {
        return Task.Run(() => _databaseManager.Users.Any(user => user.UserName == request.UserName), cancellationToken);
    }
}