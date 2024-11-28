#pragma region Includes
#include <Arduino.h>
#include <SPI.h>
#include <MFRC522.h>
#include <Servo.h>
#include <ModbusSlave.h>
#include <SoftwareSerial.h>
#pragma endregion

#pragma region Variables, costants and Methods
// Pin SPI (MOSI, MISO, SCK) dichiarati esplicitamente
#define SCK_PIN 52  // Pin SCK (Serial Clock)
#define MOSI_PIN 51 // Pin MOSI (Master Out Slave In)
#define MISO_PIN 50 // Pin MISO (Master In Slave Out)
#define SDA_PIN 9   // SDA (SS) pin del modulo RFID
#define RST_PIN 8   // Reset pin del modulo RFID

bool statoSbarra = 0;  // true alzata, false abbassata
bool statoSemaforo = 1; // true rosso, false verde

int barrierMotorDelay = 3;
int barrierWaitTime = 2000;
bool newCard = false;
String newPlate;
uint16_t modbusRegisters[10];

const int ledRosso = 4;
const int ledVerde = 3;
const int servoPin = 23;
const int buzzerPin = 48;

// Pin del sensore ultrasuoni
const int trigPin = 6; // Pin Trigger
const int echoPin = 7; // Pin Echo

SoftwareSerial RS485Serial(10, 11); // RX, TX
Modbus slave(RS485Serial, 1, 2);
MFRC522 rfid(SDA_PIN, RST_PIN); // Oggetto MFRC522 per la comunicazione con il modulo RFID
Servo sbarra;
void carAllowed();
void apriSbarra();
void chiudiSbarra();
void suonaBuzzer(int durata);
uint8_t writeDigitalOut(uint8_t fc, uint16_t address, uint16_t length);
uint8_t readDigital(uint8_t fc, uint16_t address, uint16_t length);
uint8_t readAnalogIn(uint8_t fc, uint16_t address, uint16_t length);
#pragma endregion

void setup()
{
  pinMode(ledRosso, OUTPUT);
  pinMode(ledVerde, OUTPUT);
  pinMode(buzzerPin, OUTPUT);
  pinMode(trigPin, OUTPUT); // TRIG come output
  pinMode(echoPin, INPUT);  // ECHO come input

  digitalWrite(ledRosso, HIGH);
  digitalWrite(ledVerde, LOW);
  sbarra.attach(servoPin, 0, 1000);

  // Inizializzazione della comunicazione SPI
  SPI.begin();                 // Inizializza la comunicazione SPI
  pinMode(SCK_PIN, OUTPUT);    // SCK come output
  pinMode(MISO_PIN, INPUT);    // MISO come input
  pinMode(MOSI_PIN, OUTPUT);   // MOSI come output
  pinMode(SDA_PIN, OUTPUT);    // SDA come output
  digitalWrite(SDA_PIN, HIGH); // Disabilita la selezione slave inizialmente
  rfid.PCD_Init();

  // Inizializzazione del modbus slave
  slave.cbVector[CB_READ_COILS] = readDigital;
  slave.cbVector[CB_READ_DISCRETE_INPUTS] = readDigital;
  slave.cbVector[CB_WRITE_HOLDING_REGISTERS] = writeDigitalOut;
  slave.cbVector[CB_WRITE_COILS] = writeDigitalOut;
  slave.cbVector[CB_READ_INPUT_REGISTERS] = readAnalogIn;
  Serial.begin(9600);
  RS485Serial.begin(9600);
  slave.begin(9600);
}

void loop()
{
  slave.poll();
  if (rfid.PICC_IsNewCardPresent())
  {
    newCard = true;
    if (rfid.PICC_ReadCardSerial())
    {
      newPlate = "";
      for (int i = 0; i < rfid.uid.size; i++)
      {
        newPlate += String(rfid.uid.uidByte[i], HEX);
      }
      for (int i = 0; i < 5; i++)
      {
        uint8_t char1 = newPlate[i * 2];            // Primo carattere
        uint8_t char2 = newPlate[i * 2 + 1];        // Secondo carattere
        modbusRegisters[i] = (char1 << 8) | char2; // Combina i due caratteri in uint16_t
      }

      rfid.PICC_HaltA();
      rfid.PCD_StopCrypto1();
    }
  }
}

void carAllowed()
{
  apriSbarra();
  delay(barrierWaitTime);
  chiudiSbarra();
}

void apriSbarra()
{
  statoSemaforo = 0;
  statoSbarra = 1;
  digitalWrite(ledRosso, LOW);
  digitalWrite(ledVerde, HIGH);
  for (int i = 0; i < 250; i += 1)
  {
    sbarra.write(i);
    delay(barrierMotorDelay);
  }
  suonaBuzzer(400);
}

void chiudiSbarra()
{
  statoSemaforo = 1;
  statoSbarra = 0;
  digitalWrite(ledRosso, HIGH);
  digitalWrite(ledVerde, LOW);
  for (int i = 250; i >= 1; i -= 1)
  {
    sbarra.write(i);
    delay(barrierMotorDelay);
  }
  suonaBuzzer(400);
}

void suonaBuzzer(int durata)
{
  digitalWrite(buzzerPin, HIGH);
  delay(durata);
  digitalWrite(buzzerPin, LOW);
}

uint8_t writeDigitalOut(uint8_t fc, uint16_t address, uint16_t length)
{
  if (address == 0x3100)
    barrierMotorDelay = slave.readRegisterFromBuffer(0);
  else if (address == 0x3101)
    barrierWaitTime = slave.readRegisterFromBuffer(0);
  else if (address == 0x000C)
  {
    if (statoSemaforo)
    {
      statoSemaforo = 0;
      digitalWrite(ledRosso, LOW);
      digitalWrite(ledVerde, HIGH);
    }
    else
    {
      statoSemaforo = 1;
      digitalWrite(ledRosso, HIGH);
      digitalWrite(ledVerde, LOW);
    }
  }
  else if (address == 0x000D)
    if (statoSbarra)
      chiudiSbarra();
    else
      apriSbarra();

  else if (address == 0x000E)
  {
    if (slave.readCoilFromBuffer(0))
      carAllowed();
  }

  return STATUS_OK;
}

uint8_t readDigital(uint8_t fc, uint16_t address, uint16_t length)
{
  if (address == 0x4000) // led, sbarra e presenza card
  {
    for (int i = 0; i < 3; i++)
    {
      int value = 0;
      if (i == 0)
      {
        value = (statoSemaforo) ? 1 : 0;
      }
      else if (i == 1)
      {
        value = (statoSbarra) ? 1 : 0;
      }
      else if (i == 2)
      {
        if (newCard)
          value = 1; // Card presente
        else
          value = 0; // Card not present

        newCard = false;
      }
      slave.writeCoilToBuffer(i, value);
    }
  }
  return STATUS_OK;
}

uint8_t readAnalogIn(uint8_t fc, uint16_t address, uint16_t length)
{
  if (address == 0x3000)
  {
    for (int i = 0; i < 5; i++)
    {
      slave.writeRegisterToBuffer(i, modbusRegisters[i]);
    }
    Serial.println("Scritta targa su registri");
  }

  return STATUS_OK;
}