using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace spider.Application;

public  class BushFileService
{
    private static readonly string directory = Directory.GetCurrentDirectory();
    public static void createBushFile(List<string> numbers, string folderPath,NetworkCredential credits)
    {
      using (var network = new NetworkConnection(folderPath, credits))
      {
        network.Connect();
        if (Directory.Exists(folderPath)) Directory.Delete(folderPath,true);

        Directory.CreateDirectory(folderPath);

        string textFile = Path.Combine(folderPath,$"{DateTime.UtcNow.ToString("yyyyMMddHHmm")}.dat");
        using(FileStream f=new(textFile, FileMode.OpenOrCreate))
        {
            using (StreamWriter text = new((f),Encoding.GetEncoding(1251)))
            {
                foreach (string item in numbers)
                {
                    text.WriteLine(item);
                }
            }
        }
      }
    }
}