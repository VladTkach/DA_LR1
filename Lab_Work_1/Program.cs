using System.Diagnostics;
using System.Threading.Channels;

namespace Lab_Work_1
{
    class Program
    {
        private static void Main(string[] args)
        {
            string size = "10MB";
            long number = FileWriter.getSize(size);
            int numFile = 4;
            string mainFile = "Main.bin";
            int iteration = 0;
            long extra = 0;
            var watch = new Stopwatch();

            string divider = "128MB";
            long idivider = FileWriter.getSize(divider);

            watch.Start();
            FileWriter.CreateFileBin(mainFile, number);
            watch.Stop();
            Console.WriteLine("Create main File: " + watch.ElapsedMilliseconds + " ms\n");

            watch.Restart();
            watch.Start();
            
             // // ==============Modidication=================
            // PolyFileMod[] polyFileMods = PreparetionSorting.getFileListMod(mainFile, number, idivider, numFile, ref iteration, ref extra);
            //
            // PreparetionSorting.CreateStartFilesMode(polyFileMods, mainFile, idivider);
            // // ==============Modidication=================

            PolyFileMod[] polyFiles =
                PreparetionSorting.getListFile2(mainFile, numFile, number, ref iteration, ref extra);
            
            PreparetionSorting.CreateStartFiles(polyFiles, mainFile);

            watch.Stop();
            Console.WriteLine("Preparation: " + watch.ElapsedMilliseconds + " ms\n");
            

            watch.Restart();
            watch.Start();
            PoluphaseMargeSortMod.Start(polyFiles, iteration);
            watch.Stop();
            Console.WriteLine("Sorting: " + watch.ElapsedMilliseconds + " ms");
            
            Console.WriteLine("=====Test=====");
            string FileRes = string.Empty;
            foreach (var file in polyFiles)
            {
                if (file.Length > 1)
                {
                    FileRes = file.FileName;
                }
            }
            FileReader.TestFile(FileRes, extra);
        }
    }
}