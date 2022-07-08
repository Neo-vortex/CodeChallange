using MediatR;

namespace CodeChallenge.Services.DataAccess.Queries.Base;

public class CheckUserExists : IRequest<bool>
{
    public  string UserName { get; set; }
    public CheckUserExists(){}
}