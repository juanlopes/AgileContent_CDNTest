using CandidateTesting.JuanMatheusLopes.Domain.Arguments;
using CandidateTesting.JuanMatheusLopes.Domain.Entities;

namespace CandidateTesting.JuanMatheusLopes.Domain.Mappers;

public class AgoraCDNMapping : IAgoraCDNMapping
{
    public AgoraEntries MapFromMinhaCDN(MinhaCDNEntries minhaCDNEntries)
    {
        var agoraEntries = new AgoraEntries
        {
            Version = 1.0m,
            Date = DateTime.Now
        };

        foreach (var minhaCDNEntry in minhaCDNEntries.Entries)
        {
            var agoraEntry = new AgoraEntry
            {
                CacheStatus = GetCacheStatus(minhaCDNEntry.CacheStatus),
                Provider = "MINHA CDN",
                ResponseSize = minhaCDNEntry.ResponseSize,
                StatusCode = minhaCDNEntry.StatusCode,
                TimeTaken = GetTimeTaken(minhaCDNEntry.TimeTaken),
                HttpMethod = GetHttpMethod(minhaCDNEntry.RequestedPath),
                UriPath = GetURIPath(minhaCDNEntry.RequestedPath)
            };

            agoraEntries.Entries.Add(agoraEntry);
        }

        return agoraEntries;
    }

    private static string GetHttpMethod(string requestedPath)
    {
        var httpMethod = requestedPath.Replace("\"", "");
        var splittedPath = httpMethod.Split(' ');

        httpMethod = splittedPath[0];

        return httpMethod;
    }

    private static string GetURIPath(string requestedPath)
    {
        var httpMethod = requestedPath.Replace("\"", "");
        var splittedPath = httpMethod.Split(' ');

        httpMethod = splittedPath[1];

        return httpMethod;
    }

    private static string GetCacheStatus(string cacheStatus)
    {
        switch (cacheStatus)
        {
            case "INVALIDATE": return "REFRESH_HIT";
            default: return cacheStatus;
        }
    }

    private static int GetTimeTaken(decimal timeTaken)
    {
        return (int)Math.Round(timeTaken);
    }
}
