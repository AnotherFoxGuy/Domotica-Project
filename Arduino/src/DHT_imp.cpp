#include "DHT_imp.h"

DHT dht;

float REST_Temperature;
float REST_Humidity;

void DHTsetup(aREST *rest)
{
    // Start humidity and temp sensor
    char t[12] = "temperature";
    rest->variable(t, &REST_Temperature);
    char h[9] = "humidity";
    rest->variable(h, &REST_Humidity);
    dht.setup(8);
    DHTloop();
}

void DHTloop()
{
    REST_Temperature = dht.getTemperature();
    REST_Humidity = dht.getHumidity();
}
