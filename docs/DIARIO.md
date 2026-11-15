# Diario di progetto

Dove si trova AgriNexus adesso e qual è il passo successivo.

Questo file risponde a tre domande e basta. La pianificazione operativa (task, sprint, stato) vive nel **GitHub Project** del repository; le decisioni architetturali e il loro perché vivono in [`adr/`](adr/). Qui resta solo il filo del discorso, utile per riprendere il lavoro dopo una pausa.

**Convenzione:** una voce per sessione di lavoro, la più recente in alto. Due o tre righe, non un resoconto.

---

## Stato attuale

Repository inizializzato. Nessun codice ancora: si sta definendo l'impianto prima di scrivere il primo microservizio.

## Prossima attività

Definire i bounded context e i contratti di integrazione tra i domini, così da fissare i confini prima di aprire la solution.

---

## Log

### 2026-11-15 — Inizializzazione del repository

Creato il repository con README, licenza MIT e `.gitignore` per .NET.
Impostata la roadmap: **Authorization** è il primo microservizio, seguito dall'API Gateway, prima di affrontare i domini di business.
Predisposti il diario e la cartella degli ADR.
