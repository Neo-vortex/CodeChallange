using Autofac;
using CodeChallenge.Models.Interfaces;
using CodeChallenge.Services.JWT;

namespace CodeChallenge.Services.AutofacModules;

public class JWTModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<BasicJWT>().As<IJWTservice>().SingleInstance();
        base.Load(builder);
    }
}