# 0004 — PostgreSQL come database dei servizi

- **Stato:** Accettata
- **Data:** 2026-11-22

## Contesto

Con l'ADR 0001 abbiamo stabilito che ogni servizio ha il proprio database, ma non abbiamo mai deciso quale database utilizzare. Nel README era indicato SQL Server fin dall'inizio, senza che la scelta fosse stata davvero valutata.

Il momento di decidere è adesso, perché Authorization sta per generare la sua prima migration. Le entità e le configurazioni di EF Core non dipendono dal database scelto, le migration invece sì. Finché non ne esiste nessuna, cambiare database significa solo cambiare un pacchetto e una stringa di connessione.

Ci sono alcune caratteristiche del progetto da tenere presenti. Il dominio prevede una parte di gestione territoriale, con particelle catastali e superfici, quindi prima o poi dovremo gestire dati geografici. La gestione documentale avrà metadati che cambiano da un tipo di documento all'altro. Infine, con un database per servizio, il numero di database crescerà insieme ai servizi, e nell'ambiente locale dovranno girare tutti insieme nei container.

## Opzioni valutate

### A — SQL Server

È il database più utilizzato nei sistemi .NET della pubblica amministrazione, e il provider per EF Core è sviluppato direttamente da Microsoft. Sceglierlo renderebbe AgriNexus più vicino ai sistemi reali a cui si ispira.

Il container però è pesante (circa 1,5 GB e almeno 2 GB di RAM) e in produzione la licenza è a pagamento, a meno di accettare i limiti dell'edizione Express. Con più servizi, entrambi i costi crescono. Supporta anche i dati geografici, ma con strumenti molto più limitati rispetto a PostGIS.

### B — PostgreSQL

È open source, il container è leggero ed è offerto come servizio gestito da tutti i principali cloud. Con .NET si utilizza tramite il provider Npgsql, che è maturo e segue le versioni di EF Core.

Ha anche delle estensioni che rispondono a esigenze del dominio: PostGIS per i dati territoriali, che è lo standard in questo campo, e il tipo JSONB per i metadati dei documenti, che si possono indicizzare e interrogare senza dover prevedere una colonna per ogni campo.

Lo svantaggio è che ci allontaniamo da quello che si trova più spesso nei sistemi della pubblica amministrazione.

### C — Scegliere il database servizio per servizio

Avendo un database per servizio, ogni servizio potrebbe usare il database più adatto alle sue esigenze, per esempio SQL Server per Authorization e PostgreSQL per la gestione territoriale.

Il problema è che oggi nessun servizio ha esigenze che solo SQL Server soddisfa, mentre tutto andrebbe gestito due volte: due container, due dialetti SQL, due modi diversi di configurare le migration. Avremmo i costi senza un vantaggio concreto.

Non abbiamo considerato SQLite, perché per un sistema distribuito con accessi concorrenti non è una scelta realistica.

## Decisione

Si sceglie l'opzione B: PostgreSQL per tutti i servizi.

La scelta dipende soprattutto dal dominio: PostGIS sarà necessario per la gestione territoriale e JSONB è utile per la gestione documentale. Pesano anche i costi, perché con un database per servizio sia le licenze sia le risorse dei container si moltiplicano per il numero dei servizi.

## Conseguenze

AgriNexus sarà meno simile ai sistemi .NET della pubblica amministrazione, dove SQL Server è molto diffuso. È un compromesso accettabile, perché i problemi che il progetto vuole affrontare (comunicazione tra servizi, consistenza eventuale, resilienza) non dipendono dal database scelto.

In locale basterà un solo container PostgreSQL per tutti i servizi, con un database separato e credenziali diverse per ciascuno. Questo rispetta comunque l'ADR 0001, perché ogni dato continua ad appartenere a un solo servizio.

PostgreSQL scrive in minuscolo i nomi di tabelle e colonne che non sono tra virgolette, mentre EF Core per default genera nomi come `Utenti` e `UtenteId`. Abbiamo deciso di mantenere i nomi generati da EF Core, così tabelle e colonne hanno gli stessi nomi delle classi e delle proprietà nel codice e non serve un pacchetto in più per convertirli. Di conseguenza, nelle query scritte a mano questi nomi vanno sempre messi tra virgolette, per esempio `SELECT "Username" FROM "Utenti"`.

Finché le migration sono poche e non usano funzionalità specifiche di PostgreSQL, la scelta si può ancora cambiare: basta sostituire il provider e rigenerare le migration. Quando inizieremo a usare PostGIS o JSONB, tornare indietro diventerà molto più costoso.

Se in futuro un servizio avrà esigenze che PostgreSQL non copre, potrà usare un database diverso, ma la scelta andrà motivata con un nuovo ADR.
