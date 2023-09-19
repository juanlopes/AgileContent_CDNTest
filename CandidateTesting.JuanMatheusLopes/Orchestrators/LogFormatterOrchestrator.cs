using CandidateTesting.JuanMatheusLopes.Domain.Factories;
using CandidateTesting.JuanMatheusLopes.Infrastructure.Factories;
using CandidateTesting.JuanMatheusLopes.Infrastructure.HttpClients;

namespace CandidateTesting.JuanMatheusLopes.Application.Orchestrators;

public class LogFormatterOrchestrator : ILogFormatterOrchestrator
{
    private readonly ICDNClient _cdnClient;
    private readonly ILogFactory _logFactory;
    private readonly IFileFactory _fileFactory;

    public LogFormatterOrchestrator(
        ICDNClient cdnClient,
        ILogFactory logFactory,
        IFileFactory fileFactory)
    {
        _cdnClient =
            cdnClient
            ?? throw new ArgumentNullException(nameof(cdnClient));

        _logFactory =
            logFactory
            ?? throw new ArgumentNullException(nameof(logFactory));

        _fileFactory =
            fileFactory
            ?? throw new ArgumentNullException(nameof(fileFactory));
    }

    public async Task<string> StartAsync(string sourceUrl, string destinationPath)
    {
        Console.WriteLine($"[{DateTime.Now}] - Querying url: {sourceUrl}");
        var cdnContent = await _cdnClient.GetAsync(sourceUrl);

        var logContent = cdnContent.Content.ReadAsStream();

        if (cdnContent.IsSuccessStatusCode)
        {
            var fileContent = _logFactory.Create(logContent);

            var fileCreated = _fileFactory.Create(destinationPath, fileContent);

            return fileCreated;
        }

        Console.WriteLine($"[{DateTime.Now}] - Was not possible query the following url: {sourceUrl}");
        return "";
    }
}
