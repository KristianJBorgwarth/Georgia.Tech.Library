using System.Reflection;
using GTL.Application.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GTL.Customer.Application.Configuration;

public static class CustomerApplicationConfiguration
{
    public static IServiceCollection AddCustomerApplication(this IServiceCollection services, Assembly assembly)
    {
        services.AddApplicationLayer(assembly);
        return services;
    }
}