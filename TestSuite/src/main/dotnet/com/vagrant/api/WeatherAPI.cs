using helper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace api
{
    class WeatherAPI
    {
        private static string apiId = "7fe67bf08c80ded756e598d6f8fedaea";
        public static string GetWeatherFromOpenWeatherMap(string city)
        {
            string kelvin = null;
            JObject obj =WebAPIHelper.Invoke("GET", "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + apiId);
            if (obj != null)
            {
                kelvin = obj["main"]["temp"].ToString();
            }

            return kelvin;
        }
    }
}
