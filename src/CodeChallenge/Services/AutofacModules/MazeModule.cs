using Autofac;
using CodeChallenge.Models.Interfaces;
using CodeChallenge.Services.MazeSolver;

namespace CodeChallenge.Services.AutofacModules;

public class MazeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MazeSolverService>().As<IMazeService>();
        base.Load(builder);
    }
}