using IdentityService.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
        });

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
        );

        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();
        return services;
    }

}
