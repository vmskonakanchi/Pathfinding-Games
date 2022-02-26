using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class CSVWriter
{
    public static void CreateCSV(string filePath, string nameOfColumns)
    {
        TextWriter tw = new StreamWriter(filePath, false);
        tw.WriteLine(nameOfColumns, false);
        tw.Close();
    }
    public static void WriteToCsv(string filePath, List<User> users)
    {
        TextWriter tw = new StreamWriter(filePath, true);
        if (users.Count > 0)
        {
            foreach (var item in users)
            {
                tw.WriteLine(item.rating + "," + item.changeText + "," + item.otherText);
            }
        }
        tw.Close();
    }
}