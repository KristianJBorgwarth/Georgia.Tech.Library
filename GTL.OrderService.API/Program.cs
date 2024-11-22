using System.Reflection;
using GTL.OrderService.Persistence.Configuration;
using Serilog;

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

#region Persistence

builder.Services.AddOrderServicePersistence(configuration);

#endregion


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

Log.Information("Order API is starting...");

app.Run();