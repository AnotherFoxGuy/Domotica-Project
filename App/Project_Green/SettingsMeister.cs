using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Green
{
    class SettingsMeister
    {
        Dictionary<int, string> imageC;
        public SettingsMeister()
        {
            imageC = new Dictionary<int, string>();
        }
        
        public Dictionary<int, string> imageselector()
        {
            
            if (imageC.Count == 0)
            {
                imageC.Add(0, "GreenhouseDefault.png");
                imageC.Add(1, "GreenHouse1.png");
                imageC.Add(2, "GreenHouse2.png");
                imageC.Add(3, "GreenHouse3.png");
                imageC.Add(4, "GreenHouse4.png");
                imageC.Add(5, "GreenHouse5.png");

            }
            return imageC;
        }
        public string imagesource(int x)
        {
            string NameImage = imageC[x];
            return "/Images/" + NameImage;
        }
       
    }
}
