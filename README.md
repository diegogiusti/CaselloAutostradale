### Documentazione del Progetto: Casello Autostradale con Arduino Mega 2560 e Modbus

#### Descrizione del Progetto

Questo progetto è stato sviluppato come parte del corso **"Sviluppo Embedded"** presso l'**ITS Academy Alto Adriatico**. L'obiettivo è la realizzazione di un sistema automatizzato per il controllo di un casello autostradale, sfruttando tecnologie embedded e protocolli di comunicazione standard.

Il sistema utilizza due **Arduino Mega 2560** che comunicano tramite il protocollo **Modbus RS485** e un'applicazione desktop .NET per gestire la validazione e il controllo. Quando un veicolo si avvicina al casello, il conducente deve avvicinare un **tag RFID** al lettore. L'Arduino slave legge il tag e lo invia al master tramite Modbus. Il master, a sua volta, inoltra i dati all'applicazione .NET per la validazione. 

Se il tag è autorizzato, il sistema:
1. Alza automaticamente la sbarra.
2. Accende il semaforo verde per segnalare il passaggio consentito.
3. Dopo un tempo predefinito, abbassa nuovamente la sbarra.

---

### Architettura del Sistema

#### Componenti principali:
1. **Arduino Slave**:
   - Legge i tag RFID tramite un modulo RFID (MFRC522).
   - Gestisce un servo per l'apertura e chiusura della sbarra.
   - Controlla i LED del semaforo (rosso/verde) e un buzzer.
   - Comunica lo stato della sbarra e del semaforo al master tramite Modbus.

2. **Arduino Master**:
   - Comunica con lo slave tramite Modbus RS485.
   - Riceve i dati dallo slave e li invia tramite seriale a un'applicazione .NET per la validazione.
   - Gestisce i comandi ricevuti dal software per configurazioni, apertura/chiusura manuale della sbarra e accensione dei LED.

3. **Applicazione .NET**:
   - Interfaccia grafica per il monitoraggio e il controllo del casello.
   - Validazione delle targhe dei veicoli rispetto a una lista modificabile dall'utente.
   - Invia comandi seriali all'Arduino Master.

---

### Flusso Operativo

1. **Lettura del Tag RFID**:
   - Quando un veicolo arriva, lo slave legge il tag RFID del veicolo.
   - I dati vengono inviati al master tramite Modbus.

2. **Validazione del Tag**:
   - Il master passa le informazioni all'applicazione .NET.
   - L'applicazione verifica se il tag è presente nella lista di targhe autorizzate.

3. **Apertura della Sbarra**:
   - Se il tag è valido, l'applicazione invia un comando al master.
   - Il master comunica allo slave di alzare la sbarra, accendere il semaforo verde e suonare il buzzer.

4. **Chiusura della Sbarra**:
   - Dopo un tempo predefinito, la sbarra si chiude automaticamente.

---

### Dettagli Tecnici

#### Hardware Utilizzato:
- **Arduino Mega 2560 (x2)**
- Modulo RFID (MFRC522)
- Servo motore
- LED (rosso e verde)
- Modulo RS485 per la comunicazione Modbus
- Buzzer

#### Software:
- **Arduino IDE** per la programmazione degli Arduino.
- **Librerie utilizzate**:
  - `ModbusMaster` e `ModbusSlave` per la comunicazione Modbus.
  - `MFRC522` per il modulo RFID.
  - `Servo` per il controllo del servo motore.
- **Applicazione .NET**:
  - Interfaccia utente sviluppata in C# con Windows Forms.
  - Comunicazione seriale con Arduino master.

---

### Comandi Principali

#### Comandi da Applicazione .NET al Master:
- `rsp:true` / `rsp:false`: Alza/abbassa la sbarra.
- `MAN_LED`: Cambia lo stato del semaforo.
- `MAN_BARRIER`: Cambia lo stato della sbarra manualmente.
- `CFG:<velocità>;<ritardo>`: Configura velocità della sbarra e ritardo.

#### Comunicazione Modbus:
- **Slave**:
  - Registra su Modbus le informazioni sulla presenza del tag RFID, lo stato della sbarra e dei LED.
  - Riceve comandi per aprire/chiudere la sbarra e modificare la configurazione.
- **Master**:
  - Legge i registri dallo slave per ottenere lo stato.
  - Invia comandi di controllo allo slave.

---

### Configurazioni Principali

- **Sbarra**:
  - Velocità: Configurabile da applicazione .NET (default: 3 ms/step).
  - Ritardo di chiusura: Configurabile da applicazione .NET (default: 2000 ms).

- **Semaforo**:
  - Rosso: Sbarra abbassata.
  - Verde: Sbarra alzata.

- **Tag RFID**:
  - Codifica in formato esadecimale.
  - Confronto con lista targhe autorizzate sull'applicazione .NET.

---

### Diagramma del Flusso di Comunicazione

```
Macchina (RFID Tag) --> Slave (Arduino) --> Master (Arduino) --> Applicazione .NET
                <-- Modbus RS485      <-- Serial Commands
```

---

### Conclusione

Il sistema implementa un controllo efficiente e automatizzato di un casello autostradale, utilizzando Arduino Mega 2560 e Modbus per una comunicazione robusta. L'integrazione con un'applicazione .NET fornisce flessibilità e monitoraggio in tempo reale.