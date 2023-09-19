namespace CandidateTesting.JuanMatheusLopes.Infrastructure.HttpClients;

public interface ICDNClient
{
    Task<HttpResponseMessage> GetAsync(string url);
}
