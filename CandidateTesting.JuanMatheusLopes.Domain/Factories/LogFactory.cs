using CandidateTesting.JuanMatheusLopes.Domain.Entities;
using CandidateTesting.JuanMatheusLopes.Domain.Mappers;
using System.Globalization;
using System.Text;

namespace CandidateTesting.JuanMatheusLopes.Domain.Factories;

public class LogFactory : ILogFactory
{
    private readonly IMinhaCDNMapping _minhaCDNMapping;
    private readonly IAgoraCDNMapping _agoraCDNMapping;

    public LogFactory(
        IMinhaCDNMapping minhaCDNMapping,
        IAgoraCDNMapping agoraCDNMapping)
    {
        _minhaCDNMapping =
            minhaCDNMapping
            ?? throw new ArgumentNullException(nameof(minhaCDNMapping));

        _agoraCDNMapping =
            agoraCDNMapping
            ?? throw new ArgumentNullException(nameof(agoraCDNMapping));
    }

    public string Create(Stream logContent)
    {
        var minhaCDNContent = _minhaCDNMapping.MapEntriesFromStream(logContent);
        Console.WriteLine($"[{DateTime.Now}] - Mapped Minha CDN Successfully");

        var agoraEntries = _agoraCDNMapping.MapFromMinhaCDN(minhaCDNContent);
        Console.WriteLine($"[{DateTime.Now}] - Mapped To Agora Successfully");

        var fileContent = CreateFileContent(agoraEntries);
        Console.WriteLine($"[{DateTime.Now}] - Conversion finished.");
        Console.WriteLine(
            $"[{DateTime.Now}] - Conversion Data: \r\n" +
            $"--------------------------------------------------- \r\n " +
            $"{fileContent} \r\n" +
            $"---------------------------------------------------");

        return fileContent;
    }

    private static string CreateFileContent(AgoraEntries agoraEntries)
    {
        var contentBuilder = new StringBuilder();

        contentBuilder
            .AppendLine($"#Version: {agoraEntries.Version.ToString(new CultureInfo("en-US"))}");
        contentBuilder.AppendLine($"#Date: {agoraEntries.Date}");
        contentBuilder.AppendLine(
            "#Fields: " +
                "provider " +
                "http-method " +
                "status-code " +
                "uri-path " +
                "time-taken " +
                "response-size " +
                "cache-status");

        foreach (var entry in agoraEntries.Entries)
        {
            var line =
                $"\"{entry.Provider}\" " +
                $"{entry.HttpMethod} " +
                $"{entry.StatusCode} " +
                $"{entry.UriPath} " +
                $"{entry.TimeTaken} " +
                $"{entry.ResponseSize} " +
                $"{entry.CacheStatus}";

            contentBuilder.AppendLine(line);
        }

        return contentBuilder.ToString();
    }
}
