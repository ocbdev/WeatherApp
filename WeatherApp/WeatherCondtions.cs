using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class WeatherCondtions
    {
        public static List<WeatherStatus> SetWeatherStatus()
        {

            List<WeatherStatus> weatherstates = new List<WeatherStatus>();
            weatherstates.Add(new WeatherStatus("Snow", "sn"));
            weatherstates.Add(new WeatherStatus("Sleet", "sl"));
            weatherstates.Add(new WeatherStatus("Hail", "h"));
            weatherstates.Add(new WeatherStatus("Thunderstorm", "t"));
            weatherstates.Add(new WeatherStatus("Heavy Rain", "hr"));
            weatherstates.Add(new WeatherStatus("Light Rain", "lr"));
            weatherstates.Add(new WeatherStatus("Showers", "s"));
            weatherstates.Add(new WeatherStatus("Heavy Cloud", "hc"));
            weatherstates.Add(new WeatherStatus("Light Cloud", "lc"));
            weatherstates.Add(new WeatherStatus("Clear", "c"));
            return weatherstates;

        }
    }
}
