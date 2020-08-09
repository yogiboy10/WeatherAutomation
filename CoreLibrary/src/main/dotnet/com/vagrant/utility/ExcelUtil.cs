using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LinqToExcel;

namespace Excelreader
{
    public class ExcelUtil
    {
        public IDictionary<string, IDictionary<string, string>> ExcelReader(string path)
        {
            var excel1 = new ExcelQueryFactory(path)
            {
                DatabaseEngine = LinqToExcel.Domain.DatabaseEngine.Ace,
                TrimSpaces = LinqToExcel.Query.TrimSpacesType.Both,
                UsePersistentConnection = true,
                ReadOnly = true
            };


            var rows = from p in excel1.Worksheet("Sheet1")
                       select p;
            var columns = from p in excel1.GetColumnNames("Sheet1")
                          select p;
            ArrayList columnHeader = new ArrayList();
            string testcase = string.Empty;
            IDictionary<string, IDictionary<string, string>> dic1 = new Dictionary<string, IDictionary<string, string>>();

            foreach (var firstrow in columns)
            {
                columnHeader.Add(firstrow);

            }
            foreach (LinqToExcel.Row row in rows)
            {
                IDictionary<string, string> dic = new Dictionary<string, string>();
                int i = 0;

                foreach (LinqToExcel.Cell value in row)
                {
                    if (value == null)
                    {
                        i++;
                        continue;
                    }
                    else
                    {
                        if (i == 0)
                        {
                            testcase = value;
                        }
                        else
                        {
                            dic.Add(columnHeader[i].ToString(), value);
                        }
                    }
                    i++;
                }
                dic1.Add(testcase, dic);
            }
            if (dic1 != null)
            {
                foreach (KeyValuePair<string, IDictionary<string, string>> kvp in dic1)
                {
                    Debug.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                    if (kvp.Value != null)
                    {
                        foreach (KeyValuePair<string, string> kvp1 in kvp.Value)
                        {
                            Debug.WriteLine("Key = {0}, Value = {1}", kvp1.Key, kvp1.Value);
                        }
                    }
                }
            }
            return dic1;
        }

        public void ExcelDataWithoutModel(string filePath, string sheetName, string testCaseId)
        {
            var excel = new ExcelQueryFactory(@filePath)

            {
                DatabaseEngine = LinqToExcel.Domain.DatabaseEngine.Ace,
                TrimSpaces = LinqToExcel.Query.TrimSpacesType.Both,
                UsePersistentConnection = true,
                ReadOnly = true
            };

            var planets = from p in excel.WorksheetNoHeader(sheetName)
                          select p;
            int i = 0;


            ArrayList genricList = new ArrayList();
            ArrayList columnHeader = new ArrayList();
            IDictionary<object, object> dic = new Dictionary<object, object>();
            foreach (LinqToExcel.RowNoHeader row in planets)
            {


                foreach (var item in row)
                {
                    genricList.Add(item.Value);


                }
                if (i == 0)
                {
                    columnHeader = genricList;
                    i++;
                }

                int k = genricList.Count;
                int l = columnHeader.Count;
                for (int j = 0; j < l; j++)
                {
                    Console.WriteLine(columnHeader[j]);
                }
            }
        }

    }
}














