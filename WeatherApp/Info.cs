using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Info
    {

        public float? Air_Pressure { get; set; }
        public DateTime Applicable_Date { get; set; }
        public float? Humidity { get; set; }
        public Int64? ID { get; set; }
        public Int64? Max_Temp { get; set; }
        public Int64? Min_Temp { get; set; }
        public Int32? Predictability { get; set; }
        public Int64? The_Temp { get; set; }
        public float? Visibility { get; set; }
        public WeatherStatus weatherStatus
        {

            get
            {
                return (from weatherStatus in WeatherCondtions.SetWeatherStatus() where weatherStatus.Abbreviation == Weather_State_Abbr select weatherStatus).First();
            }

        }

        public string Weather_State_Abbr { get; set; }
        public string Weather_State_Name { get; set; }
        public string Wind_Direction { get; set; }
        public string wind_direction_compass { get; set; }
        public string wind_speed { get; set; }


    }
}
