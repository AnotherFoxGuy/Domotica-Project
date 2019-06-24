/*
  This a simple example of the aREST Library for Arduino (Uno/Mega/Due/Teensy)
  using the Ethernet library (for example to be used with the Ethernet shield).
  See the README file for more details.

  Written in 2014 by Marco Schwartz under a GPL license.
*/

#define LIGHTWEIGHT 1
#define DUUR 2000       // (min) x (seconds) x (milliseconds)

// Libraries
#include <FreqCount.h>
#include <SPI.h>
#include <SD.h>
#include <Wire.h>
#include <RTClib.h>
#include <DHT.h>
#include <Ethernet.h>
#include <aREST.h>
#include <avr/wdt.h>

RTC_DS1307 RTC;

// Variables
uint32_t timer;
float temperature;
float humidity;
int pins[] = {5, 6, 7};
DHT dht;

// set up variables using the SD utility library functions:
Sd2Card card;
SdVolume volume;
SdFile root;

// change this to match your SD shield or module;
// Arduino Ethernet shield: pin 4
// Adafruit SD shields and modules: pin 10
// Sparkfun SD shield: pin 8
const int chipSelect = 4;

// Enter a MAC address for your controller below.
byte mac[] = {0x90, 0xA2, 0xDA, 0xFE, 0x0E, 0x40};

// IP address in case DHCP fails
IPAddress ip(192, 168, 1, 3);

// Ethernet server
EthernetServer server(80);

// Create aREST instance
aREST rest = aREST();

// Custom function accessible by the API
int ledControl(const String& command) {

    // Get state from command
    int state = command.toInt();

    digitalWrite(6, state);
    return 1;
}


void showDate(const char *txt, const DateTime &dt) {
    Serial.print(dt.unixtime());

    Serial.println();
}

void setup(void) {
    // Start Serial
    Serial.begin(9600);

    // Start humidity and temp sensor
    dht.setup(8);
    // On the Ethernet Shield, CS is pin 4. It's set as an output by default.
    // Note that even if it's not used as the CS pin, the hardware SS pin
    // (10 on most Arduino boards, 53 on the Mega) must be left as an output
    // or the SD library functions will not work.
    pinMode(SS, OUTPUT);


    // we'll use the initialization code from the utility libraries
    // since we're just testing if the card is working!
    while (!card.init(SPI_HALF_SPEED, chipSelect)) {
        char sdm[7] = "sd i f";
        Serial.println(sdm);
    }

    DateTime dt0(0, 1, 1, 0, 0, 0);
    char d[4] = "dt0";
    showDate(d, dt0);

    // Function to be exposed
    char l[4] = "led";
    rest.function(l, reinterpret_cast<int (*)(String)>(&ledControl));
    char t[12] = "temperature";
    rest.variable(t, &temperature);
    char h[9] = "humidity";
    rest.variable(h, &humidity);

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
        if ((millis() - timer) > DUUR) {
            temperature = dht.getTemperature();
            humidity = dht.getHumidity();
            timer = millis();
        }
    }
}
