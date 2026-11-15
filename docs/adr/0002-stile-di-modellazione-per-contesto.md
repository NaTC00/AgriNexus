# 0002 — Stile di modellazione scelto per contesto

- **Stato:** Accettata
- **Data:** 2026-11-15

## Contesto

AgriNexus nasce con il Domain-Driven Design (DDD) tra i suoi principi architetturali. Questo però non significa che tutti i microservizi debbano necessariamente utilizzare un modello di dominio ricco.

I primi tre contesti individuati nell'ADR 0001 hanno caratteristiche diverse.

Anagrafiche si occupa principalmente di gestire soggetti, aziende e fascicoli. Le operazioni sono prevalentemente inserimenti, modifiche e consultazioni, con controlli legati soprattutto alla correttezza dei dati.

Domande, invece, gestisce un processo più complesso. Una domanda ha uno stato, può attraversare determinate fasi e deve rispettare regole che coinvolgono più informazioni. Ad esempio, non può essere presentata se non contiene interventi, l'importo non può superare il massimale previsto e una domanda già presentata non può tornare in bozza senza una procedura specifica.

Utilizzare lo stesso approccio per entrambi i contesti non sarebbe una scelta particolarmente efficace: in un caso si introdurrebbe complessità non necessaria, nell'altro si rischierebbe di non gestire adeguatamente le regole del dominio.

## Opzioni valutate

### A — Utilizzare DDD per tutti i microservizi

Avremmo un'unica impostazione architetturale e seguiremmo alla lettera i principi descritti per AgriNexus.

Il problema è che Anagrafiche non ha bisogno di aggregati, value object ed eventi di dominio per gestire operazioni sostanzialmente semplici. Si finirebbe per aggiungere strutture e complessità senza ottenere benefici reali.

Il rischio sarebbe quello di avere classi con semplici proprietà e tutta la logica nei service: una struttura che ricorda DDD, ma che in realtà non ne applica i principi.

### B — Utilizzare CRUD per tutti i microservizi

Sarebbe l'approccio più semplice e veloce da implementare.

Per Anagrafiche funzionerebbe bene, ma Domande presenta regole che non possono essere trattate come semplici modifiche ai dati. La logica finirebbe nei service e nei controller, rendendo più difficile garantire che tutte le operazioni rispettino gli stessi vincoli.

In questo modo perderemmo anche l'occasione di approfondire e applicare concretamente il DDD.

### C — Scegliere l'approccio in base al contesto

Ogni microservizio utilizza lo stile di modellazione più adatto alle caratteristiche del dominio che gestisce.

È l'opzione che permette di non introdurre complessità dove non serve e, allo stesso tempo, di modellare correttamente i contesti che hanno regole di business articolate.

## Decisione

Si sceglie l'opzione C.

La scelta per i primi tre contesti è la seguente:

- **Anagrafiche:** approccio CRUD (data-driven). Le entità saranno vicine al modello di persistenza, con validazioni sui dati e senza introdurre aggregati o una struttura DDD non necessaria.
- **Domande:** approccio DDD con un modello di dominio ricco. Le entità proteggeranno le regole di business, utilizzando aggregati, value object ed eventi di dominio dove necessario. Le operazioni saranno espresse attraverso metodi che rappresentano le azioni del dominio, anziché modifiche dirette alle proprietà.
- **Authorization:** approccio data-driven, come Anagrafiche. Le poche regole presenti, come l'emissione e la scadenza dei token, saranno gestite dal servizio applicativo senza introdurre aggregati.

Il criterio da utilizzare anche per i futuri microservizi sarà semplice: bisogna capire se un'operazione può essere rifiutata per motivi che vanno oltre la semplice validazione dei dati.

Se la possibilità di eseguire un'operazione dipende dallo stato attuale dell'oggetto o dalle relazioni con altri dati, è opportuno valutare un modello di dominio ricco. Se invece si tratta principalmente di inserire, leggere e aggiornare informazioni, un approccio CRUD è più che sufficiente.

Questa scelta è in linea anche con quanto mostrato da eShopOnContainers, dove convivono servizi CRUD e servizi con un modello di dominio ricco, in base alle esigenze dei singoli contesti.

## Conseguenze

Nel progetto convivranno due diversi stili di sviluppo. È importante che questa distinzione sia riconoscibile anche dalla struttura dei progetti, per evitare che chi sviluppa un nuovo microservizio prenda come riferimento un contesto che utilizza un approccio diverso.

Anagrafiche e Domande diventeranno anche un'occasione per confrontare concretamente i due approcci, capire come cambiano l'organizzazione del codice e la gestione della logica e valutare quando la maggiore complessità del DDD è realmente giustificata.

Questa decisione introduce una distinzione rispetto alla descrizione iniziale di AgriNexus, che cita il DDD tra i principi architetturali senza fare differenze tra i contesti. Si tratta di una scelta intenzionale: l'obiettivo non è applicare DDD ovunque per uniformità, ma utilizzarlo dove porta un reale vantaggio.

Non verrà quindi introdotto un modello di dominio ricco in Anagrafiche soltanto per avere una struttura simile a Domande. Se in futuro dovessero emergere regole di business più complesse, come una validità temporale del fascicolo che condiziona altri dati, la scelta potrà essere rivalutata attraverso un nuovo ADR.
