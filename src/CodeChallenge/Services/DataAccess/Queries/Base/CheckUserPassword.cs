using MediatR;

namespace CodeChallenge.Services.DataAccess.Queries.Base;

public class CheckUserPassword : IRequest<bool>
{
    public  string UserName { get; set; }
    public string Password { get; set; }
    
    public CheckUserPassword(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
    
}