using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace _1brc;

public class Program
{
    public static void Main(string[] args)
    {
        var path = args.Length > 0 ? args[0] : ".\\measurements.txt";

        if (args.Contains("--results"))
        {
            new ResultsParser().ExtractToCsv(path);
            return;
        }

        Console.OutputEncoding = Encoding.UTF8;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || args.Contains("--worker"))
            DoWork(path);
        else
            StartSubprocess(path);
    }

    public static string Run(string path)
    {
        return DoWork(path);
    }

    private static string DoWork(string path)
    {
        var start = Stopwatch.GetTimestamp();
        using (var app = new App(path))
        {
            var strResult = app.PrintResult();
            Console.WriteLine($"Total seconds: {Stopwatch.GetElapsedTime(start).TotalSeconds}");
            //Console.Out.Close();
            return strResult;
        }
    }

    private static void StartSubprocess(string path)
    {
        string parentProcessPath = Process.GetCurrentProcess().MainModule!.FileName;

        Console.WriteLine($"CMD: -c \"{parentProcessPath} {path} --worker & \" ");

        var processStartInfo = new ProcessStartInfo
        {
            FileName = "sh", // parentProcessPath,
            Arguments = $"-c \"{parentProcessPath} {path} --worker & \" ",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = false,
        };

        var process = Process.Start(processStartInfo);
        string? output = process!.StandardOutput.ReadLine();
        Console.Write(output);
    }
}