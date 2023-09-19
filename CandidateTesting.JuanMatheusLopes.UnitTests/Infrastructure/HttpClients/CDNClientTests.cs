using AutoFixture.Idioms;
using CandidateTesting.JuanMatheusLopes.Infrastructure.HttpClients;
using CandidateTesting.JuanMatheusLopes.UnitTests.AutoFixture.Attributes;
using FluentAssertions;

namespace CandidateTesting.JuanMatheusLopes.UnitTests.Infrastructure.HttpClients;

public class CDNClientTests
{
    [Theory, AutoNSubstituteData]
    public void CDNClient_Should_Guard_Its_Clauses(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(CDNClient).GetConstructors());
    }

    [Theory, AutoNSubstituteData]
    public void CDNClient_Should_Implement_ICDNClient(CDNClient sut)
    {
        sut.Should().BeAssignableTo<ICDNClient>();
    }
}
