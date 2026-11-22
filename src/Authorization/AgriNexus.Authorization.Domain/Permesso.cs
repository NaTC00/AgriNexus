namespace AgriNexus.Authorization.Domain;

/// <summary>
/// Singola operazione che si può autorizzare.
/// Non si dà mai direttamente a un utente, ma solo attraverso un ruolo.
/// </summary>
public class Permesso
{
    public Guid Id { get; set; }
    public required string Codice { get; set; }
    public string? Descrizione { get; set; }
}
