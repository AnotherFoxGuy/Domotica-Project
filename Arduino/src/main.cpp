/*
  This a simple example of the aREST Library for Arduino (Uno/Mega/Due/Teensy)
  using the Ethernet library (for example to be used with the Ethernet shield).
  See the README file for more details.

  Written in 2014 by Marco Schwartz under a GPL license.
*/

// Libraries
#include "Prerequisites.h"
#include <Ticker.h>
#include <aREST.h>

// headers
#include "DataStorage.h"
#include "FileServe.h"
#include "DateTime.h"

RTC_DS1307 RTC;

// Variables
int pins[] = {5, 6, 7};

// Enter a MAC address for your controller below.
byte mac[] = {0x70, 0xA2, 0xDC, 0xFE, 0x0E, 0x40};

// IP address in case DHCP fails
IPAddress ip(192, 168, 1, 3);

// Ethernet server
EthernetServer server(80);

// Create aREST instance
aREST rest = aREST();

void DHTloop();
DHT dht;
int REST_Temperature;
int REST_Humidity;

Ticker DHTTimer(DHTloop, 2000);
Ticker DataStorageTimer(DataStorageloop, 300000, 0, MILLIS);

void setup()
{
    pinMode(10, OUTPUT);
    digitalWrite(10, HIGH); // Schakel Ethernet chip uit.
    // Start Serial
    Serial.begin(115200);

    Serial.print("Initializing SD card...");
    if (!SD.begin(4))
    {
        Serial.println("initialization failed!");
    }
    else
    {
        Serial.println("initialization done.");
    }

    // Give name & ID to the device (ID should be 6 characters long)
    char message[19] = "Bloemkool_BoilerV3";
    rest.set_name(message);
    char did[6] = "62153";
    rest.set_id(did);

    setupDateTime();

    // Start the Ethernet connection and the server
    // if no dhcp, use default ip
    if (EthernetClass::begin(mac) == 0)
    {
        EthernetClass::begin(mac, ip);
    }

    server.begin();
    char sm[14] = "server is at ";
    Serial.print(sm);
    Serial.println(EthernetClass::localIP());

    // Start humidity and temp sensor
    rest.variable("temperature", &REST_Temperature);
    rest.variable("humidity", &REST_Humidity);
    dht.setup(8);
    DHTTimer.start();

    DataStorageSetup();
    DataStorageTimer.start();
    DataStorageloop();

    // Start watchdog
    wdt_enable(WDTO_4S);

    FileServeSetup();

    delay(5);
}

void loop()
{
    // listen for incoming clients
    auto client = server.available();
    rest.handle(client);
    wdt_reset();

    DHTTimer.update();
    DataStorageTimer.update();
    FileServeLoop();
}

void DHTloop()
{
    REST_Temperature = dht.getTemperature();
    REST_Humidity = dht.getHumidity();
}