
using System.Text.Json;
using CodeChallenge.Models.Identity;
using CodeChallenge.Models.Structs;
using CodeChallenge.Models.Types;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Services.DataAccess;

public class ApplicationDbContext :  IdentityDbContext<ApplicationUser>
{
    
    public  DbSet<Maze> Mazes { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().Navigation(e => e.Mazes).AutoInclude();
        
        builder.Entity<Maze>().Property(p => p.Walls)
            .HasConversion(
                convertToProviderExpression: v => System.Text.Json.JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<MazePoint[] >(v,new JsonSerializerOptions()));
        
        
        builder.Entity<Maze>().Property(p => p.Start)
            .HasConversion(
                convertToProviderExpression: v => System.Text.Json.JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<MazePoint >(v,new JsonSerializerOptions()));

        
        builder.Entity<Maze>().Property(p => p.End)
            .HasConversion(
                convertToProviderExpression: v => System.Text.Json.JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<MazePoint >(v,new JsonSerializerOptions()));
        builder.Entity<Maze>().Property(p => p.ShortestPath)
            .HasConversion(
                convertToProviderExpression: v => System.Text.Json.JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<MazePoint[] >(v,new JsonSerializerOptions()));

        builder.Entity<Maze>().Property(p => p.LongestPath)
            .HasConversion(
                convertToProviderExpression: v => System.Text.Json.JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<MazePoint[] >(v,new JsonSerializerOptions()));


        
        base.OnModelCreating(builder);
    }
}