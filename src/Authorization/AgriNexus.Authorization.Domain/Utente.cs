namespace AgriNexus.Authorization.Domain;

/// <summary>
/// Persona che accede alla piattaforma. I suoi diritti non stanno qui ma nelle assegnazioni di ruolo.
/// </summary>
public class Utente
{
    public Guid Id { get; set; }
    public required string Username { get; set; }

    // Per togliere l'accesso a qualcuno si disattiva l'utente: cancellarlo non si può
    // finché ha delle assegnazioni.
    public required bool Attivo { get; set; }

    // Si riempie solo con Include. Se è vuota non vuol dire per forza che l'utente non abbia ruoli.
    public ICollection<AssegnazioneRuolo> AssegnazioniRuolo { get; set; } = [];
}