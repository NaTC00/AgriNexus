namespace AgriNexus.Authorization.Domain;

/// <summary>
/// Insieme di permessi che si assegna agli utenti.
/// </summary>
public class Ruolo
{
    public Guid Id { get; set; }
    public required string Codice { get; set; }
    public string? Descrizione { get; set; }
    public ICollection<Permesso> Permessi { get; set; } = [];
}
