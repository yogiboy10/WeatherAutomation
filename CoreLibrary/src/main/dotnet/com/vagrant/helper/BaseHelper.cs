using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using utility;

namespace helper
{
    public class BaseHelper
    {

        public static void PreSetup()
        {
            FileHelper.InitializeConfig();
            SetupDirectoryPath();
            ReportHelper.InitializeExtentReport();
        }

        public static void PostSetup()
        {
            ReportHelper.FlushReport();
            UpdateIndexHtml();
            FileHelper.CallGC();
        }

        public static void UpdateIndexHtml()
        {
            CommonUtil.UpdateScreenShotPath();
        }

        public static void SetupDirectoryPath()
        {
            if (Global.executionPath == null)
            {
                Global.executionPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
            if (Global.rootDirectory == null)
            {
                Global.rootDirectory =
                       Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).
                           Parent.Parent.Parent.FullName;
            }
            if ("true".Equals(CommonUtil.GetTokenValue("FLUSH_REPORT").ToLower()))
            {
                CommonUtil.DeleteDirectory(Global.rootDirectory + "/Reports/");
            }
            if (Global.currentReportDirectoryInfo == null)
            {
                Global.currentReportDirectoryInfo = CommonUtil.MakeDirectory(Global.rootDirectory + "/Reports/" +
                CommonUtil.AppendTimeStamp("Automation_"));
            }
        }
    }
}
