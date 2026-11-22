using AgriNexus.Authorization.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgriNexus.Authorization.Infrastructure.Configurations;

internal sealed class RuoloConfiguration : IEntityTypeConfiguration<Ruolo>
{
    public void Configure(EntityTypeBuilder<Ruolo> builder)
    {

        builder.Property(r => r.Codice).IsRequired().HasMaxLength(100);
        builder.Property(r => r.Descrizione).HasMaxLength(255);

        //un ruolo può avere molti permessi e un permesso può essere associato a molti ruoli
        builder.HasMany(r => r.Permessi)
            .WithMany()
            .UsingEntity("RuoliPermessi",
                rp => rp.HasOne(typeof(Permesso)).WithMany().HasForeignKey("PermessoId").OnDelete(DeleteBehavior.Cascade),
                rp => rp.HasOne(typeof(Ruolo)).WithMany().HasForeignKey("RuoloId").OnDelete(DeleteBehavior.Cascade));

        builder.HasIndex(r => r.Codice).IsUnique();
    }
}

