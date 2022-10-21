using System.Diagnostics;
using System.Threading.Channels;

namespace Lab_Work_1;

public class PoluphaseMargeSortMod
{
    public static void Start(PolyFileMod[] polyFiles, int iteration)
    {
        int current = polyFiles.Length - 1;
        int minLenghtFile = 0;
        long newLength = 0;
        OpenReaders(polyFiles);
       
        for (int i = 0; i < iteration; i++)
        {
            minLenghtFile = GetMinLengthFile(polyFiles);
            newLength = (polyFiles[minLenghtFile].Length + 1) / polyFiles[minLenghtFile].SeriesSize;

            using (BinaryWriter binaryWriter = new BinaryWriter(polyFiles[current].FileInfo.OpenWrite()))
            {
                long sumSeries = getSeriesSum(polyFiles);
                int[] fileReadCoordinates = new int[polyFiles.Length - 1];
                int index = 0;
                for (int k = 0; k < polyFiles.Length; k++)
                {
                    if (k != current)
                    {
                        fileReadCoordinates[index] = k;
                        index++;
                    }
                }
                for (int j = 0; j < newLength; j++)
                {
                    long[] fileSeriesSize = new long[polyFiles.Length - 1];
                    for (int k = 0; k < polyFiles.Length - 1; k++)
                    {
                        fileSeriesSize[k] = polyFiles[fileReadCoordinates[k]].SeriesSize;
                    }
                    
                    for (int k = 0; k < sumSeries; k++)
                    {
                        int min = getMin(polyFiles, fileReadCoordinates, fileSeriesSize);
                        binaryWriter.Write(min);
                    }
                }

                polyFiles[current].Length = newLength * sumSeries;
                polyFiles[current].SeriesSize = sumSeries;
            }

            polyFiles[minLenghtFile].SeriesSize = 0;
            polyFiles[minLenghtFile].CloseReader();
            polyFiles[minLenghtFile].ReCreateFile();
            polyFiles[current].OpenReader();
            current = minLenghtFile;
        }
    }

    private static void OpenReaders(PolyFileMod[] polyFileMods)
    {
        for (int i = 0; i < polyFileMods.Length - 1; i++)
        {
            polyFileMods[i].OpenReader();
        }
    }

    private static int GetMinLengthFile(PolyFileMod[] polyFiles)
    {
        long min = int.MaxValue;
        int minIndex = 0;
        for (int i = 0; i < polyFiles.Length; i++)
        {
            if (polyFiles[i].SeriesSize != 0)
            {
                if ((polyFiles[i].Length + 1) / polyFiles[i].SeriesSize < min && polyFiles[i].SeriesSize != 0)
                {
                    min = (polyFiles[i].Length + 1) / polyFiles[i].SeriesSize;
                    minIndex = i;
                }
            }
        }

        return minIndex;
    }

    private static long getSeriesSum(PolyFileMod[] polyFileMods)
    {
        long sum = 0;
        for (int i = 0; i < polyFileMods.Length; i++)
        {
            sum += polyFileMods[i].SeriesSize;
        }

        return sum;
    }

    private static int getMin(PolyFileMod[] polyFileMods, int[] coordinates, long[] seriesSize)
    {
        int min = int.MaxValue;
        int minIndex = 0;
        for (int i = 0; i < coordinates.Length; i++)
        {
            if (min >= polyFileMods[coordinates[i]].current && seriesSize[i] != 0)
            {
                min = polyFileMods[coordinates[i]].current;
                minIndex = i;
            }
        }
        seriesSize[minIndex]--;
        polyFileMods[coordinates[minIndex]].Next();
        return min;
    }
}