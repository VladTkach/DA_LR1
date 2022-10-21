using System.Diagnostics;

namespace Lab_Work_1;

public class FileReader
{
    public static void showFile(string fileName)
    {
        FileInfo fileInfo = new FileInfo(fileName);
        if (fileInfo.Exists)
        {
            long size = fileInfo.Length / 4;

            using (BinaryReader binaryReader = new BinaryReader(fileInfo.OpenRead()))
            {
                for (int i = 0; i < size; i++)
                {
                    Console.Write(binaryReader.ReadInt32() + " ");
                }

                Console.WriteLine("\n");
            }
        }

        else
        {
            Console.WriteLine("File not exist");
        }
    }

    public static void TestFile(string fileName, long extra)
    {
        Console.WriteLine("Test FIle:  " + fileName);
        FileInfo fileInfo = new FileInfo(fileName);
        int num = 50;
        using (BinaryReader binaryReader = new BinaryReader(fileInfo.OpenRead()))
        {
            for (int i = 0; i < num; i++)
            {
                Console.Write(binaryReader.ReadInt32() + " ");
            }

            Console.WriteLine("\n");
            binaryReader.BaseStream.Position = fileInfo.Length - 4 * (num + extra);
            for (int i = 0; i < num; i++)
            {
                Console.Write(binaryReader.ReadInt32() + " ");
            }

            Console.WriteLine("\n");
        }
    }
    
    public static void BigTestFile(string fileName)
    {
        Console.WriteLine("Test FIle:  " + fileName);
        FileInfo fileInfo = new FileInfo(fileName);
        long size = fileInfo.Length / 4;
        int num = 50;
        using (BinaryReader binaryReader = new BinaryReader(fileInfo.OpenRead()))
        {
            int temp = binaryReader.ReadInt32();
            int next = 0;
            for (int i = 0; i < size - 1; i++)
            {
                next = binaryReader.ReadInt32();
                if (temp > next)
                {
                    Console.WriteLine("Fuuuuuuuck");
                }

                temp = next;
            }
        }
    }
}