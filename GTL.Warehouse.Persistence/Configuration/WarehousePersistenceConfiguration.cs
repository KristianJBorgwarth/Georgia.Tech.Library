using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GTL.Warehouse.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using GTL.Warehouse.Persistence.Repositories;


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

            services.AddScoped<IBookRepository , BookRepository>();

            return services;
        }
    }
}
