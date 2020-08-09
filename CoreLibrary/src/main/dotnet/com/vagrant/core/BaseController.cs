using driver;
using Enum;
using System;

namespace core
{
    public class BaseController
    {
        private static Driver driver = new Driver();

        public BaseController()
        {
            
        }
        /**
           BeforeAll, Before, After , AfterAll works based on parameters defined in the test_config.json
        */
        public static void StartService(DriverType driverType)
        {
            Console.WriteLine("Start StartService");
            driver.StartService(driverType);
            Console.WriteLine("End StartService");
        }

        public static void SetupIndependentDriver(DriverType driverType)
        {
            Console.WriteLine("Start SetupIndependentDriver");
            driver.StartIndependentDriver(driverType);
            Console.WriteLine("End SetupIndependentDriver");
        }

        public static void StopIndependentDriver(DriverType driverType)
        {
            Console.WriteLine("Start StopIndependentDriver");
            driver.StopIndependentdriver(driverType);
            Console.WriteLine("End StopIndependentDriver");
        }

        public static void StopService(DriverType driverType)
        {
            Console.WriteLine("Start StopService");
            driver.StopService(driverType);
            Console.WriteLine("End StopService");
        }       
    }
}
