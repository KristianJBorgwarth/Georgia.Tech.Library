using GTL.OrderService.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GTL.OrderService.Persistence.Configuration;

public static class OrderServicePersistenceConfiguration
{
    public static IServiceCollection AddOrderServicePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionSting = configuration.GetConnectionString("OrderServiceDbConnectionString");

        services.AddDbContext<OrderServiceDbContext>(options =>
        {
            options.UseSqlServer(connectionSting);
        });

        return services;
    }
}