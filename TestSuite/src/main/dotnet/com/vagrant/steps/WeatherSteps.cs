using System;
using System.Collections.Generic;
using helper;
using OpenQA.Selenium;
using pages;
using utility;

namespace steps
{
    public class WeatherSteps
    {

        public WeatherSteps()
        {
        }


         
        public string[] GetCityTemperature(String city)
        {
            String[] cityTemp = null;
            Weather weather= new Weather(Global.webdriver);
            try
            {
                city = Weather.CapitalizeFirstLetter(city);
                weather.EnterSearchValue(city);
                weather.CheckCity(city);
                cityTemp = weather.GetTemp(weather.GetCityContainer(city));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception =" + e);
                ReportHelper.WriteFail("Test execution failed at Weather screen");
                ExceptionHelper.TerminateTestCase("Exception occurs while navigating to Weather screen");
            }
            return cityTemp;
        }   
        
        public Dictionary<string, string> GetWeatherInfo(String city)
        {
            Dictionary<string,string> kvp = null;
            Weather weather = new Weather(Global.webdriver);
            try
            {
                city = Weather.CapitalizeFirstLetter(city);
                weather.EnterSearchValue(city);
                weather.CheckCity(city);
                IWebElement cityElement = weather.GetCityContainer(city);
                weather.SelectCity(cityElement);
                kvp = weather.GetWeatherMap(cityElement);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception =" + e);
                ReportHelper.WriteFail("Test execution failed at Weather screen");
                ExceptionHelper.TerminateTestCase("Exception occurs while navigating to Weather screen");
            }
            return kvp;
        }
        
    }
}