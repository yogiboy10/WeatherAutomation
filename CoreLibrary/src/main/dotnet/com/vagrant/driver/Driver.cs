using System;
using System.Diagnostics;
using Enum;
using utility;

namespace driver
{
    public class Driver
    {
        private static DriverManager driverManager;

        public void SetupDriver()
        {
            Console.WriteLine("Start Setup");
            try
            {
                    String driverType = Global.testConfig.SelectToken("DRIVER_TYPE").ToString();
                    if (driverType != null)
                    {
                        DriverType driverTypeEnum = (DriverType)System.Enum.Parse(typeof(DriverType), driverType);
                        InitDriver(driverTypeEnum);
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException());
            }
            Console.WriteLine("End Setup");
        }
        public void TearDriver()
        {
            try
            {
                    String driverType = Global.testConfig.SelectToken("DRIVER_TYPE").ToString();
                    if (driverType != null)
                    {
                        DriverType driverTypeEnum = (DriverType)System.Enum.Parse(typeof(DriverType), driverType);
                        QuitDriver(driverTypeEnum);
                    }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.GetBaseException());
            }
        }
        public void InitDriver(DriverType driverType)
        {
                    switch (driverType)
                    {
                        case DriverType.CHROME:
                            if (Global.webdriver == null)
                            {
                                driverManager = DriverFactory.getManager(driverType);
                                Global.webdriver = driverManager.GetWebDriver();
                            }
                            break;
                        default:
                            break;
                    }
        }

        public void QuitDriver(DriverType driverType)
        {
                    switch (driverType)
                    {
                        case DriverType.CHROME:
                            if (Global.webdriver == null)
                            {
                                driverManager = DriverFactory.getManager(driverType);
                            }
                            driverManager.QuitWebDriver();
                            Global.webdriver = null;
                            break;
                        default:
                            if (Global.webdriver == null)
                            {
                                driverManager = DriverFactory.getManager(driverType);
                            }
                            driverManager.QuitWebDriver();
                            Global.webdriver = null;
                            break;
                    }
        }

        public void StartService(DriverType driverType)
        {
            Console.WriteLine("Start StartService");
            driverManager = DriverFactory.getManager(driverType);
            if (driverManager != null)
            {
                driverManager.StartService();
            }
            Console.WriteLine("End StartService");
        }
        public void StopService(DriverType driverType)
        {
            driverManager = DriverFactory.getManager(driverType);
            if (driverManager != null)
            {
                driverManager.StopService();
            }
        }

        public void StartIndependentDriver(DriverType driverType)
        {
            switch (driverType)
            {
                case DriverType.CHROME:
                    if (Global.webdriver == null)
                    {
                        driverManager = DriverFactory.getManager(driverType);
                        Global.webdriver = driverManager.GetWebDriver();
                    }
                    break;
                default:
                    break;
            }
        }

        public void StopIndependentdriver(DriverType driverType)
        {
            switch (driverType)
            {
                case DriverType.CHROME:
                    if (Global.webdriver == null)
                    {
                        driverManager = DriverFactory.getManager(driverType);
                    }
                    driverManager.QuitWebDriver();
                    Global.webdriver = null;
                    break;
                default:
                    if (Global.webdriver == null)
                    {
                        driverManager = DriverFactory.getManager(driverType);
                    }
                    driverManager.QuitWebDriver();
                    Global.webdriver = null;
                    break;
            }
        }
    }
}