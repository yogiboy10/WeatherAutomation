using helper;
using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace utility
{
    public class CommonUtil
    {
        private static Random random = new Random();
        public static String RandomGenerator(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static String RandomStringGenerator(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static String RandomIntGenerator(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static String RandomIntGeneratorWithoutZero(int length)
        {
            const string chars = "123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string AppendTimeStamp(string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Path.GetExtension(fileName)
                );
        }

        
        public static void RunProcess(String condition, String processName)
        {
            try
            {
                if ("start".Equals(condition))
                {
                    System.Diagnostics.Process.Start(processName);
                }
                else if ("kill".Equals(condition))
                {
                    foreach (Process proc in Process.GetProcessesByName(processName))
                    {
                        proc.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static void RunCommandsInCmdLine(String command,String fileName)
        {
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = fileName;
                //Path of exe that will be executed, only for "filebuffer" it will be "flvtool2.exe"
                proc.StartInfo.Arguments = command;
                //The command which will be executed
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.RedirectStandardOutput = false;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static String GetIPAddress()
        {
            string ipAddress = "";
            if (Dns.GetHostAddresses(Dns.GetHostName()).Length > 0)
            {
                ipAddress = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
            }
            return ipAddress;
        }

        public static String GetTokenValue(String key)
        {
            String value = "";
            try
            {
                value = Global.testConfig.SelectToken(key).ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception =" + e);
                ReportHelper.WriteFail("Test Failed due to incorrect json data");
                ExceptionHelper.TerminateTestCase("Exception occurs while reading value from json. No matching key or value available..");
            }
            return value;
        }
        public static DirectoryInfo MakeDirectory(String root)
        {
            DirectoryInfo dir = null;
            try
            {
                if (!Directory.Exists(root))
                {
                    dir = Directory.CreateDirectory(root);
                }
                else
                {
                    dir = new DirectoryInfo(root);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception =" + e);
                ReportHelper.WriteFail("Test Failed during directory creation");
                ExceptionHelper.TerminateTestCase("Exception occurs while creating the directory. Path= " + root);
            }
            return dir;
        }
        public static void DeleteDirectory(String root)
        {
            try
            {
                if (Directory.Exists(root))
                {
                    DirectoryInfo directory = new DirectoryInfo(root);
                    foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
                    foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception =" + e);
                ReportHelper.WriteFail("Test Failed during directory deletion");
                ExceptionHelper.TerminateTestCase("Exception occurs while deleting the files and folder under the directory. Path= "+ root);
            }
           
        }
        public static void DeleteFileByName(String path,String fileName)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo directory = new DirectoryInfo(path);
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        if (file.Name.Equals(fileName))
                        {
                            file.Delete();
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception =" + e);
                ReportHelper.WriteFail("Test Failed during directory deletion");
                ExceptionHelper.TerminateTestCase("Exception occurs while deleting the file under the directory. Path= " + path+ ", File Name = "+ fileName);
            }

        }

        public static FileInfo GetFileByName(String path, String fileName)
        {
            FileInfo returnFile = null;
            try
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo directory = new DirectoryInfo(path);
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        if (file.Name.Equals(fileName))
                        {
                            returnFile = file;
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception =" + e);
                ReportHelper.WriteFail("Expected file does not exists, file name = "+ fileName + ", Path = "+ path);
            }
            return returnFile;
        }
        public static void DeleteFileByType(String path, String fileName,String fileType)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo directory = new DirectoryInfo(path);
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        if (file.Name.Equals(fileName) && file.Extension.Equals(fileType))
                        {
                            file.Delete();
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception =" + e);
                ReportHelper.WriteFail("Test Failed during directory deletion");
                ExceptionHelper.TerminateTestCase("Exception occurs while deleting the file under the directory. Path= " + path + ", File Name = " + fileName);
            }

        }

        public static void ExtractFile(String zipPath, string extractPath)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipPath, extractPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception during file extract of the zip file, Exception = " + e);
            }

        }

        public static bool IsFileAlreadyExists(String fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.Exists;
        }
        public static void CompressFile(String reportDir , String zipPath)
        {
            try
            {
                System.IO.Compression.ZipFile.CreateFromDirectory(reportDir, zipPath, CompressionLevel.Fastest,false);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception during file compression for the report email attachment, Exception = " + e);
            }

        }


        public static void UpdateScreenShotPath()
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.Load(Global.currentReportDirectoryInfo.FullName+@"\index.html");
            var centralDiv = htmlDocument.DocumentNode.SelectNodes("//ul[@class='attachments']/li/a");
            Console.WriteLine("Output: " + centralDiv);
            if (centralDiv != null)
            {
                foreach (HtmlNode htmlNode in centralDiv)
                {
                    String href = htmlNode.GetAttributeValue("href", "");
                    foreach (String value in Global.screenshotNameList)
                    {
                        if (href.Contains(value))
                        {
                            htmlNode.SetAttributeValue("href", @".\screenshots\" + value + ".png");
                            break;
                        }
                    }
                    continue;
                }
                htmlDocument.Save(Global.currentReportDirectoryInfo.FullName + @"\index.html");

            }
        }


        
    }
}