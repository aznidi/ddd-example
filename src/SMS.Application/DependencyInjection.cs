using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace SMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Register MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(Common.Behaviors.ValidationBehavior<,>));
        });

        // Register FluentValidation validators
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
