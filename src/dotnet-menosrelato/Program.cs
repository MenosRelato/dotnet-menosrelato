using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using MenosRelato;

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;

#if DEBUG
if (args.Contains("--debug"))
{
    Debugger.Launch();
    args = args.Where(x => x != "--debug").ToArray();
}
#endif

if (args.Contains("-?"))
    args = [.. args.Select(x => x == "-?" ? "-h" : x)];

var app = App.Create();

if (args.Contains("--version"))
{
    app.ShowVersion();
#if DEBUG
    await app.ShowUpdatesAsync(args);
#endif
    return 0;
}

#if DEBUG
return await app.RunAsync(args);
#else
return await app.RunWithUpdatesAsync(args);
#endif
