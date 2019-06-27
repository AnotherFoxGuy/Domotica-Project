#include "DataStorage.h"
#include "DateTime.h"

File sensorDB;

void DataStorageSetup()
{
  REST_Temperature = 5605;
  REST_Humidity = 555;
}

void DataStorageloop()
{
  auto LightSensor = analogRead(0);
  auto GroundSensor = analogRead(1);
  auto WaterSensor = analogRead(2);

  String iocdsmciorhrtnbrbv = "\
-\n\
    time: "+ GetCurrentTime() +"\n\
    temperature: "+ REST_Temperature +"\n\
    humidity: "+ REST_Humidity +"\n\
    lightlevel: "+ LightSensor +"\n\
    moisture: "+ GroundSensor +"\n\
    waterlevel: "+ WaterSensor +"\
"
  ;
    writeData2SD(iocdsmciorhrtnbrbv);
}

void writeData2SD(String Data)
{
  String bfhghbgfhbf = GetCurrentDate();
  bfhghbgfhbf += ".txt";
  sensorDB = SD.open(bfhghbgfhbf.c_str(), FILE_WRITE);

  // if the file opened okay, write to it:
  if (sensorDB)
  {
    Serial.println("Writing to file " + bfhghbgfhbf);
    sensorDB.println(Data);
    sensorDB.close();
    Serial.println("done!");
  }
  else
  {
    // if the file didn't open, print an error:
    Serial.println("error opening file");
  }
}

