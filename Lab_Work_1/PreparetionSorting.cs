namespace Lab_Work_1;

public class PreparetionSorting
{
    public static PolyFile[] getListFile(string mainFile, int numFile, int number, ref int iteration, ref int extra)
    {
        int first = 0, second = 1;
        Fibonacci.GetFileSize(ref first, ref second, ref iteration, number);
        extra = first + second - number;
        Console.WriteLine("Fibonacci: " + first + " " + second + " " + extra);

        FileWriter.AppendFileBin(mainFile, extra);
        
        PolyFile[] polyFiles = new PolyFile[numFile];

        polyFiles[0] = new PolyFile("Poly" + 1, first, 1);
        
        polyFiles[1] = new PolyFile("Poly" + 2, second, 1);

        polyFiles[2] = new PolyFile("Poly" + 3,  0);

        return polyFiles;
    }

    public static PolyFileMod[] getListFile2(string mainFile, int numFile, long number, ref int iteration, ref long extra)
    {
        int[] fibonacci = Fibonacci.getFileSize3(number, numFile, ref iteration, ref extra);

        // Console.WriteLine("File size: ");
        // foreach (var VARIABLE in fibonacci)
        // {
        //     Console.WriteLine(VARIABLE);
        // }
        
        FileWriter.AppendFileBin(mainFile, extra);

        PolyFileMod[] polyFiles = new PolyFileMod[numFile];
        
        
        for (int i = 0; i < numFile - 1; i++)
        {
            polyFiles[i] = new PolyFileMod("Poly" + (i + 1), fibonacci[i], 1);
        }
        polyFiles[numFile - 1] = new PolyFileMod("Poly" + numFile, 0, 0);

        return polyFiles;

    }

    public static void CreateStartFiles(PolyFileMod[] polyFiles, string mainFile)
    {
        FileInfo fileInfo = new FileInfo(mainFile);
        using (BinaryReader binaryReader = new BinaryReader(fileInfo.OpenRead()))
        {
            for (int i = 0; i < polyFiles.Length; i++)
            {
                FileInfo file = new FileInfo(polyFiles[i].FileName);
                file.Delete();
                using (BinaryWriter binaryWriter = new BinaryWriter(file.OpenWrite()))
                {
                    for (int j = 0; j < polyFiles[i].Length; j++)
                    {
                        int num = binaryReader.ReadInt32();
                        binaryWriter.Write(num);
                    }
                }
            }
        }
    }

    public static PolyFileMod[] getFileListMod(string mainFile, long iSize, long idivider, int numFile, ref int iteration, ref long extra)
    {
        long num = iSize / idivider;
        // Console.WriteLine(num);

        int[] fibonacci = Fibonacci.getFileSize3(num, numFile, ref iteration, ref extra);
        // foreach (var VARIABLE in fibonacci)
        // {
        //     Console.WriteLine(VARIABLE);
        // }

        // Console.WriteLine("Extra = " + extra);
        extra = extra * idivider - (iSize - idivider * num);
        // Console.WriteLine("Extra = " + extra / idivider);
        FileWriter.AppendFileBin(mainFile, extra);
        
        PolyFileMod[] polyFiles = new PolyFileMod[numFile];
        
        for (int i = 0; i < numFile - 1; i++)
        {
            polyFiles[i] = new PolyFileMod("Poly" + (i + 1), fibonacci[i] * idivider, idivider);
        }
        polyFiles[numFile - 1] = new PolyFileMod("Poly" + numFile, 0, 0);

        return polyFiles;
        
    }
    
    public static void CreateStartFilesMode(PolyFileMod[] polyFiles, string mainFile, long idiliver)
    {
        FileInfo fileInfo = new FileInfo(mainFile);
        using (BinaryReader binaryReader = new BinaryReader(fileInfo.OpenRead()))
        {
            for (int i = 0; i < polyFiles.Length; i++)
            {
                Console.WriteLine("create file number " + (i + 1));
                FileInfo file = new FileInfo(polyFiles[i].FileName);
                file.Delete();
                using (BinaryWriter binaryWriter = new BinaryWriter(file.OpenWrite()))
                {
                    for (int j = 0; j < polyFiles[i].Length / idiliver; j++)
                    {
                        int[] arr = new int[idiliver];
                        for (int k = 0; k < idiliver; k++)
                        {
                            arr[k] = binaryReader.ReadInt32();
                        }
                        Array.Sort(arr);
                        for (int k = 0; k < idiliver; k++)
                        {
                            binaryWriter.Write(arr[k]);
                        }
                        
                    }
                }
            }
        }
    }
}