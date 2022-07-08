using CodeChallenge.Models.Identity;
using CodeChallenge.Models.Interfaces;
using CodeChallenge.Services.DataAccess.Commands.Base;
using MediatR;
using CodeChallenge.Models;
using CodeChallenge.Models.DTOs;
using CodeChallenge.Models.Identity;
using CodeChallenge.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Services.DataAccess.Commands;

public class AddNewUserHandler :IRequestHandler<AddNewUser, bool>
{
    private readonly UserManager<ApplicationUser> _databaseManager;

    public AddNewUserHandler(UserManager<ApplicationUser>  databaseManager)
    {
        _databaseManager = databaseManager;
    }


    public async Task<bool> Handle(AddNewUser request, CancellationToken cancellationToken)
    {
       var result = await _databaseManager.CreateAsync(new ApplicationUser() {UserName = request.Register.Username}, request.Register.Password);
       return result.Succeeded;
    }
}