
#include "DateTime.h"

RTC_DS1307 __RTC;

void setupDateTime()
{

  Wire.begin();
  __RTC.begin();
  if (!__RTC.isrunning())
  {
    Serial.println("RTC is NOT running!");
    // following line sets the RTC to the date & time this sketch was compiled
    __RTC.adjust(DateTime(__DATE__, __TIME__));
  }
}

String GetCurrentDate()
{
  /* Serial.print(txt);
  Serial.print(' ');
  Serial.print(dt.year(), DEC);
  Serial.print('/');
  Serial.print(dt.month(), DEC);
  Serial.print('/');
  Serial.print(dt.day(), DEC);
  Serial.print(' ');
  Serial.print(dt.hour(), DEC);
  Serial.print(':');
  Serial.print(dt.minute(), DEC);
  Serial.print(':');
  Serial.print(dt.second(), DEC);
  
  Serial.print(" = ");
  Serial.print(dt.unixtime());
  Serial.print("s / ");
  Serial.print(dt.unixtime() / 86400L);
  Serial.print("d since 1970");
  
  Serial.println();*/

  DateTime dt = __RTC.now();
  char buff[10];
  sprintf(buff, "%s%s%s", dt.day(), dt.month(), dt.year());
  return buff;
}

String GetCurrentTime()
{
  /*Serial.print(txt);
  Serial.print(" ");
  Serial.print(ts.days(), DEC);
  Serial.print(" days ");
  Serial.print(ts.hours(), DEC);
  Serial.print(" hours ");
  Serial.print(ts.minutes(), DEC);
  Serial.print(" minutes ");
  Serial.print(ts.seconds(), DEC);
  Serial.print(" seconds (");
  Serial.print(ts.totalseconds(), DEC);
  Serial.print(" total seconds)");
  Serial.println();*/

  DateTime dt = __RTC.now();
  char buff[10];
  sprintf(buff, "%s-%s", dt.hour(), dt.minute());
  return buff;
}
