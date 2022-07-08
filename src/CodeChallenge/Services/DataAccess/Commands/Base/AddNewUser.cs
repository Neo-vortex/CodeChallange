using CodeChallenge.Models.DTOs;
using MediatR;

namespace CodeChallenge.Services.DataAccess.Commands.Base;

public class AddNewUser : IRequest<bool>
{
    public AddNewUser(Register register)
    {
        Register = register;
    }

    public Register  Register { get; set; }
    
}