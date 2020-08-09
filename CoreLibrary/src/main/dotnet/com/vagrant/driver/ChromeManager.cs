using System;
using System.Diagnostics;
using constants;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using utility;

namespace driver
{
    internal class ChromeManager : DriverManager
    {
        private static ChromeDriverService _chromeDriverService;
        internal override void CreateDriver()
        {
            ChromeOptions options = null;
            Console.WriteLine("Start Chrome Manager - CreateDriver");
            try
            {
                if (_chromeDriverService != null)
                {
                    options = new ChromeOptions();
                    Global.webdriver = new ChromeDriver(_chromeDriverService, options);
                }
                Console.WriteLine("Web Driver = " + Global.webdriver);
                Console.WriteLine("Chrome option = " + options);
                Console.WriteLine("Chrome driver service = " + _chromeDriverService);
                Global.webdriver.Manage().Window.Maximize();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Exception Inside create driver = "+e);
            }
            Console.WriteLine("Start Chrome Manager - CreateDriver");
        }

        internal override void StartService()
        {
            Console.WriteLine("Start Chrome Manager - StartService");
            try
            {
                if (_chromeDriverService == null || !_chromeDriverService.IsRunning)
                {
                    string fileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    Console.WriteLine("Chrome Driver Path = " + fileName);
                    _chromeDriverService = ChromeDriverService.CreateDefaultService(fileName);
                    _chromeDriverService.SuppressInitialDiagnosticInformation = true;
                    _chromeDriverService.Start();
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Exception Inside Start Service = " + e);
            }
            Console.WriteLine("End Chrome Manager - StartService");
        }

        internal override void StopService()
        {
            if (_chromeDriverService.IsRunning)
            {
                _chromeDriverService.Dispose();
            }
            if (_chromeDriverService != null)
            {
                _chromeDriverService = null;
            }
            CommonUtil.RunProcess("kill", AppConstants.CHROME_DRIVER_EXE);
        }
    }
}