namespace CandidateTesting.JuanMatheusLopes.Infrastructure.HttpClients;

public class CDNClient : ICDNClient
{
    private readonly HttpClient _httpClient;

    public CDNClient(HttpClient httpClient)
    {
        _httpClient =
            httpClient
            ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public Task<HttpResponseMessage> GetAsync(string url)
    {
        var response = _httpClient.GetAsync(url);

        return response;
    }
}
