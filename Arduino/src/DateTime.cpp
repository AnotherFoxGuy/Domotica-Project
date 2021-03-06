#include "DateTime.h"

#define countof(a) (sizeof(a) / sizeof(a[0]))

RtcDS1307<TwoWire> Rtc(Wire);

void setupDateTime()
{
  //Wire.begin(0, 2);

  Rtc.Begin();

  RtcDateTime compiled = RtcDateTime(__DATE__, __TIME__);
  printDateTime(compiled);
  Serial.println();

  if (!Rtc.IsDateTimeValid())
  {
    if (Rtc.LastError() != 0)
    {
      // we have a communications error
      // see https://www.arduino.cc/en/Reference/WireEndTransmission for
      // what the number means
      Serial.print("RTC communications error = ");
      Serial.println(Rtc.LastError());
    }
    else
    {
      // Common Causes:
      //    1) first time you ran and the device wasn't running yet
      //    2) the battery on the device is low or even missing

      Serial.println("RTC lost confidence in the DateTime!");
      // following line sets the RTC to the date & time this sketch was compiled
      // it will also reset the valid flag internally unless the Rtc device is
      // having an issue

      Rtc.SetDateTime(compiled);
    }
  }

  if (!Rtc.GetIsRunning())
  {
    Serial.println("RTC was not actively running, starting now");
    Rtc.SetIsRunning(true);
  }

  RtcDateTime now = Rtc.GetDateTime();
  if (now < compiled)
  {
    Serial.println("RTC is older than compile time!  (Updating DateTime)");
    Rtc.SetDateTime(compiled);
  }
  else if (now > compiled)
  {
    Serial.println("RTC is newer than compile time. (this is expected)");
  }
  else if (now == compiled)
  {
    Serial.println("RTC is the same as compile time! (not expected but all is fine)");
  }

  // never assume the Rtc was last configured by you, so
  // just clear them to your needed state
  Rtc.SetSquareWavePin(DS1307SquareWaveOut_Low);

  RtcDateTime gfbhfg = Rtc.GetDateTime();

  printDateTime(gfbhfg);
  Serial.println();
}

String GetCurrentDate()
{
  RtcDateTime now = Rtc.GetDateTime();
  char yfudvnyundjj[20];
  sprintf(yfudvnyundjj, "%i0%i", now.Day(), now.Month());
  Serial.println(String(yfudvnyundjj));
  return yfudvnyundjj;
}

String GetCurrentTime()
{
  RtcDateTime now = Rtc.GetDateTime();
  char kunvdnykrytvdgfjdk[10];
  sprintf(kunvdnykrytvdgfjdk, "%i-%i", now.Hour(), now.Minute());
  Serial.println(String(kunvdnykrytvdgfjdk));
  return kunvdnykrytvdgfjdk;
}

void printDateTime(const RtcDateTime &dt)
{
  char datestring[20];

  snprintf_P(datestring,
             countof(datestring),
             PSTR("%02u/%02u/%04u %02u:%02u:%02u"),
             dt.Month(),
             dt.Day(),
             dt.Year(),
             dt.Hour(),
             dt.Minute(),
             dt.Second());
  Serial.print(datestring);
}