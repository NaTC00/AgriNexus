namespace AgriNexus.Authorization.Domain;

/// <summary>
/// Tipo di perimetro su cui vale un'assegnazione di ruolo.
/// </summary>
/// <remarks>
/// Nel database viene salvato il nome e non il numero: se si rinomina un valore bisogna
/// aggiornare anche le righe già salvate.
/// </remarks>
public enum TipoAmbito
{
    // Vale su tutto il sistema. In questo caso ValoreAmbito è "*".
    Globale = 0,
    Azienda = 1,
    Provincia = 2,
}