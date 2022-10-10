namespace Lab_Work_1;

public class PolyFile
{
    public string FileName { get; }
    public int SeriesSize { get; set; }

    public long Length { get; set; }

    public PolyFile(string path)
    {
        FileName = path;
        SeriesSize = 1;
    }

    public PolyFile(string path, int series)
    {
        FileName = path;
        Length = 0;
        SeriesSize = series;
    }

    public PolyFile(string path, long length, int series)
    {
        FileName = path;
        Length = length;
        SeriesSize = series;
    }

    public void ShowInfo()
    {
        Console.WriteLine("File name - " + FileName);
        Console.WriteLine("Length - " + Length);
        Console.WriteLine("Series Size - " + SeriesSize + "\n");
    }
}

public class PolyFileMod
{
    public int current = int.MaxValue;
    public string FileName { get; }
    public long SeriesSize { get; set; }
    public long Length { get; set; }

    public FileInfo FileInfo { get; set; }

    public BinaryReader BinaryReader { get; set; }

    public PolyFileMod(string path)
    {
        FileName = path;
        SeriesSize = 1;
        FileInfo = new FileInfo(path);
    }

    public PolyFileMod(string path, int series)
    {
        FileName = path;
        Length = 0;
        SeriesSize = series;

        FileInfo = new FileInfo(path);
    }

    public PolyFileMod(string path, long length, long series)
    {
        FileName = path;
        Length = length;
        SeriesSize = series;
        FileInfo = new FileInfo(path);
    }

    public void ShowInfo()
    {
        Console.WriteLine("File name - " + FileName);
        Console.WriteLine("Length - " + Length);
        Console.WriteLine("Current - " + current);
        Console.WriteLine("Series Size - " + SeriesSize + "\n");
    }

    public void ReCreateFile()
    {
        FileInfo.Delete();
        FileInfo = new FileInfo(FileName);
    }

    public void OpenReader()
    {
        BinaryReader = new BinaryReader(FileInfo.OpenRead());
        Next();
    }

    public void CloseReader()
    {
        BinaryReader.Close();
    }

    public void Next()
    {
        if (Length != 0)
        {
            Length--;
            current = BinaryReader.ReadInt32();
        }
        else
        {
            // Console.WriteLine("Error!!!");
            current = int.MaxValue;
        }
    }
}