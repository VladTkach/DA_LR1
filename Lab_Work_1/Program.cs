using System.Diagnostics;
using System.Threading.Channels;

namespace Lab_Work_1
{
    class Program
    {
        private static void Main(string[] args)
        {
            // // ==============Modidication=================
            string size = "100MB";
            long number = FileWriter.getSize(size);
            // Console.WriteLine("Size = " + number);
            int numFile = 4;
            string mainFile = "testBin.bin";
            int iteration = 0;
            long extra = 0;
            var watch = new Stopwatch();
            
            string divider = "10MB";
            long idivider = FileWriter.getSize(divider);
            // Console.WriteLine("Size = " + number / idivider);
            
            watch.Start();
            FileWriter.CreateFileBin(mainFile, number);
            watch.Stop();
            Console.WriteLine("Create main File: " + watch.ElapsedMilliseconds + " ms\n");
            
            watch.Restart();
            watch.Start();
            PolyFileMod[] polyFileMods = PreparetionSorting.getFileListMod(mainFile, number, idivider, numFile, ref iteration, ref extra);
            
            PreparetionSorting.CreateStartFilesMode(polyFileMods, mainFile, idivider);
            watch.Stop();
            Console.WriteLine("Preparation: " + watch.ElapsedMilliseconds + " ms\n");
            
            // Console.WriteLine(iteration);
            
            watch.Restart();
            watch.Start();
            PoluphaseMargeSortMod.Start(polyFileMods, iteration);
            watch.Stop();
            Console.WriteLine("Sorting: " + watch.ElapsedMilliseconds + " ms");
            Console.WriteLine("=====Test=====");
            
            
            string FileRes = string.Empty;
            foreach (var file in polyFileMods)
            {
                if (file.Length > 1)
                {
                    FileRes = file.FileName;
                }
            }
            FileReader.TestFile(FileRes, extra);
            // // ==============Modidication=================
            

            // ==============Can change file num=================

            // watch.Start();
            // FileWriter.CreateFileBin(mainFile, number);
            // watch.Stop();
            // Console.WriteLine("Create main File: " + watch.ElapsedMilliseconds + " ms\n");
            // watch.Restart();
            // watch.Start();
            // PolyFileMod[] polyFiles =
            //     PreparetionSorting.getListFile2(mainFile, numFile, number, ref iteration, ref extra);
            //
            // // FileReader.showFile(mainFile);
            //
            // PreparetionSorting.CreateStartFiles(polyFiles, mainFile);
            // watch.Stop();
            // Console.WriteLine("Preparation: " + watch.ElapsedMilliseconds + " ms\n");
            //
            //
            // // foreach (var VARIABLE in polyFiles)
            // // {
            // //     VARIABLE.ShowInfo();
            // //     // FileReader.showFile(VARIABLE.FileName);
            // // }
            //
            // // Console.WriteLine("Iteration = " + iteration);
            // watch.Restart();
            // watch.Start();
            // PoluphaseMargeSortMod.Start(polyFiles, iteration);
            // watch.Stop();
            // Console.WriteLine("Sorting: " + watch.ElapsedMilliseconds + " ms\n");
            //
            // // foreach (var file in polyFiles)
            // // {
            // //     file.ShowInfo();
            // //     // FileReader.showFile(file.FileName);
            // // }
            // Console.WriteLine("=====Test=====");
            //
            //
            // string FileRes = string.Empty;
            // foreach (var file in polyFiles)
            // {
            //     if (file.Length > 1)
            //     {
            //         FileRes = file.FileName;
            //     }
            // }
            //
            // // FileReader.TestFile(mainFile, extra);
            // FileReader.TestFile(FileRes, extra);

            // ==============Can cahnge file num=================

            // Fibonacci.getFileSize(number, numFile - 2);

            // watch.Start();
            // FileWriter.CreateFileBin(mainFile, number);
            // watch.Stop();
            // Console.WriteLine("Create main File: " + watch.ElapsedMilliseconds + " ms");
            //
            // watch.Reset();
            // watch.Start();
            // PolyFile[] polyFiles = PreparetionSorting.getListFile(mainFile, numFile, number, ref iteration, ref extra);
            // watch.Stop();
            // Console.WriteLine("Preparation(Get File List): " + watch.ElapsedMilliseconds + " ms");
            // // FileReader.showFile(mainFile);
            //
            // Console.WriteLine("Ineration - " + iteration);
            //
            // watch.Reset();
            // watch.Start();
            // PreparetionSorting.CreateStartFiles(polyFiles, mainFile);
            // watch.Stop();
            // Console.WriteLine("Preparation(Create File): "  + watch.ElapsedMilliseconds + " ms");
            //
            // watch.Reset();
            // watch.Start();
            // PolyphaseMargeSort.Start(polyFiles, iteration);
            // watch.Stop();
            // Console.WriteLine("Sorting: "  + watch.ElapsedMilliseconds + " ms");
            //
            // string Fileres = string.Empty;
            // foreach (var file in polyFiles)
            // {
            //     if (file.Length == 1)
            //     {
            //         Fileres = file.FileName;
            //         // FileReader.showFile(file.FileName);
            //     }
            // }
            //
            // FileReader.TestFile(mainFile, extra);
            // FileReader.TestFile(Fileres, extra);
            //
            //
        }
    }
}