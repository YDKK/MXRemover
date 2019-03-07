using System;
using System.Linq;
using System.IO;

namespace MXRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            var tsChannels = new[]
            {
                "MX", "CX", "TVS", "TBS", "TX", "NTV"
            };
            var bsChannels = new[]
            {
                "BS11", "BS-FUJI", "BS-TBS", "BS4", "BSテレ東"
            };

            var files = Directory.GetFiles(".").Where(x => tsChannels.Select(y => $" ({y}).mp4").Any(x.EndsWith));
            foreach (var file in files)
            {
                var tsChannel = tsChannels.First(x => file.EndsWith($" ({x}).mp4"));
                var searchFiles = bsChannels.Select(x => file.Replace($" ({tsChannel}).mp4", $" ({x}).mp4")).ToArray();
                if (searchFiles.Any(searchFile => File.Exists(searchFile) && new FileInfo(searchFile).Length > 100 * 1024 * 1024))
                {
                    Console.WriteLine(file);
                    File.Delete(file);
                }
            }
        }
    }
}
