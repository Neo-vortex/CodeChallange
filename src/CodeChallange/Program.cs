using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;


#region WebhostBuilding
// This is the main entry point of the application.
// Using the default ASP.NET Core configuration.
var builder = WebApplication.CreateBuilder(args);
#endregion
#region ConfigureLogging
// Configure logging.
// Log to the console and to a file.
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
#endregion
#region Services
// Using Autofac, we can create a container that can be used to resolve dependencies.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register the Autofac services in the container.
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{

    /*builder.RegisterType<AppLogger>().As<IAppLogger>().SingleInstance();
    builder.RegisterType<DataAccess>().As<IDataAccess>().SingleInstance();*/
});
#endregion
#region etc
// Add Controllers to the pipeline.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Add the Swagger generator to the services
builder.Services.AddSwaggerGen();
#endregion
#region Run
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// use https if needed
/*app.UseHttpsRedirection();*/

// use authorization if needed
/*app.UseAuthorization();*/

// Add the routing middleware to the request pipeline.
app.MapControllers();

// Start the application
app.Run();
#endregion

