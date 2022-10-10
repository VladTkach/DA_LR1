namespace Lab_Work_1;

public class PolyphaseMargeSort
{
    public static void Start(PolyFile[] polyFiles, int iteration)
    {
        int currentFile = 2;
        int minLengthFile = GetMinLengthFile(polyFiles);
        int maxLenghtFile = GetMaxLengthFile(polyFiles);

        FileInfo fileCurrent = new FileInfo(polyFiles[currentFile].FileName);
        FileInfo fileMin = new FileInfo(polyFiles[minLengthFile].FileName);
        FileInfo fileMax = new FileInfo(polyFiles[maxLenghtFile].FileName);

        BinaryReader binaryReaderMin = new BinaryReader(fileMin.OpenRead());
        for (int i = 0; i < iteration; i++)
        {
            Console.WriteLine("=====  " + i + "  =======");
            // testIteration(currentFile, minLengthFile, maxLenghtFile, fileCurrent, fileMin, fileMax, polyFiles);
            using (BinaryWriter binaryWriter = new BinaryWriter(fileCurrent.OpenWrite()))
            {
                BinaryReader binaryReaderMax = new BinaryReader(fileMax.OpenRead());

                for (int j = 0; j < polyFiles[minLengthFile].Length; j++)
                {
                    
                    int seriesMin = 0, seriesMax = 0;
                    int curMin = int.MaxValue, curMax = int.MaxValue;
                    // Console.WriteLine("Size - " + (polyFiles[minLengthFile].SeriesSize + polyFiles[maxLenghtFile].SeriesSize));
                    for (int k = 0; k < polyFiles[minLengthFile].SeriesSize + polyFiles[maxLenghtFile].SeriesSize; k++)
                    {
                        if (curMin == int.MaxValue && seriesMin != polyFiles[minLengthFile].SeriesSize)
                        {
                            // Console.WriteLine("S Min - " + seriesMin);
                            curMin = binaryReaderMin.ReadInt32();
                            seriesMin += 1;
                        }
                        if (curMax == int.MaxValue && seriesMax != polyFiles[maxLenghtFile].SeriesSize)
                        {
                            // Console.WriteLine("S Max - " + seriesMax);
                            curMax = binaryReaderMax.ReadInt32();
                            seriesMax++;
                        }

                        // Console.WriteLine("Min = " + curMin + " Max = " + curMax);

                        if (curMin < curMax)
                        {
                            // Console.WriteLine("Write min");
                            binaryWriter.Write(curMin);
                            curMin = int.MaxValue;
                        }
                        else
                        {
                            // Console.WriteLine("Write max");
                            binaryWriter.Write(curMax);
                            curMax = int.MaxValue;
                        }
                    }
                }

                binaryReaderMin.Close();
                fileMin.Delete();
                fileMin = new FileInfo(polyFiles[minLengthFile].FileName);

                polyFiles[currentFile].Length = polyFiles[minLengthFile].Length;
                polyFiles[currentFile].SeriesSize =
                    polyFiles[minLengthFile].SeriesSize + polyFiles[maxLenghtFile].SeriesSize;

                polyFiles[maxLenghtFile].Length -= polyFiles[minLengthFile].Length;
                polyFiles[minLengthFile].Length = 0;
                polyFiles[minLengthFile].SeriesSize = 0;


                int temp = minLengthFile;
                minLengthFile = maxLenghtFile;
                maxLenghtFile = currentFile;
                currentFile = temp;


                FileInfo tempFile = fileMin;
                fileMin = fileMax;
                fileMax = fileCurrent;
                fileCurrent = tempFile;

                binaryReaderMin = binaryReaderMax;
            }
        }

        binaryReaderMin.Close();
    }

    private static int GetMinLengthFile(PolyFile[] polyFiles)
    {
        long min = long.MaxValue;
        int minIndex = 0;
        for (int i = 0; i < polyFiles.Length; i++)
        {
            if (polyFiles[i].Length < min && polyFiles[i].Length != 0)
            {
                min = polyFiles[i].Length;
                minIndex = i;
            }
        }

        return minIndex;
    }

    private static int GetMaxLengthFile(PolyFile[] polyFiles)
    {
        long max = long.MinValue;
        int maxIndex = 0;
        for (int i = 0; i < polyFiles.Length; i++)
        {
            if (polyFiles[i].Length > max && polyFiles[i].Length != 0)
            {
                max = polyFiles[i].Length;
                maxIndex = i;
            }
        }

        return maxIndex;
    }

    private static void testIteration(int current, int min, int max, FileInfo fileCurrent, FileInfo fileMin,
        FileInfo fileMax, PolyFile[] polyFiles)
    {
        Console.WriteLine("Current:");
        Console.WriteLine("Current file - " + fileCurrent.FullName + " " + current);
        Console.WriteLine("Min file - " + fileMin.FullName + " " + min);
        Console.WriteLine("Max file - " + fileMax.FullName + " " + max);

        foreach (var file in polyFiles)
        {
            file.ShowInfo();
            // FileReader.showFile(file.FileName);
        }
    }
}