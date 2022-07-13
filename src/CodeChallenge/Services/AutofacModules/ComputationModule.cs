using Autofac;
using CodeChallenge.Models.Interfaces;
using CodeChallenge.Services.Computation.BareCPUEngine;
using CodeChallenge.Services.Computation.OpenClEngine;

namespace CodeChallenge.Services.AutofacModules;

public class ComputationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        
        builder.RegisterType<BareCPUEngine>().As<IComputationUnit>();
        /*if (ILGPU.Context.Create(_builder => _builder.AllAccelerators()).Devices.Any())
        {
            builder.RegisterType<OpenClEngine>().As<IComputationUnit>();
        }
        else
        {
            builder.RegisterType<BareCPUEngine>().As<IComputationUnit>();
        }*/
        base.Load(builder);
    }
}