/*
  This a simple example of the aREST Library for Arduino (Uno/Mega/Due/Teensy)
  using the Ethernet library (for example to be used with the Ethernet shield).
  See the README file for more details.

  Written in 2014 by Marco Schwartz under a GPL license.
*/

#define LIGHTWEIGHT 1
#define DUUR 2000       // (min) x (seconds) x (milliseconds)

// Libraries
#include <SPI.h>
#include <DHT.h>
#include <Ethernet.h>
#include <aREST.h>
#include <avr/wdt.h>
#include <NewRemoteTransmitter.h>

// Variables
uint32_t timer;
float temperature;
float humidity;
int pins[] = {5,6,7};
DHT dht;

// Create a transmitter on address 123, using digital pin 11 to transmit, 
// with a period duration of 260ms (default), repeating the transmitted
// code 2^3=8 times.
NewRemoteTransmitter transmitter(31787478, 2, 260, 3);

// Enter a MAC address for your controller below.
byte mac[] = {0x90, 0xA2, 0xDA, 0x0E, 0xFE, 0x40};

// IP address in case DHCP fails
IPAddress ip(192, 168, 1, 3);

// Ethernet server
EthernetServer server(80);

// Create aREST instance
aREST rest = aREST();

// Declare functions to be exposed to the API
int ledControl(String command);

// Custom function accessible by the API
int ledControl(String command) {

    // Get state from command
    int state = command.toInt();

    digitalWrite(6, state);
    return 1;
}

// Custom function accessible by the API
int transmitterTurnOnControl(String command) {
    // Get state from command
    transmitter.sendUnit(command.toInt(), true);
    digitalWrite(pins[command.toInt()], HIGH);
    return 1;
}


// Custom function accessible by the API
int transmitterTurnOffControl(String command) {
    // Get state from command
    transmitter.sendUnit(command.toInt(), false);
    digitalWrite(pins[command.toInt()], LOW);
    return 1;
}

void setup(void) {
    // Start Serial
    Serial.begin(9600);

    // Start humidity and temp sensor
    dht.setup(8);
    
    // Function to be exposed
    rest.function("led", ledControl);
    rest.function("transmitterOff", transmitterTurnOnControl);
    rest.function("transmitterOn", transmitterTurnOffControl);

    rest.variable("temperature", &temperature);
    rest.variable("humidity", &humidity);

    // Give name & ID to the device (ID should be 6 characters long)
    rest.set_id("3621");
    rest.set_name("Kaas_Knabelaar");

    // Start the Ethernet connection and the server
    //if no dhcp, use default ip
    if (Ethernet.begin(mac) == 0) {
      Ethernet.begin(mac, ip);
    }
    server.begin();
    Serial.print("server is at ");
    Serial.println(Ethernet.localIP());

    // Start watchdog
    wdt_enable(WDTO_4S);

    delay(5);
    timer = millis();
}

void loop() {
    // listen for incoming clients
    EthernetClient client = server.available();
    rest.handle(client);
    wdt_reset();

    // dht sensor
    if (timer != 0) {
      if ((millis() - timer) > DUUR ) {
          temperature = dht.getTemperature();
          humidity = dht.getHumidity();
          timer = millis();
      }
    }
}
