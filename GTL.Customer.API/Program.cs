using System.Reflection;
using GTL.Customer.Application.Configuration;
using GTL.Customer.Domain.Configuration;
using GTL.Customer.Domain.Events;
using GTL.Customer.Persistence.Configuration;
using GTL.Messaging.RabbitMq.Configuration;
using GTL.Messaging.RabbitMq.Messages.CustomerMessages;
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

#region MassTransit (Messaging)

builder.Services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));
builder.Services.AddMassTransitWithRabbitMq(Assembly.GetExecutingAssembly())
    .AddProducer<CustomerDeletedMessage>();

#endregion

#region Domain Layer

builder.Services.AddCustomerDomain();

#endregion

#region Application Layer

builder.Services.AddCustomerApplication(Assembly.Load("GTL.Customer.Application"));

#endregion

#region Persistence

builder.Services.AddPersistence(configuration, Assembly.GetExecutingAssembly());

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

