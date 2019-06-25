#pragma once

#include "Prerequisites.h"

extern  RTC_DS1307 __RTC;

void setupDateTime();
String GetCurrentDate();
String GetCurrentTime();