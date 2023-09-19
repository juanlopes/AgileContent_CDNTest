using AutoFixture.Idioms;
using CandidateTesting.JuanMatheusLopes.Domain.Arguments;
using CandidateTesting.JuanMatheusLopes.Domain.Entities;
using CandidateTesting.JuanMatheusLopes.Domain.Factories;
using CandidateTesting.JuanMatheusLopes.Domain.Mappers;
using CandidateTesting.JuanMatheusLopes.UnitTests.AutoFixture.Attributes;
using FluentAssertions;
using NSubstitute;
using System.Text;

namespace CandidateTesting.JuanMatheusLopes.UnitTests.Domain.Factories;

public class LogFactoryTests
{
    [Theory, AutoNSubstituteData]
    public void LogFactory_Should_Guard_Its_Clauses(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(LogFactory).GetConstructors());
    }

    [Theory, AutoNSubstituteData]
    public void LogFactory_Should_Implement_ILogFactory(LogFactory sut)
    {
        sut.Should().BeAssignableTo<ILogFactory>();
    }

    [Theory, AutoNSubstituteData]
    public void Create_Should_Return_Log_Formatted_As_Agora_Format(
        IMinhaCDNMapping minhaCDNMapping,
        IAgoraCDNMapping agoraCDNMapping)
    {
        var stream = SetupContentStream("312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2");
        var minhaCDNEntries = SetupMinhaCDNEntries();
        var agoraEntries = SetupAgoraEntries();

        minhaCDNMapping.MapEntriesFromStream(stream).Returns(minhaCDNEntries);
        agoraCDNMapping.MapFromMinhaCDN(minhaCDNEntries).Returns(agoraEntries);

        var expected = "\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT";

        var sut = new LogFactory(minhaCDNMapping, agoraCDNMapping);

        var result = sut.Create(stream);

        var actual = result.Split("\r\n")[3];

        actual.Should().Be(expected);
    }

    private static Stream SetupContentStream(string content)
    {
        var contentBytes = Encoding.UTF8.GetBytes(content);

        var stream = new MemoryStream(contentBytes);

        return stream;
    }

    private static MinhaCDNEntries SetupMinhaCDNEntries()
    {
        return new MinhaCDNEntries
        {
            Entries = new List<MinhaCDNEntry> {
                new MinhaCDNEntry {
                    ResponseSize = 312,
                    StatusCode = 200,
                    CacheStatus = "HIT",
                    RequestedPath = "\"GET /robots.txt HTTP/1.1\"",
                    TimeTaken = 100.2m
                }
            }
        };
    }

    private static AgoraEntries SetupAgoraEntries()
    {
        return new AgoraEntries
        {
            Date = DateTime.Now,
            Version = 1.0m,
            Entries = new List<AgoraEntry>
            {
                new AgoraEntry
                {
                    ResponseSize = 312,
                    StatusCode = 200,
                    CacheStatus = "HIT",
                    Provider = "MINHA CDN",
                    HttpMethod = "GET",
                    TimeTaken = 100,
                    UriPath = "/robots.txt"
                }
            }
        };
    }
}
