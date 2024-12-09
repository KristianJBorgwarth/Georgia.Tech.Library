using System.Reflection;
using GTL.Application.Contracts;
using GTL.Customer.Application.Contracts;
using GTL.Customer.Persistence.BackgroundJobs;
using GTL.Customer.Persistence.Context;
using GTL.Customer.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace GTL.Customer.Persistence.Configuration;

public static class CustomerServicePersistenceConfiguration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration, Assembly assembly)
    {
        var connectionSting = configuration.GetConnectionString("CustomerDbConnectionString");

        services.AddDbContext<CustomerServiceDbContext>(options =>
        {
            options.UseSqlServer(connectionSting);
        });

        services.AddScoped<IOutboxRepository, OutboxRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey($"{nameof(ProcessOutboxMessageJob)}-{assembly.GetName()}");

            configure.AddJob<ProcessOutboxMessageJob>(jobKey)
                .AddTrigger(trigger => trigger.ForJob(jobKey)
                    .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10).RepeatForever()));
        });

        services.AddQuartzHostedService();

        return services;
    }
}