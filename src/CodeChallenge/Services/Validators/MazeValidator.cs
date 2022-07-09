using CodeChallenge.Models.DTOs;
using FluentValidation;

namespace CodeChallenge.Services.Validators;

public class MazeValidator :  AbstractValidator<MazeDTO>
{
    public MazeValidator()
    {
        RuleFor(maze => maze.Walls).NotNull().NotEmpty().WithMessage("Walls are required");
        RuleFor(maze => maze.Entrance).NotNull().NotEmpty().WithMessage("Start is required");
        RuleFor(maze => maze.GridSize).NotNull().NotEmpty().WithMessage("GridSize is required");
        
    }
}