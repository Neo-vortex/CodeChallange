using Autofac;
using CodeChallenge.Models.Identity;
using CodeChallenge.Models.Interfaces;
using CodeChallenge.Services.DataAccess;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CodeChallenge.Services.AutofacModules;

public class DatabaseModule :  Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationDbContext>().As<IdentityDbContext<ApplicationUser>>();
        builder.RegisterType<ApplicationDbContext>().As<IDatabaseManager>();
        
      
        base.Load(builder);
    }
}