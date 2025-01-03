using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region Configuration

var env = builder.Environment;

var configuration = builder.Configuration;
configuration.AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables();

if (env.IsDevelopment())
{
    configuration.AddJsonFile($"appsettings.{Environments.Development}.json", true, true);
    configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
}
#endregion

#region Logger

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();
builder.Host.UseSerilog();
#endregion

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Middleware til at logge anmodninger
app.Use(async (context, next) =>
{
    Log.Information("Received API request: {Method} {Path}", context.Request.Method, context.Request.Path);

    // Tjekker om anmodningen matcher specifikke kriterier


    if (context.Request.Path.StartsWithSegments("/api"))
    {
        // Log til Serilog; automatisk sendt til Seq ifølge konfiguration
        Log.Information("Received API request: {Method} {Path}", context.Request.Method, context.Request.Path);
    }

    // Fortsæt med næste middleware (inklusiv Ocelot)
    await next();
});


await app.UseOcelot();
app.Run();
