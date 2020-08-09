using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Winium;
using utility;

namespace driver
{
    internal abstract class DriverManager
    {
        
        protected DriverManager()
        {
        }

        internal abstract void StartService();

        internal abstract void StopService();

        internal abstract void CreateDriver();

        internal IWebDriver GetWebDriver()
        {
            if (null == Global.webdriver)
            {
                this.CreateDriver();
            }
            return Global.webdriver;
        }
        internal void QuitWebDriver()
        {
            if (null != Global.webdriver)
            {
                Global.webdriver.Quit();
                Global.webdriver = null;
            }

        }

     
    }
}