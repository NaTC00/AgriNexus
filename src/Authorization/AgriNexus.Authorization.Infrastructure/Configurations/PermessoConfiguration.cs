using AgriNexus.Authorization.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgriNexus.Authorization.Infrastructure.Configurations;

internal sealed class PermessoConfiguration : IEntityTypeConfiguration<Permesso>
{
    public void Configure(EntityTypeBuilder<Permesso> builder)
    {
        builder.Property(p => p.Codice).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Descrizione).HasMaxLength(255);

        // Gli altri servizi controllano i permessi per codice, quindi due permessi con lo stesso
        // codice sarebbero ambigui.
        builder.HasIndex(p => p.Codice).IsUnique();
    }
}
