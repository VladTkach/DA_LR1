using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Channels;
using Microsoft.VisualBasic;

namespace Lab_Work_1;

public class Fibonacci
{
    public static int[] getFileSize3(double num, int order, ref int iteration, ref long extra)
    {
        int[] fileSize = new int[order];
        iteration = 1;
        fileSize[0] = 0;
        for (int i = 1; i < order; i++)
        {
            fileSize[i] = 1;
        }

        int sum = findSum(fileSize);
        int indexZero = 0;
        while (sum < num)
        {
            int maxIndex = findMaxIndex(fileSize);
            int max = fileSize[maxIndex];

            for (int i = 0; i < fileSize.Length; i++)
            {
                fileSize[i] += max;
            }

            fileSize[indexZero] = max;
            fileSize[maxIndex] = 0;
            indexZero = maxIndex;

            sum = findSum(fileSize);
            iteration++;
        }
        
        Array.Sort(fileSize);
        extra = sum;

        int[] newFileSize = new int[order - 1];
        for (int i = 0; i < newFileSize.Length; i++)
        {
            newFileSize[i] = fileSize[i + 1];
        }
        return newFileSize;
    }

    private static int findMaxIndex(int[] fileSize)
    {
        int max = int.MinValue;
        int maxIndex = 0;
        for (int i = 0; i < fileSize.Length; i++)
        {
            if (fileSize[i] >= max)
            {
                max = fileSize[i];
                maxIndex = i;
            }
        }
        return maxIndex;
    }
    
    private static int findSum(int[] fileSize)
    {
        int sum = 0;
        for (int i = 0; i < fileSize.Length; i++)
        {
            sum += fileSize[i];
        }
        return sum;
    }
}