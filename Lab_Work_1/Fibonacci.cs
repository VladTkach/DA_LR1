using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Channels;
using Microsoft.VisualBasic;

namespace Lab_Work_1;

public class Fibonacci
{
    public static void GetFileSize(ref int first, ref int second, ref int iteration, int num)
    {
        int third = 1;
        while (third <= num)
        {
            third = first + second;
            first = second;
            second = third;
            iteration++;
        }

        int temp = first;
        first = second - first;
        second = temp;

        iteration--;
    }

    public static int[] getFileSize3(long num, int order, ref int iteration, ref long extra)
    {
        int[] fileSize = new int[order];
        iteration = 1;
        fileSize[0] = 0;
        for (int i = 1; i < order; i++)
        {
            fileSize[i] = 1;
        }

        int sum = 0;
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
        extra = sum - num;

        int[] newFileSize = new int[order - 1];
        for (int i = 0; i < newFileSize.Length; i++)
        {
            newFileSize[i] = fileSize[i + 1];
        }

        // foreach (var VARIABLE in newFileSize)
        // {
        //     Console.Write(VARIABLE + " ");
        // }
        
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
    
    public static int[] getFileSize(int num, int order, ref int iteration, ref int extra)
    {
        int[] fib = new int[order + 1] ;

        for (int i = 0; i < order; i++)
        {
            fib[i] = 0;
        }
        fib[order] = 1;
        
        while (fib[order] < num)
        {
            int sum = 0;

            for (int i = 0; i < order + 1; i++)
            {
                sum += fib[i];
            }
            for (int i = 0; i < order; i++)
            {
                fib[i] = fib[i + 1];
            }

            fib[order] = sum;
            iteration++;
        }

        extra = fib[order] - num;
        // Step Beak
        int sum2 = 0;
        for (int i = 0; i < order; i++)
        {
            sum2 += fib[i];
        }

        int last = fib[order] - sum2;

        for (int i = order ; i > 0; i--)
        {
            fib[i] = fib[i - 1];
        }

        fib[0] = last;

        foreach (var VARIABLE in fib)
        {
            Console.Write(VARIABLE + " ");
        }

        Console.WriteLine("\n");
        iteration--;
        return fib;
    }
}