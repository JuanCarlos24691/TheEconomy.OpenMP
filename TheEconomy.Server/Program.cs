using System.IO;
using SampSharp.Core;
using SampSharp.Entities;

namespace TheEconomy.Server
{
    public class Program
    {
        public static void Main() =>
           new GameModeBuilder().UseEcs<Startup>().UseEncoding(Path.Combine($"{Directory.GetCurrentDirectory()}/../Encoding/", "8859-1.txt")).RedirectConsoleOutput().Run();
    }
}