using System;
using Enum;
using helper;
using OpenQA.Selenium;
using utility;

namespace pages
{
    public class Home
    {
        private static DriverHelper helper;
        private String sub_menu = "h_sub_menu";
        private String a_weather = "//a[contains(text(),'WEATHER')]";
        private String no_thanks = "No Thanks";
        public Home(Object driver){
            helper = new DriverHelper(driver);
        }

        public void HandleNotifications()
        {
            try
            {
                helper.WaitUntilElementIsVisible(By.LinkText(no_thanks), 15000, 1000);
                IWebElement jsElement = helper.GetWebElement(no_thanks, LocatorType.LINK_TEXT);
                if (helper.IsElementPresent(jsElement))
                {
                    helper.Click(jsElement);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public void ClickSubMenu()
        {
            //helper.WaitForPageLoad();
            HandleNotifications();
            helper.WaitUntilElementIsVisible(By.Id(sub_menu), 10000, 1000);
            IWebElement subMenuElement = helper.GetWebElement(sub_menu, LocatorType.ID);
            if (helper.IsElementPresent(subMenuElement))
            {
                helper.Click(subMenuElement);
            }
        }

        public void NavigateToWeather()
        {
            helper.WaitUntilElementIsVisible(By.XPath(a_weather), 2000, 1000);
            IWebElement weatherElement = helper.GetWebElement(a_weather, LocatorType.XPATH);
            if (helper.IsElementPresent(weatherElement))
            {
                helper.Click(weatherElement);
            }
        }


    }
}