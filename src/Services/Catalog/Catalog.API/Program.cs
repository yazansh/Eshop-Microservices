using Catalog.API.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddCarter(new CarterDependencyContextAssemblyCatalogCustom());

var app = builder.Build();

app.MapCarter();

app.Run();
