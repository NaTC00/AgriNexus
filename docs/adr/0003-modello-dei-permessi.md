# 0003 — Modello dei permessi: RBAC con ambito

- **Stato:** Accettata
- **Data:** 2026-11-22

## Contesto

Il contesto Authorization stabilisce chi può fare cosa negli altri contesti della piattaforma.
L'ADR 0002 lo classifica come data-driven: non ha un dominio ricco, ma ha un modello dei dati
che condiziona tutti i servizi che dipenderanno da lui.

Il dominio simulato prevede figure operative distinte: chi presenta domande per conto di
un'azienda, chi le istruisce, chi autorizza, chi si limita a consultare. Le stesse persone
possono ricoprire più figure.

Va deciso dove vivono i permessi: se appartengono sempre a un ruolo, se possono essere
concessi direttamente a una persona, e se valgono ovunque o solo su un perimetro.

## Opzioni valutate

### A — RBAC puro

Un utente ha uno o più ruoli, un ruolo ha uno o più permessi. I permessi non si assegnano mai
direttamente a una persona.

Alla domanda «chi può fare X?» si risponde guardando quali ruoli contengono quel permesso e chi
li possiede. La risposta è deterministica e non richiede di ispezionare ogni singolo utente.

### B — RBAC con permessi diretti

Come sopra, ma è possibile concedere un permesso a una singola persona, in deroga ai suoi ruoli.

È flessibile e risolve in fretta i casi particolari. Il costo si paga dopo: le deroghe concesse
per un'emergenza non vengono quasi mai revocate, non compaiono da nessuna parte quando si
guardano i ruoli, e dopo qualche mese il vero modello di autorizzazione è l'insieme delle
eccezioni, che nessuno ha documentato.

### C — RBAC con ambito

Un ruolo vale su un perimetro invece che ovunque: istruttore di certe pratiche, non di tutte.

È il modello che serve davvero nella stragrande maggioranza dei casi di un sistema della
pubblica amministrazione. Il costo è un modello dati più articolato e una valutazione di
permesso contestuale, ma rappresenta direttamente il rapporto fra persona, ruolo e perimetro.

## Decisione

Si adotta l'opzione C: RBAC con ambito.

Il modello è: `Utente` molti-a-molti `Ruolo`, con un'entità di assegnazione che contiene l'ambito;
`Ruolo` molti-a-molti `Permesso`. Nessun legame diretto fra utente e permesso.

Un utente possiede un permesso in una determinata porzione del sistema quando ha un ruolo
che lo contiene e l'assegnazione comprende l'ambito richiesto. L'ambito viene rappresentato
come parte dell'assegnazione utente–ruolo, non come ruolo separato per ogni combinazione
persona-perimetro.

L'ambito è formato da un tipo e da un valore, per esempio tipo `Provincia` e valore `FI`.
Quando un ruolo vale su tutto il sistema si usa il tipo `Globale`, invece di lasciare i campi
vuoti. Il valore è un semplice codice e non un riferimento alle tabelle di altri contesti, così
da rispettare il database per servizio dell'ADR 0001. Inoltre la stessa combinazione di utente,
ruolo, tipo e valore non può essere inserita due volte.

Avevamo considerato anche un unico campo di testo (`"provincia:FI"`), ma sarebbe stato difficile
da validare e da interrogare. Una tabella `Ambito` separata invece al momento non serve, perché
nessuno ha bisogno di gestire gli ambiti come oggetti a sé.

L'opzione B viene scartata: i permessi diretti creerebbero eccezioni difficili da verificare e
renderebbero ambigua la fonte effettiva di un diritto.

## Conseguenze

La verifica di un permesso diventa contestuale: è necessario conoscere ruolo e ambito oltre
all'identità dell'utente. I servizi che richiedono l'autorizzazione devono propagare il perimetro
sull'operazione in modo esplicito.

Gli ambiti consentono di usare gli stessi ruoli su porzioni diverse del sistema. Le assegnazioni
utente–ruolo restano la fonte verificabile dei diritti e possono essere revocate senza creare
collegamenti diretti fra persone e permessi.

Il modello dati e le query diventano più complessi rispetto al RBAC puro. Questo costo è
accettato perché rappresenta la forma prevalente di autorizzazione richiesta dal dominio.

**Non è deciso qui cosa contiene il JWT**, se i ruoli o i permessi già risolti. È una scelta che
riguarda il formato del token e i servizi che lo consumano, e verrà presa nell'attività che
implementa l'emissione. Deciderla adesso significherebbe vincolare servizi che non esistono.

Le credenziali di accesso non sono oggetto di questo ADR: il modello prevede che l'utente abbia
un segreto verificabile, ma come viene conservato e verificato si decide quando si implementa
l'autenticazione.
