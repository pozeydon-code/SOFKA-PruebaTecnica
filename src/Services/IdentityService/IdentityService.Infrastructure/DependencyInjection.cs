using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using IdentityService.Application.Interfaces;
using IdentityService.Infrastructure.Repositories;
using IdentityService.Infrastructure.Persistence;
using IdentityService.Domain.Primitives;

namespace IdentityService.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        // Registrar repositorios
        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
        services.AddScoped<IClienteRepository, ClienteRepository>();
        return services;
    }
}
