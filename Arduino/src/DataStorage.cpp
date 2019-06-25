#include "DataStorage.h"
#include "DHT_imp.h"
#include "DateTime.h"

File sensorDB;
void DataStorageSetup()
{
  Serial.begin(9600);
  Serial.print("Initializing SD card...");

  if (!SD.begin(4))
  {
    Serial.println("initialization failed!");
  }
  Serial.println("initialization done.");
}

void DataStorageloop()
{
  auto LightSensor = analogRead(0);
  auto GroundSensor = analogRead(1);
  auto WaterSensor = analogRead(2);

  char buffer[150];
  const char dataf[118] = "{ \"timestamp\": \"%s\", \"temperature\": \"%f\", \"humidity\": \"%f\", \"lightlevel\": \"%i\", \"moisture\": \"%i\", \"waterlevel\": \"%i\"}";
  int stat = sprintf(buffer, dataf, GetCurrentTime().c_str(), REST_Temperature, REST_Humidity, LightSensor, GroundSensor, WaterSensor);
  if (stat > 0)
  {
    writeData2SD(buffer);
  }
  else
  {
    Serial.print("Error at sprintf 25 DataStorageloop");
  }
}

void writeData2SD(String Data)
{
  sensorDB = SD.open("test.txt", FILE_WRITE);

  // if the file opened okay, write to it:
  if (sensorDB)
  {
    Serial.print("Writing to test.txt...");
    sensorDB.println(Data);
    sensorDB.close();
    Serial.println("done.");
  }
  else
  {
    // if the file didn't open, print an error:
    Serial.println("error opening test.txt");
  }
}

void readData2SD()
{
  String fileName = GetCurrentDate();
  fileName += ".txt";
  sensorDB = SD.open(fileName.c_str());
  if (sensorDB)
  {
    Serial.println("test.txt:");

    // read from the file until there's nothing else in it:
    while (sensorDB.available())
    {
      Serial.write(sensorDB.read());
    }
    // close the file:
    sensorDB.close();
  }
}
