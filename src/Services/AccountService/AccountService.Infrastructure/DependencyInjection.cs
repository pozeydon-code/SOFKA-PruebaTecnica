using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AccountService.Application.Interfaces;
using AccountService.Infrastructure.Repositories;
using AccountService.Infrastructure.Persistence;
using AccountService.Domain.Primitives;
using AccountService.Infrastructure.Gateways;

namespace AccountService.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        services.AddHttpClient<IClienteGateway, ClienteGateway>(client =>
                {
                    client.BaseAddress = new Uri(configuration["Services:Identity:BaseUrl"]!);
                });
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        // Registrar repositorios
        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
        services.AddScoped<IMovimientoRepository, MovimientoRepository>();
        services.AddScoped<ICuentaRepository, CuentaRepository>();
        return services;
    }
}
