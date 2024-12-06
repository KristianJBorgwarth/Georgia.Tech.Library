using GTL.Application.Contracts;
using GTL.Customer.Application.Contracts;
using GTL.Customer.Persistence.Context;
using GTL.Customer.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GTL.Customer.Persistence.Configuration;

public static class CustomerServicePersistenceConfiguration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionSting = configuration.GetConnectionString("CustomerDbConnectionString");

        services.AddDbContext<CustomerServiceDbContext>(options =>
        {
            options.UseSqlServer(connectionSting);
        });

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}