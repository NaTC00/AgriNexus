# Architecture Decision Records

Questa cartella raccoglie le **decisioni architetturali** di AgriNexus: non cosa è stato costruito, ma perché è stato costruito così.

Il codice mostra la soluzione adottata; raramente conserva le alternative scartate e le ragioni della scelta. Quando quel contesto si perde, una decisione presa con criterio diventa indistinguibile da una casuale, e chi la rilegge mesi dopo — anche l'autore — fatica a capire se possa essere messa in discussione.

## Convenzione

Un file per decisione, numerato progressivamente:

```
0001-titolo-della-decisione.md
0002-altra-decisione.md
```

Si parte da [`template.md`](template.md).

## Quando scrivere un ADR

Quando la scelta è **costosa da invertire** o **non ovvia a chi legge il codice**: il meccanismo di trasporto dei messaggi, il modello di autorizzazione, i confini tra bounded context, la strategia di consistenza tra servizi.

Non serve un ADR per le decisioni che il codice spiega da sé, né per quelle reversibili in un pomeriggio.

## Stato di una decisione

Gli ADR non si cancellano e non si riscrivono: sono un registro storico. Una decisione superata resta al suo posto, con lo stato aggiornato e il rimando a quella che la sostituisce.

| Stato | Significato |
|---|---|
| `Proposta` | In valutazione, non ancora adottata |
| `Accettata` | In vigore |
| `Superata` | Sostituita da un ADR successivo (indicarne il numero) |
| `Deprecata` | Non più valida, senza una decisione che la rimpiazzi |

## Indice

| # | Decisione | Stato | Data |
|---|---|---|---|
| — | _nessuna decisione registrata_ | | |
