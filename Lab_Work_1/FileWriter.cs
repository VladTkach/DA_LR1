using System.IO;

namespace Lab_Work_1;

public class FileWriter
{
    public static void CreateFileBin(string fileName, long size)
    {
        Console.WriteLine("Create File: " + size);
        FileInfo fileInfo = new FileInfo(fileName);
        fileInfo.Delete();
        using (BinaryWriter binaryWriter = new BinaryWriter(fileInfo.OpenWrite()))
        {
            Random random = new Random();
            for (long i = 0; i < size; i++)
            {
                // int rand = random.Next(20);
                int rand = random.Next(int.MinValue, int.MaxValue);
                binaryWriter.Write(rand);
            }
        }
    }

    public static void AppendFileBin(string fileName, long num)
    {
        Console.WriteLine("Append File - " + num);
        using (FileStream stream = new FileStream(fileName, FileMode.Append))
        using (BinaryWriter binaryWriter = new BinaryWriter(stream))
        {
            for (long i = 0; i < num; i++)
            {
                binaryWriter.Write(int.MaxValue);
            }
        }
    } 

    public static long getSize(string size)
    {
        string units = !char.IsNumber(size[^2]) ? size[^2..] : size[^1..];
        string value = !char.IsNumber(size[^2]) ? size[..^2] : size[..^1];

        int coefficient = 1;
        switch (units)
        {
            case "B":
                coefficient = 1;
                break;
            case "KB":
                coefficient = 1024;
                break;
            case "MB":
                coefficient = 1024 * 1024;
                break;
            case "GB":
                coefficient = 1024 * 1024 * 1024;
                break;
        }
        
        return long.Parse(value) * coefficient / 4;
    }
}