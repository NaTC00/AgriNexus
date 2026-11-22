using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace AgriNexus.Authorization.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registra il DbContext su PostgreSQL. La stringa di connessione si chiama "Authorization"
    /// e in sviluppo si trova negli user-secrets del progetto Host.
    /// </summary>
    public static IServiceCollection AddAuthorizationInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthorizationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Authorization")));

        services.AddHealthChecks().AddDbContextCheck<AuthorizationDbContext>();

        return services;
    }
}
