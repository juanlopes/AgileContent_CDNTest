using CandidateTesting.JuanMatheusLopes.Application.Extensions;
using CandidateTesting.JuanMatheusLopes.Application.Orchestrators;
using CandidateTesting.JuanMatheusLopes.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateTesting.JuanMatheusLopes;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"[{DateTime.Now}] - Configuring Services...");
        var services = new ServiceCollection();

        services.ConfigureServices();

        var serviceProvider = services.BuildServiceProvider();

        var logFormatterOrchestrator =
            serviceProvider.GetRequiredService<ILogFormatterOrchestrator>();

        var urlValidator =
            serviceProvider.GetRequiredService<IUrlValidator>();

        if (args.Length == 2)
        {
            var sourceUrl = GetSourceUrl(urlValidator, args);
            var destinationPath = GetDestinationPath(args);

            if (AllowProceedWithProvidedData(sourceUrl, destinationPath))
            {
                Console.WriteLine(
                    $"[{DateTime.Now}] - Starting conversion process with url \"{sourceUrl}\" to be saved in following path \"{destinationPath}\" ");

                StartFormatting(logFormatterOrchestrator, sourceUrl, destinationPath).Wait();

                Console.WriteLine($"[{DateTime.Now}] - Finished, file generated in '{destinationPath}' ");
            }
        }
        else
        {
            Console.WriteLine($"[{DateTime.Now}] - Please, provide only two parameters: 1 - SourceUrl, 2 - DestinationPath");
            return;
        }
    }

    private static async Task StartFormatting(
        ILogFormatterOrchestrator logFormatterOrchestrator,
        string sourceUrl,
        string destinationPath)
    {
        Console.WriteLine($"[{DateTime.Now}] - Invoking Orchestrator...");
        await logFormatterOrchestrator.StartAsync(sourceUrl, destinationPath);
    }

    private static string GetSourceUrl(IUrlValidator urlValidator, string[] args)
    {
        var sourceUrl = args[0];

        if (urlValidator.IsValid(sourceUrl))
        {
            return sourceUrl;
        }

        Console.WriteLine($"[{DateTime.Now}] - ERROR: Please, provide a valid source url");
        return "";
    }

    private static string GetDestinationPath(string[] args)
    {
        var destinationPath = args[1];

        var path = Path.Combine(Directory.GetCurrentDirectory(), destinationPath.Replace("./", ""));

        return path;
    }

    private static bool AllowProceedWithProvidedData(string url, string destination)
    {
        return !(string.IsNullOrEmpty(url) || string.IsNullOrEmpty(destination));
    }
}