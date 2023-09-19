using AutoFixture.Idioms;
using CandidateTesting.JuanMatheusLopes.Application.Orchestrators;
using CandidateTesting.JuanMatheusLopes.Domain.Factories;
using CandidateTesting.JuanMatheusLopes.Infrastructure.Factories;
using CandidateTesting.JuanMatheusLopes.Infrastructure.HttpClients;
using CandidateTesting.JuanMatheusLopes.UnitTests.AutoFixture.Attributes;
using NSubstitute;

namespace CandidateTesting.JuanMatheusLopes.UnitTests.Application.Orchestrators;

public class LogFormatterOrchestratorTests
{
    [Theory, AutoNSubstituteData]
    public void LogFormatterOrchestrator_Should_Guard_Its_Clauses(GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(LogFormatterOrchestrator).GetConstructors());
    }

    [Theory, AutoNSubstituteData]
    public void LogFormatterOrchestrator_Should_Implement_ILogFormatterOrchestrator(
        LogFormatterOrchestrator sut)
    {
        Assert.IsAssignableFrom<ILogFormatterOrchestrator>(sut);
    }

    [Theory, AutoNSubstituteData]
    public async Task StartAsync_Should_Invoke_Factories_If_cdnContent_Is_Successfull(
        ICDNClient cdnClient,
        ILogFactory logFactory,
        IFileFactory fileFactory,
        HttpResponseMessage responseMessage,
        string sourceUrl,
        string destinationPath,
        string logFactoryResult)
    {
        responseMessage.StatusCode = System.Net.HttpStatusCode.OK;

        cdnClient.GetAsync(sourceUrl).Returns(responseMessage);

        logFactory.Create(responseMessage.Content.ReadAsStream()).Returns(logFactoryResult);

        var sut = new LogFormatterOrchestrator(cdnClient, logFactory, fileFactory);

        await sut.StartAsync(sourceUrl, destinationPath);

        fileFactory.Received(1).Create(destinationPath, logFactoryResult);
    }

    [Theory, AutoNSubstituteData]
    public async Task StartAsync_Should_Not_Invoke_Factories_When_cdnContent_Is_Not_Successfull(
        ICDNClient cdnClient,
        ILogFactory logFactory,
        IFileFactory fileFactory,
        HttpResponseMessage responseMessage,
        string sourceUrl,
        string destinationPath)
    {
        responseMessage.StatusCode = System.Net.HttpStatusCode.NotFound;

        cdnClient.GetAsync(sourceUrl).Returns(responseMessage);

        var sut = new LogFormatterOrchestrator(cdnClient, logFactory, fileFactory);

        await sut.StartAsync(sourceUrl, destinationPath);

        fileFactory.Received(0).Create(Arg.Any<string>(), Arg.Any<string>());
        logFactory.Received(0).Create(Arg.Any<Stream>());
    }
}
