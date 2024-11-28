#include <ModbusMaster.h>
#include <SoftwareSerial.h>

SoftwareSerial RS485Serial(10, 11); // RX, TX (Modbus over RS485)
#define MAX485_DE 2

ModbusMaster node;

// Variabili per lo stato della sbarra e del LED
int ledState = LOW;   // LED (verde=0, rosso=1)
int barrierState = 0; // Sbarra (0=abbassata, 1=alzata)

// Variabili per configurazione velocità e tempo
int velocity = 0;  // Velocità di movimento della sbarra
int delayTime = 0; // Tempo di attesa prima di chiudere la sbarra

unsigned long lastDebounceTime = 0;
unsigned long debounceDelay = 500;

bool readSlaveValues();
void sendMovementsToSoftware();
void processSoftwareCommands();

// Funzione di pre-trasmissione (per RS485)
void preTransmission()
{
  digitalWrite(MAX485_DE, 1);
}

// Funzione di post-trasmissione (per RS485)
void postTransmission()
{
  digitalWrite(MAX485_DE, 0);
}

void setup()
{
  pinMode(MAX485_DE, OUTPUT);
  digitalWrite(MAX485_DE, 0);

  Serial.begin(9600);
  RS485Serial.begin(9600);
  node.begin(1, RS485Serial);
  node.preTransmission(preTransmission);
  node.postTransmission(postTransmission);
}

void loop()
{
  if ((millis() - lastDebounceTime) > debounceDelay)
  {
    if (readSlaveValues())
    {
      sendMovementsToSoftware();
    }

    if (Serial.available() > 0)
      processSoftwareCommands();

    lastDebounceTime = millis();
  }
}

bool readSlaveValues()
{
  uint8_t result = node.readCoils(0x4000, 3); // Registri per LED e sbarra
  if (result == 0)
  {
    int response = node.getResponseBuffer(0);
    ledState = bitRead(response, 0);     // Stato LED (0 = verde, 1 = rosso)
    barrierState = bitRead(response, 1); // Stato sbarra (0 = abbassata, 1 = alzata)

    if (bitRead(response, 2) == 1)
    {
      if (node.readInputRegisters(0x3000, 5) == node.ku8MBSuccess)
      {
        char decodedString[11] = {0}; // Stringa da 10 caratteri + terminatore null
        for (int i = 0; i < 5; i++)
        {
          uint16_t regValue = node.getResponseBuffer(i);

          decodedString[i * 2] = (regValue >> 8) & 0xFF; // High byte
          decodedString[i * 2 + 1] = regValue & 0xFF;    // Low byte
        }
        Serial.print("qst:");
        Serial.println(decodedString);
      }
    }
    return true;
  }
  else
  {
    Serial.println("ModBus communication error");
  }
  return false;
}

void sendMovementsToSoftware()
{
  // Invia movimenti della sbarra e del LED al software
  String barrierMovement = "MOV_BARRIER:" + String(barrierState); // Sbarra (0=abbassata, 1=alzata)
  String ledMovement = "MOV_LED:" + String(ledState);             // LED (0=verde, 1=rosso)

  Serial.println(barrierMovement);
  Serial.println(ledMovement);
}

void processSoftwareCommands()
{
  String command = Serial.readStringUntil('\n'); // Legge il comando dalla seriale
  command.trim();
  Serial.println(command);

  // Se il comando è di configurazione (cfg)
  if (command.startsWith("cfg:"))
  {
    int separatorIndex = command.indexOf(';');
    if (separatorIndex > 0)
    {
      String velocityStr = command.substring(4, separatorIndex); // Velocità
      String delayStr = command.substring(separatorIndex + 1);   // Tempo di ritardo

      velocity = velocityStr.toInt();
      delayTime = delayStr.toInt();

      // Salva la configurazione per la sbarra
      node.writeSingleRegister(0x3100, velocity);  // Scrive la velocità nello slave
      node.writeSingleRegister(0x3101, delayTime); // Scrive il tempo di ritardo nello slave
      Serial.println("Configuration updated");
    }
  }

  // Se il comando è MAN_LED, inverte lo stato del LED
  if (command.startsWith("MAN_LED"))
  {
    ledState = (ledState == LOW) ? HIGH : LOW;
    node.writeSingleCoil(0x000C, ledState); // Scrive il nuovo stato del LED nello slave
    Serial.println("LED state changed");
  }

  // Se il comando è MAN_BARRIER, inverte lo stato della sbarra
  if (command.startsWith("MAN_BARRIER"))
  {
    barrierState = (barrierState == 0) ? 1 : 0;
    node.writeSingleCoil(0x000D, barrierState); // Scrive il nuovo stato della sbarra nello slave
    Serial.println("Barrier state changed");
  }

  // Se il comando è rsp:true/false, solleva o abbassa la sbarra
  if (command.startsWith("rsp:"))
  {
    bool raise = command.substring(4) == "true"; // Determina se deve alzare la sbarra
    barrierState = raise ? 1 : 0;                // Alza o abbassa la sbarra
    node.writeSingleCoil(0x000E, barrierState);  // Comando per lo slave
    Serial.println("rsp");
  }
}