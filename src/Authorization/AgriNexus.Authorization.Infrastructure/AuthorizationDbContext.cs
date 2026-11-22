using AgriNexus.Authorization.Domain;
using Microsoft.EntityFrameworkCore;

namespace AgriNexus.Authorization.Infrastructure;

public class AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : DbContext(options)
{

    public DbSet<Utente> Utenti { get; set; }
    public DbSet<Ruolo> Ruoli { get; set; }
    public DbSet<Permesso> Permessi { get; set; }
    public DbSet<AssegnazioneRuolo> AssegnazioniRuolo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Le configurazioni stanno in Configurations/, una classe per entità: qui le carichiamo tutte.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthorizationDbContext).Assembly);
    }
}
