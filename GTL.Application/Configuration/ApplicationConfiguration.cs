using System.Reflection;
using FluentValidation;
using GTL.Application.Behaviour;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GTL.Application.Configuration;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}