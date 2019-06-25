#pragma once

#include "Prerequisites.h"

extern DHT dht;

extern float REST_Temperature;
extern float REST_Humidity;

void DHTsetup(aREST *pRest);

void DHTloop();
