#include "DHT_imp.h"

uint32_t timer;
DHT dht;

float REST_Temperature;
float REST_Humidity;

void DHTsetup(aREST *rest) {
    // Start humidity and temp sensor
    char t[12] = "temperature";
    rest->variable(t, &REST_Temperature);
    char h[9] = "humidity";
    rest->variable(h, &REST_Humidity);
    dht.setup(8);

    REST_Temperature = 500;
    REST_Humidity = 612;
}

void DHTloop() {
    // dht sensor
    /*if (timer != 0) {
        if ((millis() - timer) > DUUR) {
            REST_Temperature = dht.getTemperature();
            REST_Humidity = dht.getHumidity();
            timer = millis();
        }
    }*/
}
