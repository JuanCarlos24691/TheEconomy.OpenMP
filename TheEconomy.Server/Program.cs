using SampSharp.Core;
using SampSharp.Entities;
using System.Text;

namespace TheEconomy.Server;

public class Program
{
    public static void Main()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        new GameModeBuilder()
            .UseEcs<Startup>()
            .UseEncoding(Encoding.GetEncoding("iso-8859-1"))
            .RedirectConsoleOutput()
            .Run();
    }
}