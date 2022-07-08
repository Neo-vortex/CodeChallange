using CodeChallenge.Models.Identity;
using MediatR;

namespace CodeChallenge.Services.DataAccess.Queries.Base;

public class GetUser :IRequest<ApplicationUser>
{
    public  string Username { get; set; }
    public GetUser(string username)
    {
        Username = username;
    }
}