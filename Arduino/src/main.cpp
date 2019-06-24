/*
  This a simple example of the aREST Library for Arduino (Uno/Mega/Due/Teensy)
  using the Ethernet library (for example to be used with the Ethernet shield).
  See the README file for more details.

  Written in 2014 by Marco Schwartz under a GPL license.
*/


// Libraries
#include "Prerequisites.h"

// headers
#include "DHT_imp.h"
#include "REST_Endpoints.h"

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

void setup() {
    // Start Serial
    Serial.begin(9600);

    setupRestEndpoints(&rest);

    // Give name & ID to the device (ID should be 6 characters long)
    char message[19] = "Bloemkool_BoilerV3";
    rest.set_name(message);
    char did[5] = "1551";
    rest.set_id(did);

    // Start the Ethernet connection and the server
    //if no dhcp, use default ip
    if (EthernetClass::begin(mac) == 0) {
        EthernetClass::begin(mac, ip);
    }
    server.begin();
    char sm[14] = "server is at ";
    Serial.print(sm);
    Serial.println(EthernetClass::localIP());

    DHTsetup(&rest);

    // Start watchdog
    wdt_enable(WDTO_4S);

    delay(5);
}

void loop() {
    // listen for incoming clients
    auto client = server.available();
    rest.handle(client);
    wdt_reset();

    auto uu = millis();
    DHTloop();
}