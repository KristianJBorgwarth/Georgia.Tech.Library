using GTL.Customer.Persistence.Context;
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

        return services;
    }
}