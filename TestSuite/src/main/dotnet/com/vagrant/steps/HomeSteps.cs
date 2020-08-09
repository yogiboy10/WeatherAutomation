using System;
using helper;
using pages;
using utility;

namespace steps
{
    public class HomeSteps
    {

        public HomeSteps()
        {
        }


         
        public void NavigateToWeatherScreen(String URL)
        {
            Home home = new Home(Global.webdriver);
            try
            {
                DriverHelper helper = new DriverHelper(Global.webdriver);
                helper.LaunchWebApp(URL);
                home.ClickSubMenu();
                home.NavigateToWeather();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception =" + e);
                ReportHelper.WriteFail("Test execution failed at home screen");
                ExceptionHelper.TerminateTestCase("Exception occurs while navigating to Weather screen");
            }
           
        }      
        
    }
}