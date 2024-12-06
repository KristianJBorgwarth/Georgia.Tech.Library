using GTL.Customer.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GTL.Customer.Domain.Configuration;

public static class CustomerDomainConfiguration
{
    public static void AddCustomerDomain(this IServiceCollection services)
    {
        services.AddScoped<ICustomerLifeCycleService, CustomerLifeCycleService>();
    }
}