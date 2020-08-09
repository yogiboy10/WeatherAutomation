using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using System;

namespace utility
{
    public class ReportHelper
    {
        public static void InitializeExtentReport()
        {
            // start reporters
            var reporter = new ExtentHtmlReporter(Global.currentReportDirectoryInfo.FullName + "/sanity.html");
            reporter.LoadConfig(Global.executionPath + "/src/main/resources/xml/extent-config.xml");
            // create ExtentReports and attach reporter(s)
            Global.extent = new AventStack.ExtentReports.ExtentReports();
            Global.extent.AttachReporter(reporter);
        }

        public static void CreateExtentTest(String testCaseName)
        {
            // creates a test 
            Global.extentTest = Global.extent.CreateTest(testCaseName);
        }

        public static void FlushReport()
        {
            // test with snapshot
            Global.extent.Flush();
        }



        public static void WriteLog(Status status, String message)
        {
            // log(Status, details)
            Global.extentTest.Log(status, message);
        }

        public static void WriteInfo(String message)
        {
            // info(details)
            Global.extentTest.Info(message);
        }
        public static void WritePass(String message)
        {
            //step pass
            Global.extentTest.Pass(message);
        }
        public static void WriteFail(String message)
        {
            string screenShotPath = Capture(Global.webdriver, "ScreenShot_");
            Global.extentTest.Fail(message + ", Snapshot below... ");
            Global.extentTest.AddScreenCaptureFromPath(screenShotPath);
        }

        public static string Capture(IWebDriver driver, string screenShotName)
        {
            string localpath = "";
            try
            {
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                if (Global.screenshotDirectoryInfo == null)
                {
                    Global.screenshotDirectoryInfo = CommonUtil.MakeDirectory(Global.currentReportDirectoryInfo.FullName + "/screenshots");
                }
                String screenshotNameAppend = CommonUtil.AppendTimeStamp(screenShotName);
                string finalpth = Global.screenshotDirectoryInfo.FullName + "/" + screenshotNameAppend + ".png";
                localpath = new Uri(finalpth).LocalPath;
                screenshot.SaveAsFile(localpath);
                Global.screenshotNameList.Add(screenshotNameAppend);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception = " + e);
            }

            return localpath;
        }
 

    }
}
