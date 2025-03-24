using DotNetConfig;
using Spectre.Console;
using Spectre.Console.Cli;

namespace MenosRelato.Quoter;

public class QuoteCommand : AsyncCommand<QuoteCommandSettings>
{
    public override Task<int> ExecuteAsync(CommandContext context, QuoteCommandSettings settings)
    {
        var config = Config.Build(settings.Directory);

        AnsiConsole.Status().Start($"Processing quotes from {settings.Directory}", ctx =>
        {
            foreach (var file in Directory.EnumerateFiles(settings.Directory, "*.txt"))
            {
                var subsection = Path.GetFileName(file);
                ctx.Status($"Processing quotes in {subsection}");

                var section = config.GetSection("quoter", subsection);
                var table = new Table();
                table.AddColumn("Key");
                table.AddColumn("Value");
                foreach (var item in config.AsEnumerable().Where(x => x.Section == "quoter" && x.Subsection == subsection))
                    table.AddRow(item.Variable, item.RawValue ?? "");

                AnsiConsole.Write(table);
            }
        });

        return Task.FromResult(0);
    }
}

public class QuoteCommandSettings : CommandSettings
{
    [CommandOption("-d|--directory")]
    public required string Directory { get; set; }
}
