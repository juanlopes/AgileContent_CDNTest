using CandidateTesting.JuanMatheusLopes.Application.Interfaces;
using CandidateTesting.JuanMatheusLopes.Infrastructure.Factories;
using CandidateTesting.JuanMatheusLopes.Infrastructure.HttpClients;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateTesting.JuanMatheusLopes.Application.Modules;

public class InfrastructureModule : IDependencyModule
{
    public IServiceCollection RegisterDependencies(IServiceCollection services)
    {
        services.AddScoped<ICDNClient, CDNClient>();
        services.AddScoped(_ => new HttpClient());

        services.AddScoped<IFileFactory, FileFactory>();

        return services;
    }
}
