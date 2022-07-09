using Autofac;
using CodeChallenge.Models.DTOs;
using CodeChallenge.Services.Validators;
using FluentValidation;

namespace CodeChallenge.Services.AutofacModules;

public class ValidatersModule  : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RegisterValidator>().As<IValidator<Register>>();
        builder.RegisterType<LoginValidator>().As<IValidator<Login>>();
        builder.RegisterType<MazeValidator>().As<IValidator<MazeDTO>>();
        base.Load(builder);
    }
}