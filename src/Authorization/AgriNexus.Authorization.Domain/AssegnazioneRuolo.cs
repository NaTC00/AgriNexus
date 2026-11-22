namespace AgriNexus.Authorization.Domain;

/// <summary>
/// Dice che un utente ha un ruolo su un certo ambito, per esempio istruttore sulla provincia di Firenze.
/// La stessa persona può avere lo stesso ruolo su ambiti diversi (ADR 0003).
/// </summary>
public class AssegnazioneRuolo
{
    public Guid Id { get; set; }
    public Guid UtenteId { get; set; }
    public Utente Utente { get; set; } = null!;
    public Guid RuoloId { get; set; }
    public Ruolo Ruolo { get; set; } = null!;

    // È required anche se è un enum: senza, chi si dimentica di impostarlo otterrebbe 0,
    // cioè Globale, e darebbe il ruolo su tutto il sistema.
    public required TipoAmbito TipoAmbito { get; set; }

    // Il codice dell'ambito, per esempio "FI" per una provincia. Per Globale si usa "*".
    // Non è nullable perché in Postgres due NULL non sono considerati uguali e l'indice univoco
    // lascerebbe passare due assegnazioni globali identiche.
    public required string ValoreAmbito { get; set; }
}
