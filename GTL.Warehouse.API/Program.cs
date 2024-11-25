using System.Reflection;
using MassTransit;
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

#region MassTransit Configuration

builder.Services.AddMassTransit(x =>
{
    // Add consumers here
    x.AddConsumer<ReduceBookQuantityConsumer>();
    x.AddConsumer<CustomerDeleteConsumer>();
    x.AddConsumer<ProcessOrderFailedConsumer>();

    // Configure RabbitMQ as the message transport
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration["RabbitMq:Host"], h =>
        {
            h.Username(configuration["RabbitMq:Username"]);
            h.Password(configuration["RabbitMq:Password"]);
        });

        // Register consumers
        cfg.ReceiveEndpoint("warehouse-queue", e =>
        {
            e.ConfigureConsumer<ReduceBookQuantityConsumer>(context);
            e.ConfigureConsumer<CustomerDeleteConsumer>(context);
            e.ConfigureConsumer<ProcessOrderFailedConsumer>(context);
        });
    });
});

// Add MassTransit hosted service
builder.Services.AddMassTransitHostedService();

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

Log.Information("Warehouse API is starting...");

app.Run();
