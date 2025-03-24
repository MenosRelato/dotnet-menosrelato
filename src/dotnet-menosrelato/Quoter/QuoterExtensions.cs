using MenosRelato.Quoter;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace MenosRelato;

public static class QuoterExtensions
{
    public static IServiceCollection UseQuoter(this IServiceCollection services) => services;

    public static ICommandApp UseQuoter(this ICommandApp app)
    {
        app.Configure(config =>
        {
            config.AddCommand<QuoteCommand>("quote");
        });
        return app;
    }
}
