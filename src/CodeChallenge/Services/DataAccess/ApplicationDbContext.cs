using CodeChallenge.Models;
using CodeChallenge.Models.DTOs;
using CodeChallenge.Models.Identity;
using CodeChallenge.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Services.DataAccess;

public class ApplicationDbContext :  IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}