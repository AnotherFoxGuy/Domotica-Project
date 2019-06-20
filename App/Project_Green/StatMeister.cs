using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Green
{
    class StatMeister
    {
        public int Limitcheck(int max, int min, int value)
        {
            if (value < min) { value = 0; }
            if (value > max) { value = 100; }
            return value;
        }
        public string ColorRep(int value, int max , int min)
        {
            string color = "";
            int convertedvalue = convert(value,max,min);
            color = convertedvalue.ToString();
            Dictionary<int, string> TimeDates = new Dictionary<int, string>();
            TimeDates.Add(1, "#FF1493");
            TimeDates.Add(2, "#FF1493");
            TimeDates.Add(3, "#FF1493");
            TimeDates.Add(4, "#FF1493");
            TimeDates.Add(5, "#FF1493");
            TimeDates.Add(6, "#FF1493");
            TimeDates.Add(7, "#FF1493");
            TimeDates.Add(8, "#FF1493");
            TimeDates.Add(9, "#FF1493");
            TimeDates.Add(10, "#FF1493");
            return color;
        }

        public int convert (int value,int max ,int min)
        {
            return (value - max) / (min - max) * (10 - 0) + 10;           
        }
       

    }
}
