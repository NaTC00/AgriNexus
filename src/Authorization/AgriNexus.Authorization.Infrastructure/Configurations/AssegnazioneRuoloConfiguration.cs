using AgriNexus.Authorization.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgriNexus.Authorization.Infrastructure.Configurations;

internal sealed class AssegnazioneRuoloConfiguration : IEntityTypeConfiguration<AssegnazioneRuolo>
{
    public void Configure(EntityTypeBuilder<AssegnazioneRuolo> builder)
    {
        // Salviamo il nome del tipo ("Provincia") invece del numero, così il dato si capisce
        // anche guardando direttamente la tabella.
        builder.Property(ar => ar.TipoAmbito).HasConversion<string>().HasMaxLength(50);
        builder.Property(ar => ar.ValoreAmbito).HasMaxLength(255);

        // Restrict su tutti e due i collegamenti: un utente o un ruolo con assegnazioni non si può
        // cancellare. Prima si revocano le assegnazioni, così nessuno perde dei diritti per sbaglio.
        builder.HasOne(ar => ar.Utente)
            .WithMany(u => u.AssegnazioniRuolo)
            .HasForeignKey(ar => ar.UtenteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Ruolo non ha la lista delle assegnazioni, per questo WithMany è vuoto.
        builder.HasOne(ar => ar.Ruolo)
            .WithMany()
            .HasForeignKey(ar => ar.RuoloId)
            .OnDelete(DeleteBehavior.Restrict);

        // Lo stesso ruolo non si può dare due volte alla stessa persona sullo stesso ambito.
        builder.HasIndex(ar => new { ar.UtenteId, ar.RuoloId, ar.TipoAmbito, ar.ValoreAmbito }).IsUnique();
    }
}

