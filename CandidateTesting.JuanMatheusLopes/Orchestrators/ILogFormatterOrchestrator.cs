namespace CandidateTesting.JuanMatheusLopes.Application.Orchestrators;

public interface ILogFormatterOrchestrator
{
    Task<string> StartAsync(string sourceUrl, string destinationPath);
}
