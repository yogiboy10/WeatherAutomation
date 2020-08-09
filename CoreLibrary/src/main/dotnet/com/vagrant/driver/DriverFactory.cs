using System;
using Enum;

namespace driver
{
    internal class DriverFactory
    {
        internal static DriverManager getManager(DriverType type)
        {
            Object driverManager = null;
            switch (type)
            {
                case DriverType.CHROME:
                    driverManager = new ChromeManager();
                    break;
                default:
                    driverManager = new ChromeManager();
                    break;
            }

            return (DriverManager)driverManager;
        }
    }
}