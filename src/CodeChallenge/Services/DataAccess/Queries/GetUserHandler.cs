using CodeChallenge.Models.Identity;
using CodeChallenge.Services.DataAccess.Queries.Base;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CodeChallenge.Services.DataAccess.Queries;

public class GetUserHandler :  IRequestHandler<GetUser, ApplicationUser>
{
    private readonly IdentityDbContext<ApplicationUser> _databaseManager;

    public GetUserHandler(IdentityDbContext<ApplicationUser> databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public Task<ApplicationUser> Handle(GetUser request, CancellationToken cancellationToken)
    {
        return Task.Run(() => _databaseManager.Users.SingleOrDefault(user => user.UserName == request.Username) ?? throw new KeyNotFoundException("API tries to access a user that does not exist"), cancellationToken);
    }
}