using CodeChallenge.Models.DTOs;
using FluentValidation;

namespace CodeChallenge.Services.Validators;

public class LoginValidator :  AbstractValidator<Login>
{
    public LoginValidator()
    {
        RuleFor(model => model.Username).NotEmpty().WithMessage("Username is required");
        RuleFor(model => model.Password).NotEmpty().WithMessage("Password is required");
    }
}