using AventStack.ExtentReports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System.Collections;
using System.IO;
using System.Threading;

namespace utility
{
    public class Global
    {
        public static IWebDriver webdriver;
        public static JToken testConfig = null;
        public static string windowsAppName;
        public static AventStack.ExtentReports.ExtentReports extent = null;
        public static ExtentTest extentTest = null;
        public static string rootDirectory;
        public static DirectoryInfo currentReportDirectoryInfo;
        public static string executionPath;
        public static DirectoryInfo screenshotDirectoryInfo;
        public static DirectoryInfo prjDownloadsDirectory;
        public static ArrayList screenshotNameList = new ArrayList();
        public static Thread thread;
        public static DirectoryInfo prjDriversDirectory;
    }
}