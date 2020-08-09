using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace utility
{
    public class VarianceImpl
    {
        public static bool Comparator(String type, string ui, string api_kelvin)
        {
            float f = float.Parse(ui);
            double ui_kelvin_double = 0.0;
            if (type.Equals("CELSIUS"))
            {
                ui_kelvin_double = Celsius_to_Kelvin(f);
            }
            else
            {
                ui_kelvin_double = Math.Round(Fahrenheit_to_Kelvin(f) * 1000.0) / 1000.0;
            }

            string variance_kelvin = Global.testConfig.SelectToken("VARIANCE.KELVIN").ToString();
            double variance_kelvin_double = Double.Parse(variance_kelvin);
            double api_kelvin_double = Double.Parse(api_kelvin);

            if (Math.Abs(ui_kelvin_double - api_kelvin_double) <= variance_kelvin_double)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static float Celsius_to_Kelvin(float C)
        {
            return (float)(C + 273.15);
        }

        private static float Fahrenheit_to_Kelvin(float F)
        {
            return 273.5f + ((F - 32.0f) *
                            (5.0f / 9.0f));
        }

    }
}
