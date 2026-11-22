# Diario di progetto

Dove si trova AgriNexus adesso e qual è il passo successivo.

Questo file risponde a tre domande e basta. La pianificazione operativa (task, sprint, stato) vive nel **GitHub Project** del repository; le decisioni architetturali e il loro perché vivono in [`adr/`](adr/). Qui resta solo il filo del discorso, utile per riprendere il lavoro dopo una pausa.

**Convenzione:** una voce per sessione di lavoro, la più recente in alto. Due o tre righe, non un resoconto.

---

## Stato attuale

Authorization ha il suo modello dati su PostgreSQL, creato dalla prima migration, e risponde
su `/health`. Non emette ancora token e non ci sono ancora test.

## Prossima attività

Emissione dei token in Authorization, decidendo prima cosa contiene il JWT (lasciato aperto
dall'ADR 0003).

---

## Log

### 2026-11-22 — Impianto di Authorization e modello dei permessi

Abbiamo scelto RBAC con ambito (ADR 0003) e creato la solution con i progetti Domain,
Application, Infrastructure e Host, con le entità, la prima migration e l'endpoint di health
che controlla anche il collegamento al database.
Nota: l'ambito è formato da tipo e valore, e un utente o un ruolo con assegnazioni non si può
cancellare, così i diritti non vengono tolti per errore.
Nota: prima della migration passiamo a PostgreSQL (ADR 0004) e a .NET 10, perché .NET 9 non è
più supportato dal 10 novembre 2026.

Issue #3 · branch `feature/auth-impianto-solution-e-modello-dati`

### 2026-11-15 — Inizializzazione del repository

Creato il repository con README, licenza MIT e `.gitignore` per .NET.
Impostata la roadmap: **Authorization** è il primo microservizio, seguito dall'API Gateway, prima di affrontare i domini di business.
Predisposti il diario e la cartella degli ADR.
