using Lab11_ZeaBurga.Domain.Interfaces;
using Lab11_ZeaBurga.Infrastructure.Persistence;
using Lab11_ZeaBurga.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab11_ZeaBurga.Infrastructure;

/// <summary>
/// Clase estática para registrar todos los servicios
/// de la capa de Infrastructure (DbContext, Repos, etc.)
/// </summary>
public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // 1. Registrar el DbContext (Movimos esto de Program.cs aquí)
        services.AddDbContext<TicketeraDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        // 2. Registrar el UnitOfWork
        // Usamos AddScoped para que el UoW (y su DbContext)
        // vivan durante todo el ciclo de vida de un request HTTP.
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // 3. Registrar el Repositorio Genérico
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}