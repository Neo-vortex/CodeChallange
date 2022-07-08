using CodeChallenge.Models.Types;
using Microsoft.AspNetCore.Identity;

namespace CodeChallenge.Models.Identity;

public class ApplicationUser : IdentityUser
{
    public ICollection<Maze>? Mazes { get; set; }
}