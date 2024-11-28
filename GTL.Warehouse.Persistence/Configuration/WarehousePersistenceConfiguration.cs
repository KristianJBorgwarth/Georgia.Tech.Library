using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GTL.Warehouse.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace GTL.Warehouse.Persistence.Configuration
{
    public static class WarehousePersistenceConfiguration
    {
        public static IServiceCollection AddWarehousePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionSting = configuration.GetConnectionString("WarehouseDbConnectionString");

            services.AddDbContext<WarehouseDbContext>(options =>
            {
                options.UseSqlServer(connectionSting);
            });

            return services;
        }
    }
}
