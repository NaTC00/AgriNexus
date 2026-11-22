# AgriNexus

**Enterprise Agricultural Information System** — piattaforma distribuita per la gestione integrata di procedimenti amministrativi, aziende agricole, beneficiari e interventi finanziati nell'ambito delle politiche agricole e dello sviluppo rurale.

> ⚠️ **Progetto dimostrativo.** Tutti i dati, le anagrafiche, le organizzazioni, i beneficiari e i procedimenti sono **completamente fittizi** e creati esclusivamente per finalità di sviluppo, studio e sperimentazione. AgriNexus non è collegato ad alcun ente pubblico reale né ne riproduce sistemi in esercizio.

---

## Obiettivo

Ricostruire un ecosistema applicativo enterprise analogo a quelli impiegati dalle pubbliche amministrazioni per la gestione dei finanziamenti al settore primario, riproducendone i domini applicativi, le relazioni tra entità, i processi di business e le integrazioni tra sistemi.

Il progetto è un ambiente concreto di sperimentazione su **architetture software distribuite**: microservizi, Domain-Driven Design, comunicazione event-driven, resilienza e consistenza eventuale.

---

## Domini applicativi

| Dominio | Responsabilità |
|---|---|
| **Anagrafiche** | Beneficiari, aziende agricole, soggetti giuridici, rappresentanti, fascicoli aziendali |
| **Gestione territoriale** | Terreni, particelle catastali, superfici agricole, conduzioni e occupazioni del suolo |
| **Domande e finanziamenti** | Presentazione, gestione e consultazione delle domande di contributo e degli interventi finanziati |
| **Procedimenti amministrativi** | Iter, istruttorie, verifiche, autorizzazioni, chiusura delle pratiche |
| **Gestione documentale** | Generazione, archiviazione, consultazione e distribuzione dei documenti amministrativi |
| **Firma digitale** | Processi di firma, documenti da sottoscrivere, gestione degli esiti |
| **Workflow e notifiche** | Orchestrazione dei processi, gestione degli stati, comunicazioni agli utenti |
| **Integrazioni** | Comunicazione con sistemi esterni simulati via API REST e messaggistica asincrona |

---

## Architettura

L'architettura è basata su **microservizi indipendenti**, organizzati per domini funzionali e progettati secondo i principi del Domain-Driven Design.

Principi guida:

- **Clean Architecture** e separazione netta tra dominio, applicazione e infrastruttura
- **Domain-Driven Design**: aggregati, value object, domain event, bounded context espliciti
- **SOLID** e Separation of Concerns
- **Database per servizio**: nessuna condivisione di persistenza tra microservizi
- **Comunicazione esplicita**: API REST per le interazioni sincrone, eventi di integrazione per quelle asincrone
- **Consistenza eventuale**, idempotenza e resilienza come requisiti di progetto, non come aggiunte successive

Ogni servizio possiede una responsabilità funzionale ben definita, un proprio modello di dominio, una propria persistenza e contratti di comunicazione espliciti.

---

## Stack tecnologico

**Piattaforma**
- .NET 10 (LTS) / C#
- ASP.NET Core — API REST documentate con OpenAPI

**Persistenza**
- Entity Framework Core
- PostgreSQL (un database indipendente per microservizio, [ADR 0004](docs/adr/0004-postgresql-come-database.md))

**Messaggistica e integrazione**
- RabbitMQ — comunicazione event-driven
- Event bus per gli integration event tra bounded context

**Sicurezza**
- Autenticazione JWT
- Gestione centralizzata delle autorizzazioni

**Infrastruttura e operabilità**
- Docker e containerizzazione
- Logging strutturato e distributed tracing
- Gestione centralizzata delle eccezioni
- Retry, circuit breaker e politiche di resilienza

**Qualità**
- Unit test, integration test e test end-to-end
- CI/CD con automazione di build e deploy

---

## Stato del progetto

🚧 **In fase di avvio.** Lo sviluppo procede in modo incrementale: ogni microservizio viene introdotto con il proprio modello di dominio, la propria persistenza e i propri contratti, verificando di volta in volta i flussi distribuiti, la gestione degli errori e la consistenza dei dati.

Lo stato corrente e la prossima attività sono tracciati in [`docs/DIARIO.md`](docs/DIARIO.md); la pianificazione operativa vive nel GitHub Project del repository.

### Roadmap

- [ ] Definizione dei bounded context e dei contratti di integrazione
- [ ] Impianto della solution e delle convenzioni di progetto (Clean Architecture, cross-cutting concerns)
- [ ] Microservizio **Authorization** — utenti, ruoli, permessi, emissione e validazione dei JWT
- [ ] API Gateway e propagazione dell'identità tra i servizi
- [ ] Microservizio **Anagrafiche** — beneficiari, aziende agricole, fascicoli
- [ ] Microservizio **Gestione territoriale** — particelle, superfici, conduzioni
- [ ] Microservizio **Domande e finanziamenti**
- [ ] Microservizio **Procedimenti amministrativi** — istruttorie e workflow
- [ ] Event bus e integration event tra i servizi
- [ ] Gestione documentale e firma digitale
- [ ] Containerizzazione e orchestrazione dell'ambiente locale
- [ ] Osservabilità: logging strutturato, tracing, health check
- [ ] Pipeline CI/CD

---

## Requisiti

- [.NET SDK](https://dotnet.microsoft.com/download) 10.0
- [Docker](https://www.docker.com/) e Docker Compose
- PostgreSQL e RabbitMQ (forniti via container nell'ambiente di sviluppo locale)

## Avvio in locale

> Le istruzioni verranno completate con l'introduzione dei primi microservizi.

```bash
git clone https://github.com/NaTC00/AgriNexus.git
cd AgriNexus
```

---

## Struttura del repository

```
AgriNexus/
├── src/              # Microservizi e librerie condivise
├── tests/            # Unit, integration ed end-to-end test
├── docs/
│   ├── adr/          # Architecture Decision Records
│   └── DIARIO.md     # Stato di avanzamento e prossimi passi
└── deploy/           # Compose, manifest e configurazioni di deploy
```

---

## Documentazione

- [`docs/adr/`](docs/adr/) — le decisioni architetturali prese, con il contesto e le alternative valutate
- [`docs/DIARIO.md`](docs/DIARIO.md) — dove si trova il progetto adesso e qual è il passo successivo

---

## Licenza

Distribuito con licenza MIT. Si veda il file [`LICENSE`](LICENSE).
