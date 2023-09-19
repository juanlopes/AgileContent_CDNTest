using CandidateTesting.JuanMatheusLopes.Domain.Arguments;
using CandidateTesting.JuanMatheusLopes.Domain.Entities;
using CandidateTesting.JuanMatheusLopes.Domain.Mappers;
using CandidateTesting.JuanMatheusLopes.UnitTests.AutoFixture.Attributes;
using FluentAssertions;

namespace CandidateTesting.JuanMatheusLopes.UnitTests.Domain.Mappers;

public class AgoraCDNMappingTests
{
    [Theory, AutoNSubstituteData]
    public void AgoraCDNMapping_Should_Implement_IAgoraCDNMapping(AgoraCDNMapping sut)
    {
        sut.Should().BeAssignableTo<IAgoraCDNMapping>();
    }

    [Fact]
    public void MapFromMinhaCDN_Should_Map_To_AgoraCDN_Correctly()
    {
        var source = SetupMinhaCDNEntries();

        var expected = SetupAgoraEntries();

        var sut = new AgoraCDNMapping();

        var actual = sut.MapFromMinhaCDN(source);

        actual.Should().BeEquivalentTo(expected, options => options.Excluding(s => s.Date));
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
                    RequestedPath = "GET /robots.txt HTTP/1.1",
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
