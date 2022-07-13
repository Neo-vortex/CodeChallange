using CodeChallenge.Models.Identity;
using CodeChallenge.Models.Types;
using CodeChallenge.Services.DataAccess;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Models.Interfaces;

public abstract class IDatabaseManager :  IdentityDbContext<ApplicationUser>
{
    public abstract   DbSet<Maze> Mazes { get; set; }

    protected IDatabaseManager(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}