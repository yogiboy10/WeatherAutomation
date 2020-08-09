using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Enum;
using System.Diagnostics;
using utility;
using NickStrupat;
using HtmlAgilityPack;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;

namespace helper
{
    public class FileHelper
    {
        public static void InitializeConfig()
        {
            if (Global.testConfig == null)
            {
                Global.testConfig = FileHelper.ReadJsonFileAsJToken(FileType.JSON, @"src\main\resources\config\", "test_config.json");
                Console.WriteLine("Test Config = " + Global.testConfig);
                Console.WriteLine("OS Version = " + new ComputerInfo().OSVersion);
                Console.WriteLine("OS Name = " + new ComputerInfo().OSFullName);
                Console.WriteLine("OS Platform = " + new ComputerInfo().OSPlatform);
                Console.WriteLine("IP Address = " + CommonUtil.GetIPAddress());

            }
        }
        /** Read Json file in Json folder and list the value for specific key */
        public static String ReadJsonValueAgainstKey(String key, FileType fileType, String filePath, String fileName)
        {
            JToken value = null;
            String streamText = ReadSpecificFile(fileType, @filePath, fileName);
            JToken jToken = ParseToJson(streamText);
            Debug.WriteLine("\nEnv - " + jToken.SelectToken(key));
            if(jToken!=null){
                value = jToken.SelectToken(key);
            }else{
                Debug.WriteLine("JSon parsing failed.. value = "+ value);
            }
            return value.ToString();
        }
        public static JToken ReadJsonFileAsJToken(FileType fileType, String filePath, String fileName)
        {
            String streamText = ReadSpecificFile(fileType, @filePath, fileName);
            JToken jToken = ParseToJson(streamText);
            Debug.WriteLine("JSon parsed value = "+ jToken);
            return jToken;
        }

        public static JObject ParseToJson(String streamText)
        {
            JObject jObject = JObject.Parse(streamText);
            return jObject;
        }

        public static String ReadSpecificFile(FileType fileType, String dir, String fileName)
        {
            var stream = "";
            string[] filePaths = GetAllFilesByType(fileType, dir);
            foreach (String file in filePaths)
            {
                Console.WriteLine("\nFile Name : " + Path.GetFileName(file));
                if (fileName.Equals(Path.GetFileName(file)))
                {
                    stream = File.ReadAllText(file);
                }
            }
            return stream;
        }

        public static String[] GetAllFilesByType(FileType fileType, string dir)
        {
            string[] filePaths = null;
            // FileType type = (FileType)System.Enum.Parse(typeof(FileType), fileType);
            switch (fileType)
            {
                case FileType.JSON:
                    filePaths = Directory.GetFiles(dir, "*.json*");
                    break;
                case FileType.XML:
                    filePaths = Directory.GetFiles(dir, "*.xml*");
                    break;
                default:
                    break;
            }
            return filePaths;
        }



        public static void CallGC()
        {
            GC.Collect();
        }
    }
}
