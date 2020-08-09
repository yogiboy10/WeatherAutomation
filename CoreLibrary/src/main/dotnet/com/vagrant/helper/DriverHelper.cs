using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Enum;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Winium;
using utility;
using System.Runtime.InteropServices;

namespace helper
{

    public class DriverHelper
    {
        private Object driver;
        public DriverHelper(Object driver){
            this.driver = driver;
        }

        public void LaunchWebApp(String URL)
        {
            if (Global.webdriver != null)
            {
                Global.webdriver.Navigate().GoToUrl(URL);
                String currentWindow = Global.webdriver.CurrentWindowHandle;
                Global.webdriver.SwitchTo().Window(currentWindow);
            }
            else
            {
                Console.WriteLine("Webdriver is null, Webdriver = " + Global.webdriver);
            }
        }

        public void WaitForPageLoad()
        {
            WebDriverWait wait = null;
            if (driver is IWebDriver)
            {
                wait = new WebDriverWait(Global.webdriver, TimeSpan.FromSeconds(60));
                wait.Until(driver => ((IJavaScriptExecutor)Global.webdriver).ExecuteScript("return document.readyState").Equals("complete"));
            }

            
        }
        public void EnterText(IWebElement webElement, string value)
        {
            try
            {
                webElement.Clear();
                webElement.SendKeys(value);
            }
            catch(NoSuchElementException e)
            {
                ReportHelper.WriteFail("Exception  occurs when entering the text = " + value+ ", Element = "+ webElement);
            }
            
        }
        public void Click(IWebElement webElement)
        {
            try
            {
                webElement.Click();
            }
            catch (NoSuchElementException e)
            {
                ReportHelper.WriteFail("Exception occurs when clicking the element= "+webElement);
            }
            
        }

        public string GetText(IWebElement webElement)
        {
            String text = "";
            try
            {
                text = webElement.Text;
            }
            catch (NoSuchElementException e)
            {
                ReportHelper.WriteFail("Exception occurs when fetching the text from the element = " + webElement);
            }
            return text;
        }
        public string GetAttribute(IWebElement webElement, string attribute)
        {
            String text = "";
            try
            {
                text = webElement.GetAttribute(attribute);
            }
            catch (NoSuchElementException e)
            {
                ReportHelper.WriteFail("Exception occurs when fetching the attribute = "+attribute+" from the element = " + webElement);
            }
            return text;
        }
        public IWebElement GetWebElement(String locator, LocatorType locatorType)
        {
            IWebElement element = null;
            try
            {
                By by = this.GetLocator(locator, locatorType);
                if (driver is IWebDriver)
                {
                    element = Global.webdriver.FindElement(by);
                }
            }
            catch (NoSuchElementException e)
            {
                ReportHelper.WriteFail("Exception occurs when fetching the web element for the locator = " + locator+ " & locator type " + locatorType);
            }
            return element;
        }
        public ReadOnlyCollection<IWebElement> GetWebElements(String locator, LocatorType locatorType)
        {
            ReadOnlyCollection<IWebElement> element = null;
            try
            {
                By by = this.GetLocator(locator, locatorType);
                if (driver is IWebDriver)
                {
                    element = Global.webdriver.FindElements(by);
                }
            }
            catch (NoSuchElementException e)
            {
                ReportHelper.WriteFail("Exception occurs when fetching the web elements for the locator = " + locator + " & locator type " + locatorType);
            }
            return element;
        }
        public By GetLocator(string locator, LocatorType locatorType)
        {
            By by = null;
            try
            {
                switch (locatorType)
                {
                    case LocatorType.ID:
                        by = By.Id(locator);
                        break;
                    case LocatorType.NAME:
                        by = By.Name(locator);
                        break;
                    case LocatorType.XPATH:
                        by = By.XPath(locator);
                        break;
                    case LocatorType.CSS:
                        by = By.CssSelector(locator);
                        break;
                    case LocatorType.CLASS:
                        by = By.ClassName(locator);
                        break;
                    case LocatorType.LINK_TEXT:
                        by = By.LinkText(locator);
                        break;
                    case LocatorType.PARTIAL_LINK_TEXT:
                        by = By.PartialLinkText(locator);
                        break;
                    default:
                        break;

                }
            }
            catch (NoSuchElementException e)
            {
                ReportHelper.WriteFail("Exception occurs on finding the elements for the locator = " + locator + " & locator type " + locatorType);
            }
            
            return by;
        }

        public void SelectElement(DropdownType dropDownType, IWebElement webElement, [Optional] String value,[Optional] int index)
        {
            try
            {
                SelectElement select = new SelectElement(webElement);
                switch (dropDownType)
                {
                    case DropdownType.TEXT:
                        select.SelectByText(value);
                        break;

                    case DropdownType.INDEX:
                        select.SelectByIndex(index);
                        break;


                    case DropdownType.VALUE:
                        select.SelectByValue(value);
                        break;

                    default:
                        break;
                }
            }
            catch (NoSuchElementException e)
            {
                ReportHelper.WriteFail("Exception occurs when selecting the web elements = " + webElement+ " , value = " + value +
                    ", Index = "+ index+", Drop down type = "+ dropDownType);
            }
           
        }


      

        public bool WaitUntilElementIsVisible(By by, long timeout, long pollInterval)
        {
            IWebElement element = null;
            bool status = false;
            try
            {
                DefaultWait<IWebDriver> fluentwait = null;
                if (driver is IWebDriver)
                {
                    fluentwait = new DefaultWait<IWebDriver>(Global.webdriver);
                }
                DefaultWait(fluentwait, timeout, pollInterval);
                element = fluentwait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
                status = element.Displayed;
            }
            catch (NoSuchElementException e)
            {
                ReportHelper.WriteFail("Exception occurs on waiting for the element till it is visible where element = " + by);
            }

            return status;
        }
        public bool WaitUntilInvisibilityOfElementLocated(By by, long timeout, long pollInterval)
        {
            DefaultWait<IWebDriver> fluentwait = null;
            bool status = false; ;
            try
            {
                if (driver is IWebDriver)
                {
                    fluentwait = new DefaultWait<IWebDriver>(Global.webdriver);
                }
                DefaultWait(fluentwait, timeout, pollInterval);
                status = fluentwait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
            }
            catch (NoSuchElementException e)
            {
                ReportHelper.WriteFail("Exception occurs on waiting for the element till it get invisible where element = " + by);
            }
            return status;
        }
        public void DefaultWait(DefaultWait<IWebDriver> fluentwait, long timeout, long pollIntervalInSeconds)
        {
            fluentwait.Timeout = TimeSpan.FromMilliseconds(timeout);
            fluentwait.PollingInterval = TimeSpan.FromMilliseconds(pollIntervalInSeconds);
            fluentwait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        public bool IsElementPresent(IWebElement webElement)
        {
            try
            {
                return webElement.Displayed;
            }
            catch (NoSuchElementException)
            {
                ReportHelper.WriteFail("Exception occurs on finding the element = " + webElement);
                return false;
            }
        }

        public IWebElement SearchElementUsingIWebElement(IWebElement rootElement, By searchElement)
        {
            IWebElement returnElement = null;
            if (rootElement.FindElement(searchElement).Displayed)
            {
                returnElement  = rootElement.FindElement(searchElement);
            }
            return returnElement;
        }
        public ReadOnlyCollection<IWebElement> SearchElementsUsingIWebElement(IWebElement rootElement, By searchElement)
        {
            return rootElement.FindElements(searchElement);
        }
       
    }
}