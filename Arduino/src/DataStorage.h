#pragma one
#include "Prerequisites.h"

extern int REST_Temperature;
extern int REST_Humidity;

void DataStorageSetup();
void DataStorageloop();
void writeData2SD(String Data);
void readData2SD();
extern File sensorDB;
