using CodeChallenge.Models.Identity;
using CodeChallenge.Models.Interfaces;
using CodeChallenge.Services.DataAccess.Queries.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CodeChallenge.Services.DataAccess.Queries;


public class CheckUserPasswordHandler : IRequestHandler<CheckUserPassword , bool>
{
    private readonly  IDatabaseManager _databaseManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public CheckUserPasswordHandler( IDatabaseManager databaseManager , UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _databaseManager = databaseManager;
    }

    public  Task<bool> Handle(CheckUserPassword request, CancellationToken cancellationToken)
    {
        return  _userManager.CheckPasswordAsync(_databaseManager.Users.FirstOrDefault(user => user.UserName == request.UserName)!, request.Password);
    }
}