using CodeChallenge.Models.Identity;
using CodeChallenge.Services.DataAccess.Queries.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Services.DataAccess.Queries;


public class CheckUserPasswordHandler : IRequestHandler<CheckUserPassword , bool>
{
    private readonly  IdentityDbContext<ApplicationUser> _databaseManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public CheckUserPasswordHandler( IdentityDbContext<ApplicationUser> databaseManager , UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _databaseManager = databaseManager;
    }

    public  Task<bool> Handle(CheckUserPassword request, CancellationToken cancellationToken)
    {
        return  _userManager.CheckPasswordAsync(_databaseManager.Users.FirstOrDefault(user => user.UserName == request.UserName)!, request.Password);
    }
}