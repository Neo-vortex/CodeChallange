using CodeChallenge.Models.Identity;
using CodeChallenge.Services.DataAccess.Queries.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CodeChallenge.Services.DataAccess.Queries;

public class CheckUserExistsHandler : IRequestHandler<CheckUserExists, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CheckUserExistsHandler(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
    }

    public Task<bool> Handle(CheckUserExists request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_userManager.Users.Any(usr => usr.UserName == request.UserName));
    }
}