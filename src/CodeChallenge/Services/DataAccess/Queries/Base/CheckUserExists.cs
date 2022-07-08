using MediatR;

namespace CodeChallenge.Services.DataAccess.Queries.Base;

public class CheckUserExists : IRequest<bool>
{
    public CheckUserExists(string userName)
    {
        UserName = userName;
    }

    public  string UserName { get; set; }
}