using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });
app.Run();
