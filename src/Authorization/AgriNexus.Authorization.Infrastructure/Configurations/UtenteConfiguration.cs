using AgriNexus.Authorization.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgriNexus.Authorization.Infrastructure.Configurations;

internal sealed class UtenteConfiguration : IEntityTypeConfiguration<Utente>
{
    public void Configure(EntityTypeBuilder<Utente> builder)
    {

        builder.Property(u => u.Username).HasMaxLength(100);
        builder.HasIndex(u => u.Username).IsUnique();
    }
}
