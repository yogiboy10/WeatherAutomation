using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Enum;
using helper;
using OpenQA.Selenium;
using utility;

namespace pages
{
    public class Weather
    {
        private static DriverHelper helper;
        private String input_searchbox = "searchBox";
        private String outerContainer = "outerContainer";
        private By tempRedText = By.ClassName("tempRedText");
        private By tempWhiteText = By.ClassName("tempWhiteText");
        private string leaflet_popup_content = "leaflet-popup-content";
        private By temp_info_popup = By.TagName("b");
      

        public Weather(Object driver){
            helper = new DriverHelper(driver);
        }

        public void EnterSearchValue(String city)
        {
            //helper.WaitForPageLoad();
            helper.WaitUntilElementIsVisible(By.Id(input_searchbox), 10000, 1000);
            IWebElement searchElement = helper.GetWebElement(input_searchbox, LocatorType.ID);
            if (helper.IsElementPresent(searchElement))
            {
                helper.EnterText(searchElement, city);
            }
        }

        public void CheckCity(String city)
        {
            IWebElement cityElement = helper.GetWebElement(city, LocatorType.ID);
            helper.Click(cityElement);
        }

        public IWebElement GetCityContainer(String city)
        {
            IWebElement returnElement = null;
            helper.WaitUntilElementIsVisible(By.ClassName(outerContainer), 3000, 1000);
            ReadOnlyCollection<IWebElement> weatherElement = helper.GetWebElements(outerContainer, LocatorType.CLASS);
            foreach(IWebElement cityElement in weatherElement)
            {
                if (cityElement.GetAttribute("title").Equals(city))
                {
                    returnElement =  cityElement;
                    break;
                }
            }
            return returnElement;
        }

        public String[] GetTemp(IWebElement cityElement)
        {
            String[] temp = new String[2];
            if(helper.SearchElementUsingIWebElement(cityElement, tempRedText).Displayed)
            {
                temp[0] = helper.SearchElementUsingIWebElement(cityElement, tempRedText).Text;
                temp[0] = temp[0].Substring(0, 2);
            }
            if (helper.SearchElementUsingIWebElement(cityElement, tempWhiteText).Displayed)
            {
                temp[1] = helper.SearchElementUsingIWebElement(cityElement, tempWhiteText).Text;
                temp[1] = temp[1].Substring(0, 2);
            }
            return temp;
        }

        public void SelectCity(IWebElement cityElement)
        {
            cityElement.Click();
        }


        public Dictionary<String,String> GetWeatherMap(IWebElement cityElement)
        {
            Dictionary<String, String> kvp = new Dictionary<string, string>();
            helper.WaitUntilElementIsVisible(By.ClassName(leaflet_popup_content), 3000, 1000);
            IWebElement popupElement = helper.GetWebElement(leaflet_popup_content, LocatorType.CLASS);
            if (helper.IsElementPresent(popupElement))
            {
                ReadOnlyCollection<IWebElement> bElements = helper.SearchElementsUsingIWebElement(popupElement, temp_info_popup);
                foreach (IWebElement element in bElements)
                {
                    String[] arr = SplitString(element.Text, ":");
                    kvp.Add(arr[0], arr[1]);
                }
            }
            return kvp;
        }

        public static String[] SplitString(String value,String item)
        {
            String[] arr = value.Split(item);
            arr = (from e in arr
                     select e.Trim()).ToArray();
            return arr;
        }

        public static string CapitalizeFirstLetter(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;
            if (s.Length == 1)
                return s.ToUpper();
            return s.Remove(1).ToUpper() + s.Substring(1);
        }



    }
}