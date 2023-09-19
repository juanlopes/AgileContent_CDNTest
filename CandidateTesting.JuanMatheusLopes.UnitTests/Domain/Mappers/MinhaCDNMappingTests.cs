using CandidateTesting.JuanMatheusLopes.Domain.Arguments;
using CandidateTesting.JuanMatheusLopes.Domain.Mappers;
using CandidateTesting.JuanMatheusLopes.UnitTests.AutoFixture.Attributes;
using FluentAssertions;
using System.Text;

namespace CandidateTesting.JuanMatheusLopes.UnitTests.Domain.Mappers;

public class MinhaCDNMappingTests
{
    [Theory, AutoNSubstituteData]
    public void AgoraCDNMapping_Should_Implement_IAgoraCDNMapping(MinhaCDNMapping sut)
    {
        sut.Should().BeAssignableTo<IMinhaCDNMapping>();
    }

    [Fact]
    public void MapFromMinhaCDN_Should_Map_To_AgoraCDN_Correctly()
    {
        var source = SetupContentStream("312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2");
        var expected = SetupMinhaCDNEntries();

        var sut = new MinhaCDNMapping();

        var actual = sut.MapEntriesFromStream(source);

        actual.Should().BeEquivalentTo(expected);
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

    private static Stream SetupContentStream(string content)
    {
        var contentBytes = Encoding.UTF8.GetBytes(content);

        var stream = new MemoryStream(contentBytes);

        return stream;
    }
}
