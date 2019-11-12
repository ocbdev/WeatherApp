using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{

    public class WeatherStatus
    {
        public String Name { get; set; }
        public String Abbreviation { get; set; }

        public String Icon
        {
            get
            {
                return "https://www.metaweather.com/static/img/weather/png/64/" + Abbreviation + ".png";
            }
        }

        public WeatherStatus(string name, string abbreviation)
        {
            Name = name;
            Abbreviation = abbreviation;
        }
    }
}
