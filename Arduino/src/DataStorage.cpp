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

  /*
  String sensorData = "{" + 
    "timestamp : " + showCurrentTime() + ", " 
    "temperature : " + String(REST_Temperature) + ", " 
    "humidity : " + String(REST_Humidity) + ", " 
    "lightlevel : " +String(LightSensor) + ", " 
    "moisture : " + String(GroundSensor) + ", " 
    "waterlevel : " + String(WaterSensor)
  + "}";
  */

  String sensorData = String(REST_Temperature) + ", " + String(REST_Humidity) + ", " + String(LightSensor) + ", " + String(GroundSensor) + ", " + String(WaterSensor);
  writeData2SD(sensorData);
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
  String fileName = showCurrentDate();
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
