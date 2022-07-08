using CodeChallenge.Models.Identity;
using CodeChallenge.Services.DataAccess.Queries.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CodeChallenge.Services.DataAccess.Queries;

public class CheckUserPasswordHandler : IRequestHandler<CheckUserPassword , bool>
{
    private readonly UserManager<ApplicationUser> _databaseManager;

    public CheckUserPasswordHandler(UserManager<ApplicationUser> databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public Task<bool> Handle(CheckUserPassword request, CancellationToken cancellationToken)
    {
       return _databaseManager.CheckPasswordAsync(new ApplicationUser() { UserName = request.UserName }, request.Password);
    }
}