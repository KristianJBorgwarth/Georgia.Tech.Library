using System.Reflection;
using GTL.Messaging.RabbitMq.Configuration;
using GTL.Messaging.RabbitMq.Messages.BookMessages;
using GTL.Messaging.RabbitMq.Messages.CustomerMessages;
using GTL.Messaging.RabbitMq.Messages.OrderMessages;
using GTL.Warehouse.API.Messages.BookCreatedMessage;
using GTL.Warehouse.Persistence.Configuration;
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

builder.Services.AddWarehousePersistence(configuration);

#endregion

#region MassTransit (Messaging)

builder.Services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));
builder.Services.AddMassTransitWithRabbitMq(Assembly.GetExecutingAssembly())
    .AddProducer<BookCreatedMessage>().
    AddProducer<BookQuantityChangedMessage>();
   // .AddProducer<CustomerDeletedMessage>();
    
    

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

Log.Information("Warehouse API is starting...");

app.MapControllers();

app.Run();
