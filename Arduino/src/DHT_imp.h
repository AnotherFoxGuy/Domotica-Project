#pragma once

#include "DHT_imp.h"
#define DUUR 2000       // (min) x (seconds) x (milliseconds)

#include "Prerequisites.h"

extern uint32_t timer;
extern DHT dht;

extern float REST_Temperature;
extern float REST_Humidity;

void DHTsetup(aREST *pRest);

void DHTloop();
