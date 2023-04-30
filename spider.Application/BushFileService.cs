using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace spider.Application;

public  class BushFileService
{
    private static readonly string directory = Directory.GetCurrentDirectory();
    public static void createBushFile(List<string> numbers, string folderName)
    {
        string folderPath = @$"\\192.168.1.116\expl\{folderName}";
        if (Directory.Exists(folderPath)) Directory.Delete(folderPath);
        Directory.CreateDirectory(folderPath);

        string textFile = Path.Combine(folderPath, "test.txt");

        using (StreamWriter text = File.CreateText(textFile))
        {
            foreach (string item in numbers)
            {
                text.WriteLine(item);
            }
        }
    }
}