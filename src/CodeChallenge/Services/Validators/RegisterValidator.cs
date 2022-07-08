using CodeChallenge.Models.DTOs;
using FluentValidation;

namespace CodeChallenge.Services.Validators;

public class RegisterValidator : AbstractValidator<Register>
{
    public RegisterValidator()
    {
        RuleFor(model => model.Username).NotEmpty() .WithMessage("Username is required");
        RuleFor(model => model.Password).NotEmpty() .WithMessage("Password is required");
        RuleFor(model => model.Password).Matches("[a-z]") .WithMessage("Password must contain at least one lowercase letter");
        RuleFor(model => model.Password).Matches("[A-Z]") .WithMessage("Password must contain at least one uppercase letter");
        RuleFor(model => model.Password).Matches("[0-9]") .WithMessage("Password must contain at least one number");
        RuleFor(model => model.Password).Matches("[^a-zA-Z0-9]") .WithMessage("Password must contain at least one special character");
        RuleFor(model => model.Password).Matches("[a-zA-Z0-9]{8,}") .WithMessage("Password must be at least 8 characters");
    }
}