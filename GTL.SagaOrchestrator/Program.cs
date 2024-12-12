using System.Reflection;
using GTL.Messaging.RabbitMq.Configuration;
using GTL.SagaOrchestrator.Persistence.Context;
using GTL.SagaOrchestrator.Saga.Order;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables()
    .Build();


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();


try
{
    var host = Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(builder =>
        {
            builder.AddConfiguration(configuration);
        })
        .UseSerilog()
        .ConfigureServices(services =>
        {

            #region Persistence

            services.AddDbContext<OrchestratorDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("GTL.SagaOrchestratorDbConnectionString")!,
                    b => b.MigrationsAssembly(typeof(OrchestratorDbContext).Assembly.FullName));
            });

            #endregion

            #region MassTransit (Messaging)

            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));
            services.AddMassTransitWithRabbitMq(Assembly.GetExecutingAssembly(), x =>
            {
                x.AddSagaStateMachine<OrderProcessingSaga, OrderProcessingSagaState>()
                    .EntityFrameworkRepository(r =>
                    {
                        r.ExistingDbContext<OrchestratorDbContext>();
                        r.UseSqlServer();
                    });
            });

            #endregion

        }).Build();

    await host.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "Application start-up failed");
    Console.WriteLine(e);
}
finally
{
    Log.CloseAndFlush();
}