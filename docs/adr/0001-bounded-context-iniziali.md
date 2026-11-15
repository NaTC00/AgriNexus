# 0001 — Tre bounded context iniziali, un database per servizio

- **Stato:** Accettata
- **Data:** 2026-11-15

## Contesto

AgriNexus simula il dominio dei finanziamenti agricoli con un modello proprio: entità,
tabelle e colonne sono inventate per questo progetto e non derivano da alcuno schema
esistente.

Gli ambiti funzionali descritti per la piattaforma sono otto. Tradurli meccanicamente in
microservizi produce nove servizi con nove database, nove set di migrazioni, nove
configurazioni di deploy.

Il progetto è di studio e viene portato avanti da una persona sola. Il tempo speso in
impalcatura è tempo non speso sui problemi che il progetto esiste per affrontare:
comunicazione fra servizi, consistenza eventuale, resilienza.

## Opzioni valutate

### A — Nove contesti da subito

Copre tutti gli ambiti e rispecchia la struttura finale. Costo: nove volte l'infrastruttura
prima che un solo flusso funzioni da capo a fondo. Il rischio concreto è che il progetto si
fermi all'impalcatura, con nove servizi che espongono CRUD e nessuna interazione reale.

### B — Un servizio modulare, da spezzare in seguito

Parte in fretta e rimanda i confini. Ma senza confini veri non ci sono né riferimenti
cross-contesto né consistenza eventuale: si costruirebbe un monolite ben organizzato,
che è un esercizio diverso da quello voluto.

### C — Tre contesti ora, gli altri dopo

Authorization, Anagrafiche, Domande.

## Decisione

Si adotta l'opzione C.

Tre contesti sono il minimo che contiene tutte le classi di problema che il progetto vuole
studiare: identità propagata fra servizi (Authorization verso gli altri), riferimento a
un'entità che vive in un altro database senza integrità referenziale (Domande verso
Anagrafiche), comunicazione asincrona con consistenza eventuale fra i due. Il quarto
contesto non aggiunge una classe di problema nuova: aggiunge volume.

Ogni contesto possiede il proprio database. Nessun servizio accede alle tabelle di un altro,
nemmeno in sola lettura.

## Conseguenze

Diventa più facile aggiungere il quarto contesto e i successivi, perché impalcatura, event
bus e convenzioni esisteranno già e saranno stati messi alla prova su un caso vero.

Diventa più difficile ogni interrogazione che attraversa due contesti. Non esiste una JOIN
fra azienda e domanda: ogni lettura combinata va progettata, e costa.

Si rinuncia all'integrità referenziale fra contesti. Il database non può più impedire una
domanda che punta a un'azienda inesistente: quel controllo si sposta nel codice applicativo
e diventa un caso da testare esplicitamente. È la conseguenza più scomoda di questa
decisione ed è bene averla messa per iscritto.

Gli ambiti rimasti fuori — Territorio, Procedimenti, Documentale, Firma digitale, Workflow e
API Gateway — restano in roadmap. Finché Procedimenti non esiste, lo stato di una domanda
vive in Domande; quando nascerà, quella scelta andrà rivista con un ADR nuovo.

Il modo in cui Domande si riferisce a un'azienda di Anagrafiche **non è deciso qui**: sarà
oggetto di un ADR successivo, al momento di costruire Domande. Deciderlo adesso
significherebbe sceglierlo senza il codice davanti.

Reversibilità: aggiungere contesti costa poco. Fonderne due costa una migrazione dei dati e
la rimozione della comunicazione asincrona fra i due — fattibile, ma non gratis.
