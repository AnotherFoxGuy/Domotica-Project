#include "DataStorage.h"
#include "DateTime.h"

File sensorDB;

extern float REST_Temperature;
extern float REST_Humidity;

void DataStorageSetup()
{

}

void DataStorageloop()
{
  auto LightSensor = analogRead(0);
  auto GroundSensor = analogRead(1);
  auto WaterSensor = analogRead(2);

  String buffer = "\
-\n\
    time :"+ GetCurrentTime() +":\n\
    temperature: "+ REST_Temperature +"\n\
    humidity: "+ REST_Humidity +"\n\
    lightlevel: "+ LightSensor +"\n\
    moisture: "+ GroundSensor +"\n\
    waterlevel: "+ WaterSensor +"\
"
  ;
    writeData2SD(buffer);
}

void writeData2SD(String Data)
{
  sensorDB = SD.open(GetCurrentDate().c_str(), FILE_WRITE);

  // if the file opened okay, write to it:
  if (sensorDB)
  {
    Serial.print("Writing to file...");
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

