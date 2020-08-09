using api;
using core;
using Enum;
using helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using steps;
using System;
using System.Collections.Generic;
using utility;

namespace test
{
    [TestClass]
    public class Test: BaseController
    {
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Console.WriteLine("ClassInitialize");
            StartService(DriverType.CHROME);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Console.WriteLine("TestInitialize");
            ReportHelper.CreateExtentTest(TestContext.TestName);
            SetupIndependentDriver(DriverType.CHROME);
        }
        
        [TestMethod]
        public void HappyFlowTest()
        {
            ValidateTemperature();
        }

        private void ValidateTemperature()
        {
            //API implementation
            string kelvin_ui = WeatherAPI.GetWeatherFromOpenWeatherMap("Indore");
            
            HomeSteps homeSteps = new HomeSteps();
            homeSteps.NavigateToWeatherScreen(CommonUtil.GetTokenValue("WEB_APP_URL"));
            WeatherSteps weatherSteps = new WeatherSteps();
            String[] temperature = weatherSteps.GetCityTemperature("Indore");

            //compare celsius UI value to api kelvin
            bool celsius_result = VarianceImpl.Comparator("CELSIUS", temperature[0], kelvin_ui);
            //compare fahrenheit UI value to api kelvin
            bool fahrenheit_result = VarianceImpl.Comparator("FAHRENHEIT", temperature[1], kelvin_ui);

            if (celsius_result)
            {
                ReportHelper.WritePass("Celsius comparison passed successfully, Celsius UI value = "+ temperature[0] + ", Kelvin API value = " + kelvin_ui);
            }
            else
            {
                ReportHelper.WriteFail("Celsius comparison failed , Celsius UI value = " + temperature[0] + ", Kelvin API value = " + kelvin_ui);
            }

            if (fahrenheit_result)
            {
                ReportHelper.WritePass("Fahrenheit comparison passed successfully , Fahrenheit UI value = " + temperature[0] + ", Kelvin API value = " + kelvin_ui);
            }
            else
            {
                ReportHelper.WriteFail("Fahrenheit comparison failed , Fahrenheit UI value = " + temperature[0] + ", Kelvin API value = " + kelvin_ui);
            }

            //Dictionary<string,string> kvp = weatherSteps.GetWeatherInfo("alwar");

        }

        [TestCleanup]
        public void TestCleanup()
        {
            Console.WriteLine("TestCleanup");
            StopIndependentDriver(DriverType.CHROME);
        }

       [ClassCleanup]
        public static void ClassCleanup()
        {
            Console.WriteLine("ClassCleanup");
            StopService(DriverType.CHROME);
        }

        
    }
}
