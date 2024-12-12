using System.Reflection;
using GTL.Messaging.RabbitMq.Configuration;
using GTL.Messaging.RabbitMq.Messages.OrderMessages;
using GTL.OrderService.API.Services;
using GTL.OrderService.Persistence.Configuration;
using GTL.OrderService.Persistence.Repositories;
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

#region MassTransit (Messaging)

builder.Services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));
builder.Services.AddMassTransitWithRabbitMq(Assembly.GetExecutingAssembly())
    .AddProducer<ProcessOrderRequestMessage>();

#endregion

#region Application Services

builder.Services.AddScoped<IOrderProcessingService, OrderProcessingService>();

#endregion

builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
